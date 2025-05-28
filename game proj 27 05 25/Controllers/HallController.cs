using Microsoft.AspNetCore.Mvc;
using game_proj_27_05_25.Models;

namespace game_proj_27_05_25.Controllers
{
    public class HallController : Controller
    {
        private const string SessionKey = "HallItems";
        [Route("/hall")]
        [HttpGet]
        public IActionResult Hall()
        {
            var items = 
            return View();
        }

    }
}
