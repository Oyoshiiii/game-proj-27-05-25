﻿using Microsoft.AspNetCore.Mvc;

namespace game_proj_27_05_25.Controllers
{
    public class EndController : Controller
    {
        [Route("/End")]
        public IActionResult End()
        {
            return View();
        }
    }
}
