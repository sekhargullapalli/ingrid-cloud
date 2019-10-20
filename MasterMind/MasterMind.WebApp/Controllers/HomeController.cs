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
using Microsoft.EntityFrameworkCore;

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
                KeyPeg1= KeyPeg.None,
                KeyPeg2 = KeyPeg.None,
                KeyPeg3 = KeyPeg.None,
                KeyPeg4 = KeyPeg.None,
                Game = game
            });
            _context.Games.Add(game);
            _context.GamePatterns.Add(game.Patterns.First());
            _context.SaveChanges();
            return View(game);
        }
        [HttpPost]
        public IActionResult Index(GameViewModel model)
        {
            Game game = null;
            try
            {
                game = _context.Games.AsNoTracking().Include(p=>p.Patterns).Where(g => g.Guid == model.guid).FirstOrDefault();
                int currlevel = game.Patterns.Count();
                var basePatern = game.Patterns.Where(p => p.Level == 0).FirstOrDefault();
                List<CodePeg> code = new List<CodePeg>
                {
                    basePatern.CodePeg1,
                    basePatern.CodePeg2,
                    basePatern.CodePeg3,
                    basePatern.CodePeg4
                };
                List<CodePeg> guess = new List<CodePeg>
                {
                    (CodePeg)Enum.Parse(typeof(CodePeg), model.codepeg1),
                    (CodePeg)Enum.Parse(typeof(CodePeg), model.codepeg2),
                    (CodePeg)Enum.Parse(typeof(CodePeg), model.codepeg3),
                    (CodePeg)Enum.Parse(typeof(CodePeg), model.codepeg4)
                };
                List<KeyPeg> keys = _codeAnalyzer.EvaluateGuess(guess, code).ToList();
                List<KeyPeg> _keys = new List<KeyPeg>();
                foreach(var key in keys)
                    _keys.Add(key);
                while (_keys.Count < 4)
                    _keys.Add(KeyPeg.None);

                var pattern = new GamePattern
                {
                    Level=currlevel,
                    CodePeg1 = guess[0],
                    CodePeg2 = guess[1],
                    CodePeg3 = guess[2],
                    CodePeg4 = guess[3],
                    KeyPeg1 = _keys[0],
                    KeyPeg2 = _keys[1],
                    KeyPeg3 = _keys[2],
                    KeyPeg4 = _keys[3],
                    Game =game
                };
                game.Patterns.Add(pattern);
                game.IsCompleted = pattern.IsCompleted();
                _context.Games.Update(game);
                _context.GamePatterns.Add(pattern);                           
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                if(game==null)
                    RedirectToAction(nameof(HomeController.Index), "Home");
                return
                    View(game);
            }            
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
    }
}
