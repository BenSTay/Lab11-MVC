using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineApp.Models;

namespace WineApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Results(int minPoints = 0, int maxPoints = 100, decimal minPrice = 1, decimal maxPrice = 2400)
        {
            List<Wine> wines = Wine.GetWineList();
            var result = from wine in wines
                         where wine.Points >= minPoints &&
                               wine.Points <= maxPoints &&
                               wine.Price >= minPrice &&
                               wine.Price <= maxPrice
                         select wine;
            return View(result.Take(100).ToList());
        }
    }
}