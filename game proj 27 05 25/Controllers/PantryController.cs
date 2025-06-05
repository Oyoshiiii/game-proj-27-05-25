using Microsoft.AspNetCore.Mvc;
using game_proj_27_05_25.Models;

namespace game_proj_27_05_25.Controllers
{
    public class PantryController : Controller
    {
        List<Item> items;
        FlashLight flashLight;
        LaboratoryCard labCard;
        private const string SessionKeyPantry = "PantryItems";
        private const string SessionKeyPilotCabin = "PilotCabinItems";
        [Route("/Pantry")]
        public IActionResult Pantry()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKeyPantry);
            if (items == null)
            {
                items = PantryDefault();
            }

            var pcItems = HttpContext.Session.Get<List<Item>>(SessionKeyPilotCabin);
            var photo = pcItems.FirstOrDefault(i => i.Id == 1);
            //тоже самое на фонарик еще сделать надо, его найдут на кровати за подушкой
            //(поверх нее сделать кнопку чуть высветленную или попытаться вырезать аккуратно подушку)
            //после нажатия просто перед подушкой появится небольшой фонарик - будто его вытащили из-под нее
            //и уже тогда на сам фонарик можно будет нажать и подобрать
            //и с картой от лаборатории тоже
            if (photo != null)
            {
                ViewBag.PhotoFound = photo.WasFound;
                ViewBag.PhotoUsed = photo.WasUsed;
            }
            else
            {
                ViewBag.PhotoFound = false;
                ViewBag.PhotoUsed = false;
            }
            return View(items);
        }

        [HttpGet("Pantry/GetDescription/{id}")]
        public IActionResult GetDescription(int id)
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKeyPantry);
            var item = items?.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound(new { error = "Ошибка, такого элемента нет" });
            }
            else return Ok(item.Description);
        }

        [HttpPost("/Pantry/TakeItem{id}")]
        public IActionResult TakeItem(int id)
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKeyPantry);
            var item = items?.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound(new { success = false, message = "Ошибка, такого предмета нет" });
            }

            item.WasFound = true;
            HttpContext.Session.Set(SessionKeyPantry, items);

            return Ok(new { success = true, message = $"Вы взяли: {item.Name}", itemId = id });
        }

        [HttpGet("/Pantry/GoBack")]
        public IActionResult GoBack()
        {
            return Redirect("/Hall");
        }

        private List<Item> PantryDefault()
        {
            labCard = new LaboratoryCard();
            flashLight = new FlashLight();
            items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "Ключ-карта от лаборатории",
                    WasFound = labCard.WasFound,
                    WasUsed = labCard.WasUsed,
                    Description = "Ключ-карта от лаборатории. Вроде бы она должна быть в холле слева.."
                },
                new Item
                {
                    Id = 2,
                    Name = "Фонарик",
                    WasFound = flashLight.WasFound,
                    Description = "Небольшой фонарик, который легко установить на плечо. Жаль только, что заряда его хватит на несколько использований"
                }
            };

            HttpContext.Session.Set(SessionKeyPantry, items);
            return items;
        }
    }
}
