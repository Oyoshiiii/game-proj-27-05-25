using Microsoft.AspNetCore.Mvc;
using game_proj_27_05_25.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace game_proj_27_05_25.Controllers
{
    public class PilotCabinController : Controller
    {
        Photo photo = null;
        List<Item> items = null;
        private const string SessionKey = "PilotCabinItems";

        [HttpGet("/PilotCabin")]
        public IActionResult PilotCabin()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            if (items == null)
            {
                items = PilotCabinDefault();
                HttpContext.Session.Set(SessionKey, items);
            }
            return View(items);
        }

        [HttpGet("/PilotCabin/GetDescription/{id}")]
        public IActionResult GetDescription(int id)
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            var item = items?.FirstOrDefault(i => i.Id == id);

            if(item == null)
            {
                return NotFound(new { error = "Ошибка, такого элемента нет" });
            }
            else return Ok(item.Description);
        }

        [HttpPost("/PilotCabin/TakeItem/{id}")]
        public IActionResult TakeItem(int id)
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            var item = items?.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound(new { success = false, message = "Ошибка, такого предмета нет" });
            }

            item.WasFound = true;
            HttpContext.Session.Set(SessionKey, items);

            return Ok(new { success = true, message = $"Вы взяли: {item.Name}", itemId = id });
        }

        private List<Item> PilotCabinDefault()
        {
            photo = new Photo();
            items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "Фотография двух девушек в форме",
                    Interaction = InteractionType.PickupDialog,
                    WasFound = photo.WasFound,
                    WasUsed = photo.WasUsed,
                    Description = "Странная фотография с изображениеv двух девушек в форме. Интересно, они как-то связаны с этим местом?"
                }
            };

            HttpContext.Session.Set(SessionKey, items);
            return items;
        }
    }
}
