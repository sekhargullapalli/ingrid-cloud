using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MasterMind.WebApp.Models;
using MasterMind;
using Microsoft.AspNetCore.Authorization;
using MasterMind.Data;

namespace MasterMind.WebApp.Controllers
{    
    public class HomeController : Controller
    {
        private ICodeAnalyzer _codeAnalyzer;
        private GameDataContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICodeAnalyzer codeAnalyzer, GameDataContext context)
        {
            _logger = logger;
            _codeAnalyzer = codeAnalyzer;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("game")]
        public IActionResult Game()
        {
            var game = new Game
            {
                Guid = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsCompleted = false
            };      
            game.Patterns.Add(new GamePattern
            {
                Level = 0,
                CodePeg1 = SiteExtensions.RandomEnum<CodePeg>(),
                CodePeg2 = SiteExtensions.RandomEnum<CodePeg>(),
                CodePeg3 = SiteExtensions.RandomEnum<CodePeg>(),
                CodePeg4 = SiteExtensions.RandomEnum<CodePeg>(),
                Game = game
            });
            _context.Games.Add(game);
            _context.GamePatterns.Add(game.Patterns.First());           
            return View(game);
        }
        [HttpGet("game/{gameid}")]
        public IActionResult Game(string gameid)
        {
            var game = _context.Games.FirstOrDefault(g => g.Guid == gameid);
            if (game == null)
                return RedirectToLocal("/game");
            return View(game);
        }     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        public IActionResult NotFoundRoute(string id)
        {
            Response.StatusCode = 404;
            id = id.Replace("*^S^*", "/");
            ViewData["OriginalRoute"] = id;
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
