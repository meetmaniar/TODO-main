using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using todo.web.Models;
using todo.web.Resources;
using todo.web.Services;

namespace todo.web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ITodoClient client) : base(client)
        {
        }

        [HttpGet("")]
        public IActionResult GetSplash()
        {
            return View("Splash");
        }

        [HttpGet("/{culture:regex(en)}/home", Name = "IndexEn")]
        [HttpGet("/{culture:regex(fr)}/accueil", Name = "IndexFr")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("/{culture:regex(en)}/change-user")]
        [HttpGet("/{culture:regex(fr)}/chnage-utilisateur")]
        public async Task<IActionResult> ChangeUserGet() => View("ChangeUser");

        [HttpPost("/{culture:regex(en)}/change-user")]
        [HttpPost("/{culture:regex(fr)}/chnage-utilisateur")]
        public async Task<IActionResult> ChangeUserPost(string username)
        {
            HttpContext.Session.SetString(AppSettings.Instance.TodoUserHeader, username);
            TempData["Success"] = Labels.ChangesSavedSuccessfully;
            return RedirectToAction("Index");
        }

        [HttpGet("/{culture:regex(en)}/error")]
        [HttpGet("/{culture:regex(fr)}/erreur")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ShowRequestId = true });
        }
    }
}
