using Microsoft.AspNetCore.Mvc;
using game_proj_27_05_25.Models;

namespace game_proj_27_05_25.Controllers
{
    public class PilotCabinController : Controller
    {
        private const string SessionKey = "PilotCabinItems";
        [Route("/PilotCabin")]
        [HttpGet]
        public IActionResult PilotCabin()
        {
            
            return View();
        }
    }
}
