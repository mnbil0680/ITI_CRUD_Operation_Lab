namespace CRUD_Operation.Models
{
    public class Employee
    {

        // Properties
        public int ID { get; private set; }
        public string ImagePath { get; private set; }
        public string EmployeeNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Address { get; private set; }
        public int Age { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Position { get; private set; }
        public DateTime HireDate { get; private set; }
        public double Salary { get; private set; }
        public string? Notes { get; set; }

        // Six Essentails Properties
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        // Realtions
        public string? Project { get; private set; }
        public string? Manager { get; private set; }
        public string? Department { get; private set; }
        public string? Training { get; private set; }
    }
}
