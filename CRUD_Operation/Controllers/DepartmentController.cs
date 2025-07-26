using CRUD_Operation.Database;
using CRUD_Operation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDBContext db;

        public DepartmentController(AppDBContext context)
        {
            db = context;
        }

        // GET: Department
        public IActionResult Index()
        {
            var departments = db.Departments
                .Where(d => !d.IsDeleted)
                .Include(d => d.EmployeesList)
                .Take(6) // Limit for the landing page
                .ToList();

            

            // Get some statistics for the dashboard
            ViewBag.TotalDepartments = db.Departments.Count(d => !d.IsDeleted);
            ViewBag.TotalEmployees = db.Employees.Count(e => !e.IsDeleted);
            ViewBag.TotalProjects = db.Projects.Count(p => !p.IsDeleted);
            ViewBag.TotalBudget = db.Departments.Where(d => !d.IsDeleted).Sum(d => d.Budget);

            return View(departments);
        }

        // GET: Department/ListAll
        public IActionResult ListAll()
        {
            var departments = db.Departments
                .Where(d => !d.IsDeleted)
                .Include(d => d.EmployeesList)
                .ToList();

            

            return View("ListAll", departments);
        }

        // GET: Department/Details/5
        public IActionResult Details(int id)
        {
            var department = db.Departments
                .Include(d => d.EmployeesList!.Where(e => !e.IsDeleted))
                .FirstOrDefault(d => d.ID == id && !d.IsDeleted);

            if (department == null)
                return NotFound();

           
            return View(department);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateDepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create a new Department
                    var newDepartment = new Department
                    {
                        Name = model.Name,
                        Code = model.Code,
                        Description = model.Description,
                        Budget = model.Budget,
                        CreatedOn = DateTime.Now,
                        CreatedBy = "System Admin", // You can get this from user context
                        ModifiedOn = null,
                        ModifiedBy = null,
                        IsDeleted = false,
                        DeleteTime = null
                    };

                    db.Departments.Add(newDepartment);
                    db.SaveChanges();

                    TempData["Success"] = "Department created successfully!";
                    return RedirectToAction(nameof(ListAll));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the department: " + ex.Message);
                }
            }

            return View(model);
        }

        // GET: Department/Edit/5
        public IActionResult Edit(int id)
        {
            var department = db.Departments
                .FirstOrDefault(d => d.ID == id);

            if (department == null || department.IsDeleted)
                return NotFound();

            // Map department to EditDepartmentViewModel
            var viewModel = new EditDepartmentViewModel
            {
                ID = department.ID,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                Budget = department.Budget
            };

            return View(viewModel);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditDepartmentViewModel model)
        {
            if (id != model.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = db.Departments.FirstOrDefault(d => d.ID == id && !d.IsDeleted);
                    if (existing == null)
                        return NotFound();

                    // Update properties
                    existing.Name = model.Name;
                    existing.Code = model.Code;
                    existing.Description = model.Description;
                    existing.Budget = model.Budget;
                    existing.ModifiedOn = DateTime.Now;
                    existing.ModifiedBy = "System Admin"; // You can get this from user context

                    db.SaveChanges();

                    TempData["Success"] = "Department updated successfully!";
                    return RedirectToAction(nameof(ListAll));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the department: " + ex.Message);
                }
            }

            return View(model);
        }

        // GET: Department/Delete/5
        public IActionResult Delete(int id)
        {
            var department = db.Departments
                .Include(d => d.EmployeesList!.Where(e => !e.IsDeleted))
                .FirstOrDefault(d => d.ID == id);

            if (department == null || department.IsDeleted)
                return NotFound();

            
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = db.Departments.Find(id);
            if (department == null || department.IsDeleted)
                return NotFound();

            // Check if department has employees
            var hasEmployees = db.Employees.Any(e => e.DepartmentID == id && !e.IsDeleted);
            if (hasEmployees)
            {
                TempData["Error"] = "Cannot delete department because it has active employees. Please reassign or remove employees first.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            // Soft delete
            department.IsDeleted = true;
            department.DeleteTime = DateTime.Now;
            db.SaveChanges();

            TempData["Success"] = "Department deleted successfully!";
            return RedirectToAction(nameof(ListAll));
        }
    }

    // ViewModel for Create action to handle model binding
    public class CreateDepartmentViewModel
    {
        [Required]
        [Display(Name = "Department Name")]
        [StringLength(50)]
        public string Name { get; set; } = "";

        [Required]
        [Display(Name = "Department Code")]
        [StringLength(20)]
        public string Code { get; set; } = "";

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
        [Display(Name = "Budget")]
        public double Budget { get; set; }
    }

    // ViewModel for Edit action to handle model binding
    public class EditDepartmentViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        [StringLength(50)]
        public string Name { get; set; } = "";

        [Required]
        [Display(Name = "Department Code")]
        [StringLength(20)]
        public string Code { get; set; } = "";

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
        [Display(Name = "Budget")]
        public double Budget { get; set; }
    }
}
