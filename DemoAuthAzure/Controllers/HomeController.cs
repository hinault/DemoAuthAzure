using DemoAuthAzure.Interfaces;
using DemoAuthAzure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace DemoAuthAzure.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IDemoAuthAzureService _demoAuthAzureService;

        public HomeController(ILogger<HomeController> logger, IDemoAuthAzureService demoAuthAzureService)
        {
            _logger = logger;
            _demoAuthAzureService = demoAuthAzureService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Weather()
        {
            return View(await _demoAuthAzureService.Obtenir());
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}