using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
