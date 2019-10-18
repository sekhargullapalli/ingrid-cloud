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

namespace MasterMind.WebApp.Controllers
{    
    public class HomeController : Controller
    {
        private ICodeAnalyzer _codeAnalyzer;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICodeAnalyzer codeAnalyzer)
        {
            _logger = logger;
            _codeAnalyzer = codeAnalyzer;
        }

        public IActionResult Index()
        {
            return View();
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
