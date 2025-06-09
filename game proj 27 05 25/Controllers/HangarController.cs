using game_proj_27_05_25.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace game_proj_27_05_25.Controllers
{
    public class HangarController : Controller
    {
        List<Item> items = null;
        bool end;
        private const string SessionKey = "RoomItems";
        private const string EndKey = "End";
        [Route("/Hangar")]
        [HttpGet]
        public IActionResult Hangar()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            end = HttpContext.Session.Get<bool>(EndKey);
            if (items != null)
            {
                var protectCard = items.FirstOrDefault(i => i.Id == 1);
                ViewBag.ProtectCardFound = protectCard.WasFound;
                ViewBag.ProtectCardUsed = protectCard.WasUsed;
            }
            else
            {
                ViewBag.ProtectCardFound = false;
                ViewBag.ProtectCardUsed = false;
            }
            ViewBag.End = end;
            return View();
        }
        [HttpPost("/Hangar/UseItem")]
        public IActionResult UseItem()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            var protectCard = items.FirstOrDefault(i => i.Id == 1);
            protectCard.WasUsed = true;
            ViewBag.ProtectCardUsed = protectCard.WasUsed;
            HttpContext.Session.Set(SessionKey, items);

            return Ok(new { success = true, message = $"Вы использовали: {protectCard.Name}", itemId = 1 });
        }
        [HttpPost("/Hangar/PutOnItem")]
        public IActionResult PutOnItem()
        {
            end = true;
            HttpContext.Session.Set(EndKey, end);
            return Ok(new { success = true, message = $"Вы надели скафандр", itemId = 1 });
        }
        [HttpGet("/Hangar/GoBack")]
        public IActionResult GoBack()
        {
            return Redirect("/Laboratory");
        }
        [HttpGet("/Hangar/End")]
        public IActionResult End()
        {
            return Redirect("/End");
        }
    }
}
