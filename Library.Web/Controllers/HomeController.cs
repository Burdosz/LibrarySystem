using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Library.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using Library.Web.Models;
using Microsoft.Extensions.Options;

namespace Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private EnvironmentConfig _environmentConfiguration;

        public HomeController(IOptions<EnvironmentConfig> configuration)
        {
            _environmentConfiguration = configuration.Value;
        }

        public IActionResult Index()
        {
            var res = FetchBorrows().Result;
            Console.WriteLine(res);
            ViewData["borrow"] = res;
            return View();
        }
        
        private async Task<string> FetchBorrows()
        {
            
            var client = new HttpClient();
            //var httpLocalhostApiBorrows = "http://library:80/api/borrows/1";
            var httpLocalhostApiBorrows = $@"{_environmentConfiguration.LibraryWebApiServiceHost}/api/borrows/1";
            Console.WriteLine($"blabla {_environmentConfiguration.LibraryWebApiServiceHost}");
            var c = await client.GetStringAsync(httpLocalhostApiBorrows);
            
            return c;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}