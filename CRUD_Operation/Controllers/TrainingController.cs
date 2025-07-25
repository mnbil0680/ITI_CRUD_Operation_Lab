using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation.Controllers
{
    public class TrainingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
