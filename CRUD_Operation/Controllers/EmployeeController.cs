using CRUD_Operation.Database;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation.Controllers
{
    public class EmployeeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
