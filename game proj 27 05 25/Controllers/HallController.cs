using Microsoft.AspNetCore.Mvc;
using game_proj_27_05_25.Models;

namespace game_proj_27_05_25.Controllers
{
    public class HallController : Controller
    {
        List<Item> items = null;
        private const string SessionKey = "PantryItems";
        [Route("/Hall")]
        [HttpGet]
        public IActionResult Hall()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            if(items != null)
            {
                var laboratoryCard = items.FirstOrDefault(i => i.Id == 1);
                ViewBag.LaboratoryCardFound = laboratoryCard.WasFound;
                ViewBag.LaboratoryCardUsed = laboratoryCard.WasUsed;
            }
            else
            {
                ViewBag.LaboratoryCardFound = false;
                ViewBag.LaboratoryCardUsed = false;
            }
            return View();
        }

        [HttpGet("/Hall/GoRight")]
        public IActionResult GoRight()
        {
            return Redirect("/Pantry");
        }

        [HttpPost("/Hall/UseCard")]
        public IActionResult UseCard()
        {
            items = HttpContext.Session.Get<List<Item>>(SessionKey);
            if (items != null)
            {
                var laboratoryCard = items.FirstOrDefault(i => i.Id == 1);
                if (laboratoryCard != null)
                {
                    laboratoryCard.WasUsed = true;
                    ViewBag.LaboratoryCardUsed = laboratoryCard.WasUsed;
                    HttpContext.Session.Set(SessionKey, items);
                    return Ok();
                }
                else return NotFound();
            }
            else return BadRequest();
        }

        [HttpGet("/Hall/GoLeft")]
        public IActionResult GoLeft()
        {
            return Redirect("/Laboratory");
        }

        [HttpGet("/Hall/GoBack")]
        public IActionResult GoBack()
        {
            return Redirect("/PilotCabin");
        }
    }
}
