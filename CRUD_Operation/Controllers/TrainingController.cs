using CRUD_Operation.Database;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation.Controllers
{
    public class TrainingController : Controller
    {
        private readonly AppDBContext db;
        public IActionResult Index()
        {
            return View();
        }
    }
}
