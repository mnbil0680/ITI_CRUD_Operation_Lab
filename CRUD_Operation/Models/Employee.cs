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
        public bool IsDeleted { get; private set; }

        [Display(Name = "Delete On Date")]
        public DateTime? DeleteTime { get; private set; }

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
    }
}
