using System;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using T3RMSWS.Data;
using T3RMSWS.Models;

namespace T3RMSWS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public HomeController(ApplicationDbContext context)
        {
            try
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult RedirectUser()
        {
            if (User.IsInRole("Manager"))
                return RedirectToAction("Index", "Home", new { area = "Management" });
            else if (User.IsInRole("Member"))
                return RedirectToAction("Index", "Home", new { area = "Member" });
            else
                return LocalRedirect("/");
        }
    }
}
