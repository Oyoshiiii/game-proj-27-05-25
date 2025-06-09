using game_proj_27_05_25.Models;
using Microsoft.AspNetCore.Mvc;

namespace game_proj_27_05_25.Controllers
{
    public class LaboratoryController : Controller
    {
        List<Item> items = null;
        private const string SessionKey = "PantryItems";
        [Route("/Laboratory")]
        [HttpGet]
        public IActionResult Laboratory()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            if (items != null)
            {
                var flashlight = items.FirstOrDefault(i => i.Id == 2);
                ViewBag.FlashlightFound = flashlight.WasFound;
                ViewBag.FlashlightUsed = flashlight.WasUsed;
            }
            else
            {
                ViewBag.FlashlightFound = false;
                ViewBag.FlashlightUsed = false;
            }
            return View();
        }
        [HttpPost("/Laboratory/UseItem")]
        public IActionResult UseItem()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            var flashlight = items.FirstOrDefault(i => i.Id == 2);
            flashlight.WasUsed = true;
            ViewBag.FlashlightUsed = flashlight.WasUsed;
            HttpContext.Session.Set(SessionKey, items);

            return Ok(new { success = true, message = $"Вы использовали: {flashlight.Name}", itemId = 2 });
        }
        [HttpGet("/Laboratory/GoUp")]
        public IActionResult GoUp()
        {
            return Redirect("/Hangar");
        }
        [HttpGet("/Laboratory/GoRight")]
        public IActionResult GoRight()
        {
            return Redirect("/Room");
        }
        [HttpGet("/Laboratory/GoBack")]
        public IActionResult GoBack()
        {
            return Redirect("/Hall");
        }
    }
}
