using Microsoft.AspNetCore.Mvc;
using game_proj_27_05_25.Models;

namespace game_proj_27_05_25.Controllers
{
    public class PantryController : Controller
    {
        List<Item> items;
        FlashLight flashLight;
        LaboratoryCard labCard;
        private const string SessionKey = "PantryItems";
        [Route("/Pantry")]
        public IActionResult Pantry()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            if (items == null)
            {
                items = PantryDefault();
                HttpContext.Session.Set(SessionKey, items);
            }
            return View(items);
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

            HttpContext.Session.Set(SessionKey, items);
            return items;
        }
    }
}
