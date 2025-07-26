using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Operation.Models
{
    public class Training
    {
        // Primary Key
        [Key]
        [Required]
        public int ID { get; set; }

        // Training Properties
        [Required]
        [Display(Name = "Training Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Training Code")]
        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Training Type")]
        [StringLength(50)]
        public string TrainingType { get; set; }
        // e.g., "Technical", "Soft Skills", "Safety", "Compliance", "Leadership", "Project Management", "First Aid", "IT Security", "Customer Service", "Language Skills", "Team Building", "Time Management", "Diversity & Inclusion", "Product Training", "Sales Techniques"
        
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Duration (Hours)")]
        [Range(1, 1000, ErrorMessage = "Duration must be between 1 and 1000 hours")]
        public int DurationHours { get; set; }

        [Required]
        [Display(Name = "Training Status")]
        [StringLength(50)]
        public string Status { get; set; }
        // e.g., "Scheduled", "In Progress", "Completed", "Cancelled", "Postponed"

        [Display(Name = "Training Location")]
        [StringLength(200)]
        public string? Location { get; set; }

        [Display(Name = "Instructor Name")]
        [StringLength(100)]
        public string? InstructorName { get; set; }

        [Display(Name = "Training Capacity")]
        [Range(1, 500, ErrorMessage = "Max participants must be between 1 and 500")]
        public int? MaxParticipants { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive value")]
        [Column(TypeName = "decimal(18,2)")]
        public double Cost { get; set; }

        [Display(Name = "Training Materials")]
        [StringLength(500)]
        public string? Materials { get; set; }

        
        // Computed property to calculate employee count
        [NotMapped]
        [Display(Name = "Number of Participants")]
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
