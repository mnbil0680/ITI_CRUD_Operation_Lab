using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation.Models
{
    public class Manager : Employee
    {

        public List<Employee>? EmployeesList { get; set; }
        

    }
}
