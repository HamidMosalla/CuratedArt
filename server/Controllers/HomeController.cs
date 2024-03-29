﻿using CuratedArt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CuratedArt.Controllers
{
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        [HttpGet("about/{*slug}")]
        [HttpGet("submissions/{*slug}")]
        [HttpGet("admin/{*slug}")]
        [HttpGet("errors/{*slug}")]
        public IActionResult Index()
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