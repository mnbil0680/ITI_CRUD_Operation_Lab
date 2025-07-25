using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
