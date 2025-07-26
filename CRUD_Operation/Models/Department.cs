using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation.Models
{
    public class Department
    {
        // properties

        // Primary Key
        [Key]
        [Required]
        public int ID { get; set; }

        // Department Properties
        [Required]
        [Display(Name = "Department Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Department Code")]
        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        // Computed property to calculate employee count
        [NotMapped]
        [Display(Name = "Number of Employees")]
        public int CountEmployees => EmployeesList?.Count(e => !e.IsDeleted) ?? 0;

       
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
        [Column(TypeName = "decimal(18,2)")]
        public double Budget { get; set; }

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
        public DateTime? DeleteTime { get;  set; }


        // Navigation Property
        public List<Employee>? EmployeesList { get; set; }
        
    


    
    }
}
