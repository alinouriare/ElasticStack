using BlogEngine.Output.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Output.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISiteRepository siteRepository;

        public HomeController(ILogger<HomeController> logger, ISiteRepository siteRepository)
        {
            _logger = logger;
            this.siteRepository = siteRepository;
        }

        public IActionResult Index()
        {
            var posts = siteRepository.GetAll();
            return View(posts);
        }
        public IActionResult Detail(string postId)
        {
            var post = siteRepository.Find(postId);
            return View(post);
        }
        public IActionResult Search(string q)
        {
            var posts = siteRepository.Search(q);
            return View(posts);
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
