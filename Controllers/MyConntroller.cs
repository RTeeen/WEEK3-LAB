using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WEEK3_LAB.Models;
using Microsoft.AspNetCore.Hosting;

namespace WEEK3_LAB.Controllers
{
    public class MyController : Controller
    {
        private readonly ILogger<MyController> _logger;

        private readonly IWebHostEnvironment _env;

        public MyController(ILogger<MyController> logger, IWebHostEnvironment env)
        {
            _env = env;
            _logger = logger;
        }

        public Array GetFiles() {
            string contentPath = _env.ContentRootPath;
            string[] files = Directory.GetFiles($"{contentPath + "/TextFiles"}", "*.txt").ToArray();
            return files;
        }

        public IActionResult Index()
        {
            ViewBag.file = GetFiles();
            return View();
        }

        public IActionResult FileInfo(string id)
        {

            string contentPath = _env.ContentRootPath;
            string[] file = Directory.GetFiles($"{contentPath + "/TextFiles"}", $"{id}").ToArray();
            string content = System.IO.File.ReadAllText(file[0]);
            ViewBag.singleFile = id;
            return View((object)content);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
