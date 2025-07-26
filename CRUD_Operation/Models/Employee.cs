using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Operation.Models
{
    public class Employee
    {
        // Properties  
        [Key]
        [Required]
        public int ID { get; private set; }

        [Display(Name = "Profile Image")]
        [StringLength(500)]
        public string ImagePath { get; private set; }

        [Required]
        [Display(Name = "Employee Number")]
        [StringLength(20)]
        public string EmployeeNumber { get; private set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName { get; private set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; private set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; private set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        [StringLength(20)]
        public string PhoneNumber { get; private set; }

        [StringLength(200)]
        public string? Address { get; private set; }

        [Required]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; private set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; private set; }

        [Required]
        [StringLength(100)]
        public string Position { get; private set; }

        [Required]
        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; private set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
        [Column(TypeName = "decimal(18,2)")]
        public double Salary { get; private set; }

        [StringLength(2000)]
        public string? Notes { get; set; }

        // Six Essentials Properties  
        [Required]
        [Display(Name = "Created On Date")]
        public DateTime CreatedOn { get; private set; }

        [Required]
        [Display(Name = "Created By")]
        [StringLength(100)]
        public string CreatedBy { get; private set; }

        [Display(Name = "Modified On Date")]
        public DateTime? ModifiedOn { get; private set; }

        [Display(Name = "Modified By")]
        [StringLength(100)]
        public string? ModifiedBy { get; private set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Delete On Date")]
        public DateTime? DeleteTime { get; set; }

        // Relations  
        [ForeignKey("Project")]
        [StringLength(100)]
        public int? ProjectID { get; set; }
        public Project? Project { get; set; }

        [ForeignKey("Manager")]
        [StringLength(100)]
        public int? MangerID { get; set; }
        public Manager? Manager { get; set; }

        [ForeignKey("Department")]
        [StringLength(100)]
        public int? DepartmentID { get; set; }
        public Department? Department { get; set; }

        [ForeignKey("Training")]
        [StringLength(100)]
        public int? TrainingID { get; set; }
        public Training? Training { get; set; }
        // CTORs

        public Employee(
            
            string imagePath,
            string employeeNumber,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string? address,
            DateTime dateOfBirth,
            string position,
            DateTime hireDate,
            double salary,
            string? notes,
            DateTime createdOn,
            string createdBy,
            DateTime? modifiedOn,
            string? modifiedBy,
            bool isDeleted,
            DateTime? deleteTime,
            int? projectID,
            Project? project,
            int? mangerID,
            Manager? manager,
            int? departmentID,
            Department? department,
            int? trainingID,
            Training? training
        )
        {
            
            ImagePath = imagePath;
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Age = CalcAge(dateOfBirth);
            DateOfBirth = dateOfBirth;
            Position = position;
            HireDate = hireDate;
            Salary = salary;
            Notes = notes;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
            ModifiedOn = modifiedOn;
            ModifiedBy = modifiedBy;
            IsDeleted = isDeleted;
            DeleteTime = deleteTime;
            ProjectID = projectID;
            Project = project;
            MangerID = mangerID;
            Manager = manager;
            DepartmentID = departmentID;
            Department = department;
            TrainingID = trainingID;
            Training = training;
        }

        // Parameterless constructor for EF Core and model binding
        public Employee()
        {
        }

        // CTORs
        public Employee(
            string imagePath,
            string employeeNumber,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string? address,
            DateTime dateOfBirth,
            string position,
            DateTime hireDate,
            double salary,
            string? notes,
            
            int? projectID,
            Project? project,
            int? mangerID,
            Manager? manager,
            int? departmentID,
            Department? department,
            int? trainingID,
            Training? training
        )
        {
            // From Parameters
            ImagePath = imagePath;
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = Address;
            DateOfBirth = dateOfBirth;
            Age = CalcAge(dateOfBirth);
            Position = position;
            HireDate = hireDate;
            Salary = salary;
            Notes = notes;
            DeleteTime = null;

            // Default
            CreatedOn = DateTime.Now;
            CreatedBy = "Mohamed Nabil";
            ModifiedOn = null;
            ModifiedBy = "None";
            IsDeleted = false;

            ProjectID = projectID;
            Project = project;
            MangerID = mangerID;
            Manager = manager;
            DepartmentID = departmentID;
            Department = department;
            TrainingID = trainingID;
            Training = training;





        }
        public int CalcAge(DateTime BirthDate)
        {
            if (BirthDate == default)
                return 0;

            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
        public Employee(
    string imagePath,
    string employeeNumber,
    string firstName,
    string lastName,
    string email,
    string phoneNumber,
    string? address,
    DateTime dateOfBirth,
    string position,
    DateTime hireDate,
    double salary,
    string? notes
)
        {
            // From Parameters
            ImagePath = imagePath;
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = Address;
            DateOfBirth = dateOfBirth;
            Age = CalcAge(dateOfBirth);
            Position = position;
            HireDate = hireDate;
            Salary = salary;
            Notes = notes;
            DeleteTime = null;

            // Default
            CreatedOn = DateTime.Now;
            CreatedBy = "Mohamed Nabil";
            ModifiedOn = null;
            ModifiedBy = "None";
            IsDeleted = false;

            ProjectID = null;
            Project =      null;
            MangerID =     null;
            Manager =      null;
            DepartmentID = null;
            Department =   null;
            TrainingID =   null;
            Training =     null;





        }
    }
}
