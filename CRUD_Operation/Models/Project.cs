using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Operation.Models
{
    public class Project
    {
        // Primary Key
        [Key]
        [Required]
        public int ID { get; set; }

        // Project Properties
        [Required]
        [Display(Name = "Project Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Project Code")]
        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
        [Column(TypeName = "decimal(18,2)")]
        public double Budget { get; set; }

        [Required]
        [Display(Name = "Project Status")]
        [StringLength(50)]
        public string Status { get; set; }
        // e.g., "Planning", "In Progress", "Completed", "On Hold", "Cancelled", "Delayed", "Under Review", "Approved", "Archived"

        [Display(Name = "Priority Level")]
        [StringLength(20)]
        public string? Priority { get; set; }
        // e.g., "Low", "Medium", "High", "Critical"

       
        
        // Computed property to calculate employee count
        [NotMapped]
        [Display(Name = "Number of Employees")]
        public int CountEmployees => EmployeesList?.Count(e => !e.IsDeleted) ?? 0;

        // Six Essentials Properties
        [Required]
        [Display(Name = "Created On Date")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Display(Name = "Created By")]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Display(Name = "Modified On Date")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Modified By")]
        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Delete On Date")]
        public DateTime? DeleteTime { get; set; }

        // Navigation Property
        public List<Employee>? EmployeesList { get; set; }
    }
}
