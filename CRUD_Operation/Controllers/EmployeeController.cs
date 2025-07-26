using CRUD_Operation.Database;
using CRUD_Operation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        // GET: Employee
        public IActionResult Index()
        {
            var employees = db.Employees
                .Where(e => !e.IsDeleted)
                .Include(e => e.Department)
                .Include(e => e.Project)
                .Include(e => e.Manager)
                .Include(e => e.Training)
                .Take(2) // Limit to 6 for the landing page
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
            return View(employees);
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
            return View();
        }

        // In EmployeeController.cs, update the Create action to use the Employee constructor for initialization

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Optionally load related entities if you want to set navigation properties
                var project = employee.ProjectID.HasValue ? db.Projects.Find(employee.ProjectID) : null;
                var manager = employee.MangerID.HasValue ? db.Managers.Find(employee.MangerID) : null;
                var department = employee.DepartmentID.HasValue ? db.Departments.Find(employee.DepartmentID) : null;
                var training = employee.TrainingID.HasValue ? db.Trainings.Find(employee.TrainingID) : null;

                // Use the constructor to initialize the new employee
                var newEmployee = new Employee(
                    employee.ImagePath,
                    employee.EmployeeNumber,
                    employee.FirstName,
                    employee.LastName,
                    employee.Email,
                    employee.PhoneNumber,
                    employee.Address,
                    employee.DateOfBirth,
                    employee.Position,
                    employee.HireDate,
                    employee.Salary,
                    employee.Notes,
                    employee.ProjectID,
                    project,
                    employee.MangerID,
                    manager,
                    employee.DepartmentID,
                    department,
                    employee.TrainingID,
                    training
                );

                db.Employees.Add(newEmployee);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null || employee.IsDeleted)
                return NotFound();

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                // Optionally load related entities if you want to set navigation properties
                var project = employee.ProjectID.HasValue ? db.Projects.Find(employee.ProjectID) : null;
                var manager = employee.MangerID.HasValue ? db.Managers.Find(employee.MangerID) : null;
                var department = employee.DepartmentID.HasValue ? db.Departments.Find(employee.DepartmentID) : null;
                var training = employee.TrainingID.HasValue ? db.Trainings.Find(employee.TrainingID) : null;

                // Get the original employee from the database
                var existing = db.Employees.AsNoTracking().FirstOrDefault(e => e.ID == id && !e.IsDeleted);
                if (existing == null)
                    return NotFound();

                // Create a new Employee using the constructor, copying unchanged audit fields
                var updatedEmployee = new Employee(
                    employee.ImagePath,
                    employee.EmployeeNumber,
                    employee.FirstName,
                    employee.LastName,
                    employee.Email,
                    employee.PhoneNumber,
                    employee.Address,
                    employee.DateOfBirth,
                    employee.Position,
                    employee.HireDate,
                    employee.Salary,
                    employee.Notes,
                    existing.CreatedOn, // preserve original creation date
                    existing.CreatedBy, // preserve original creator
                    DateTime.Now,       // set modified date
                    employee.ModifiedBy ?? existing.ModifiedBy,
                    existing.IsDeleted, // preserve deletion status
                    existing.DeleteTime,
                    employee.ProjectID,
                    project,
                    employee.MangerID,
                    manager,
                    employee.DepartmentID,
                    department,
                    employee.TrainingID,
                    training
                );

                // Set the ID to match the entity being updated
                typeof(Employee).GetProperty("ID")?.SetValue(updatedEmployee, id);

                db.Entry(updatedEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null || employee.IsDeleted)
                return NotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null || employee.IsDeleted)
                return NotFound();

            employee.IsDeleted = true;
            employee.DeleteTime = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
