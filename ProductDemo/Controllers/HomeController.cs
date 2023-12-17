using CommonLibrary;
using Microsoft.AspNetCore.Mvc;
using ProductDemo.Models;
using System.Diagnostics;

namespace ProductDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfigManager _configuration;

        public HomeController(ILogger<HomeController> logger, IConfigManager configuration)
        {
            this._configuration = configuration;            
            _logger = logger;
        }

        public IActionResult Index()
        {
            string strConn = _configuration.ProductDB;// reading from appsetting.json
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}