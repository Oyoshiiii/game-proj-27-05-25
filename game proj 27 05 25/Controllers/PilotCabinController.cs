using Microsoft.AspNetCore.Mvc;
using game_proj_27_05_25.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace game_proj_27_05_25.Controllers
{
    public class PilotCabinController : Controller
    {
        Photo photo;
        List<Item> items;
        private const string SessionKey = "PilotCabinItems";
        [Route("/PilotCabin")]
        [HttpGet]
        public IActionResult PilotCabin()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            if (items == null || items.Count == 0)
            {
                items = PilotCabinDefault();
            }
            else
            {
                var photoItem = items.FirstOrDefault(items => items.Id == 1);
                photo = new Photo(photoItem.WasFound, photoItem.WasUsed);
            }
            return View(items);
        }
        [HttpPost]
        public IActionResult Interact()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            if (items == null || items.Count == 0)
            {
                return NotFound();
            }
            return View();
        }

        private List<Item> PilotCabinDefault()
        {
            photo = new Photo();
            var items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "photo",
                    ImagePath = "/images/PilotCabin/photo.png",
                    X = 200,
                    Y = 400,
                    Interaction = InteractionType.PickupDialog,
                    WasFound = photo.WasFound,
                    WasUsed = photo.WasUsed
                }
            };

            HttpContext.Session.Set(SessionKey, items);
            return items;
        }
    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
