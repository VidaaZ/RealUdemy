using Microsoft.AspNetCore.Mvc;

namespace PracticeWithVideo.Controllers
{
    public class FoodTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
