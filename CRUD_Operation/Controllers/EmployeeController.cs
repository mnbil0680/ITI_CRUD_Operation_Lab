using CRUD_Operation.Database;
using CRUD_Operation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDBContext db;

        public EmployeeController(AppDBContext context)
        {
            db = context;
        }

        // GET: Employee
        public IActionResult Index()
        {
            var employees = db.Employees
                .Where(e => !e.IsDeleted)
                .Include(e => e.Department)
                .Include(e => e.Project)
                .Include(e => e.Manager)
                .Include(e => e.Training)
                .Take(2)
                .ToList();

            // Get some statistics for the dashboard
            ViewBag.TotalEmployees = db.Employees.Count(e => !e.IsDeleted);
            ViewBag.TotalDepartments = db.Departments.Count(d => !d.IsDeleted);
            ViewBag.TotalProjects = db.Projects.Count(p => !p.IsDeleted);
            ViewBag.TotalTrainings = db.Trainings.Count(t => !t.IsDeleted);

            return View(employees);
        }

        // GET: Employee
        public IActionResult ListAll()
        {
            var employees = db.Employees
                .Where(e => !e.IsDeleted)
                .Include(e => e.Department)
                .Include(e => e.Project)
                .Include(e => e.Manager)
                .Include(e => e.Training)
                .ToList();
            return View("ListAll", employees);
        }

        // GET: Employee/Details/5
        public IActionResult Details(int id)
        {
            var employee = db.Employees
                .Include(e => e.Department)
                .Include(e => e.Project)
                .Include(e => e.Manager)
                .Include(e => e.Training)
                .FirstOrDefault(e => e.ID == id && !e.IsDeleted);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            // Populate dropdown lists for related entities
            ViewBag.Departments = new SelectList(
                db.Departments.Where(d => !d.IsDeleted).OrderBy(d => d.Name),
                "ID", "Name");

            ViewBag.Projects = new SelectList(
                db.Projects.Where(p => !p.IsDeleted).OrderBy(p => p.Name),
                "ID", "Name");

            ViewBag.Managers = new SelectList(
                db.Managers.Where(m => !m.IsDeleted).OrderBy(m => m.FirstName),
                "ID", "FirstName");

            ViewBag.Trainings = new SelectList(
                db.Trainings.Where(t => !t.IsDeleted).OrderBy(t => t.Name),
                "ID", "Name");

            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Use the constructor to initialize the new employee
                    var newEmployee = new Employee(
                        model.ImagePath ?? "/images/default-avatar.jpg",
                        model.EmployeeNumber,
                        model.FirstName,
                        model.LastName,
                        model.Email,
                        model.PhoneNumber,
                        model.Address,
                        model.DateOfBirth,
                        model.Position,
                        model.HireDate,
                        model.Salary,
                        model.Notes,
                        model.ProjectID,
                        null, // Project will be loaded by EF
                        model.MangerID,
                        null, // Manager will be loaded by EF
                        model.DepartmentID,
                        null, // Department will be loaded by EF
                        model.TrainingID,
                        null  // Training will be loaded by EF
                    );

                    db.Employees.Add(newEmployee);
                    db.SaveChanges();

                    TempData["Success"] = "Employee created successfully!";
                    return RedirectToAction(nameof(ListAll));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the employee: " + ex.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            PopulateDropdowns();
            return View(model);
        }

        private void PopulateDropdowns()
        {
            ViewBag.Departments = new SelectList(
                db.Departments.Where(d => !d.IsDeleted).OrderBy(d => d.Name),
                "ID", "Name");

            ViewBag.Projects = new SelectList(
                db.Projects.Where(p => !p.IsDeleted).OrderBy(p => p.Name),
                "ID", "Name");

            ViewBag.Managers = new SelectList(
                db.Managers.Where(m => !m.IsDeleted).OrderBy(m => m.FirstName),
                "ID", "FirstName");

            ViewBag.Trainings = new SelectList(
                db.Trainings.Where(t => !t.IsDeleted).OrderBy(t => t.Name),
                "ID", "Name");
        }

        
        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null || employee.IsDeleted)
                return NotFound();

            return View(employee);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var department = db.Departments
                    .Include(d => d.EmployeesList)
                    .FirstOrDefault(d => d.ID == id);

                if (department == null || department.IsDeleted)
                {
                    TempData["Error"] = "Department not found or already deleted.";
                    return RedirectToAction(nameof(ListAll));
                }

                // Unassign employees from this department
                var employeesToUnassign = db.Employees
                    .Where(e => e.DepartmentID == id && !e.IsDeleted)
                    .ToList();

                foreach (var employee in employeesToUnassign)
                {
                    employee.DepartmentID = null; // Unassign department
                }

                // Soft delete the department
                department.IsDeleted = true;
                department.DeleteTime = DateTime.Now;

                db.SaveChanges();

                if (employeesToUnassign.Any())
                {
                    TempData["Success"] = $"Department deleted successfully! {employeesToUnassign.Count} employee(s) were unassigned from the department.";
                }
                else
                {
                    TempData["Success"] = "Department deleted successfully!";
                }

                return RedirectToAction(nameof(ListAll));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while deleting the department: {ex.Message}";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }



        // GET: Employee/Edit/5
        public IActionResult Edit(int id)
        {
            var employee = db.Employees
                .Include(e => e.Department)
                .Include(e => e.Project)
                .Include(e => e.Manager)
                .Include(e => e.Training)
                .FirstOrDefault(e => e.ID == id);

            if (employee == null || employee.IsDeleted)
                return NotFound();

            // Map employee to EditEmployeeViewModel
            var viewModel = new EditEmployeeViewModel
            {
                ID = employee.ID,
                ImagePath = employee.ImagePath,
                EmployeeNumber = employee.EmployeeNumber,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position,
                HireDate = employee.HireDate,
                Salary = employee.Salary,
                Notes = employee.Notes,
                ProjectID = employee.ProjectID,
                MangerID = employee.MangerID,
                DepartmentID = employee.DepartmentID,
                TrainingID = employee.TrainingID
            };

            PopulateDropdownsWithSelected(employee.DepartmentID, employee.ProjectID, employee.MangerID, employee.TrainingID);
            return View(viewModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditEmployeeViewModel model)
        {
            if (id != model.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = db.Employees.AsNoTracking().FirstOrDefault(e => e.ID == id && !e.IsDeleted);
                    if (existing == null)
                        return NotFound();

                    // Create a new Employee using the constructor
                    var updatedEmployee = new Employee(
                        model.ImagePath ?? "/images/default-avatar.jpg",
                        model.EmployeeNumber,
                        model.FirstName,
                        model.LastName,
                        model.Email,
                        model.PhoneNumber,
                        model.Address,
                        model.DateOfBirth,
                        model.Position,
                        model.HireDate,
                        model.Salary,
                        model.Notes,
                        existing.CreatedOn,
                        existing.CreatedBy,
                        DateTime.Now,
                        "System",
                        existing.IsDeleted,
                        existing.DeleteTime,
                        model.ProjectID,
                        null,
                        model.MangerID,
                        null,
                        model.DepartmentID,
                        null,
                        model.TrainingID,
                        null
                    );

                    // Set the ID using reflection
                    typeof(Employee).GetProperty("ID")?.SetValue(updatedEmployee, id);

                    db.Entry(updatedEmployee).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["Success"] = "Employee updated successfully!";
                    return RedirectToAction(nameof(ListAll));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the employee: " + ex.Message);
                }
            }

            // If we got this far, something failed, redisplay form with existing values
            PopulateDropdownsWithSelected(model.DepartmentID, model.ProjectID, model.MangerID, model.TrainingID);
            return View(model);
        }

        private void PopulateDropdownsWithSelected(int? selectedDepartmentId, int? selectedProjectId, int? selectedManagerId, int? selectedTrainingId)
        {
            ViewBag.Departments = new SelectList(
                db.Departments.Where(d => !d.IsDeleted).OrderBy(d => d.Name),
                "ID", "Name", selectedDepartmentId);

            ViewBag.Projects = new SelectList(
                db.Projects.Where(p => !p.IsDeleted).OrderBy(p => p.Name),
                "ID", "Name", selectedProjectId);

            ViewBag.Managers = new SelectList(
                db.Managers.Where(m => !m.IsDeleted).OrderBy(m => m.FirstName),
                "ID", "FirstName", selectedManagerId);

            ViewBag.Trainings = new SelectList(
                db.Trainings.Where(t => !t.IsDeleted).OrderBy(t => t.Name),
                "ID", "Name", selectedTrainingId);
        }



    }

    // ViewModel for Create action to handle model binding
    public class CreateEmployeeViewModel
    {
        [Display(Name = "Profile Image")]
        [StringLength(500)]
        public string? ImagePath { get; set; }

        [Required]
        [Display(Name = "Employee Number")]
        [StringLength(20)]
        public string EmployeeNumber { get; set; } = "";

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName { get; set; } = "";

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = "";

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = "";

        [StringLength(200)]
        public string? Address { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        public string Position { get; set; } = "";

        [Required]
        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; } = DateTime.Today;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
        public double Salary { get; set; }

        [StringLength(2000)]
        public string? Notes { get; set; }

        public int? ProjectID { get; set; }
        public int? MangerID { get; set; }
        public int? DepartmentID { get; set; }
        public int? TrainingID { get; set; }
    }
    // ViewModel for Edit action to handle model binding
    public class EditEmployeeViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Profile Image")]
        [StringLength(500)]
        public string? ImagePath { get; set; }

        [Required]
        [Display(Name = "Employee Number")]
        [StringLength(20)]
        public string EmployeeNumber { get; set; } = "";

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName { get; set; } = "";

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = "";

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = "";

        [StringLength(200)]
        public string? Address { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        public string Position { get; set; } = "";

        [Required]
        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
        public double Salary { get; set; }

        [StringLength(2000)]
        public string? Notes { get; set; }

        public int? ProjectID { get; set; }
        public int? MangerID { get; set; }
        public int? DepartmentID { get; set; }
        public int? TrainingID { get; set; }
    }



}

