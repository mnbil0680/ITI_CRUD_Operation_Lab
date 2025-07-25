using CRUD_Operation.Database;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDBContext db;
        public IActionResult Index()
        {
            return View();
        }
    }
}
