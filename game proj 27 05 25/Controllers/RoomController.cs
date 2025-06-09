using game_proj_27_05_25.Models;
using Microsoft.AspNetCore.Mvc;

namespace game_proj_27_05_25.Controllers
{
    public class RoomController : Controller
    {
        List<Item> items = null;
        ProtectionCabinCard protectCard = null;
        private const string SessionKey = "RoomItems";
        [Route("/Room")]
        [HttpGet]
        public IActionResult Room()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            if (items == null)
            {
                items = RoomDefault();
            }
        
            var protectCard = items.FirstOrDefault(i => i.Id == 1);
            ViewBag.ProtectCardFound = protectCard.WasFound;
            ViewBag.ProtectCardUsed = protectCard.WasUsed;
            return View();
        }

        [HttpPost("/Room/TakeItem/{id}")]
        public IActionResult TakeItem(int id)
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            var item = items?.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound(new { success = false, message = "Ошибка, такого предмета нет" });
            }

            item.WasFound = true;
            ViewBag.ProtectCardFound = item.WasFound;
            HttpContext.Session.Set(SessionKey, items);

            return Ok(new { success = true, message = $"Вы взяли: {item.Name}", itemId = id });
        }

        [HttpGet("/Room/GoBack")]
        public IActionResult GoBack()
        {
            return Redirect("/Laboratory");
        }

        private List<Item> RoomDefault()
        {
            protectCard = new ProtectionCabinCard();
            items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "Ключ-карта от шкафа с защитными скафандрами",
                    WasFound = protectCard.WasFound,
                    WasUsed = protectCard.WasUsed,
                    Description = "Ключ-карта от шкафа с защитными скафандрами. Думаю, стоит поторопиться, я все еще надеюсь успеть её найти"
                },
            };

            HttpContext.Session.Set(SessionKey, items);
            return items;
        }
    }
}
