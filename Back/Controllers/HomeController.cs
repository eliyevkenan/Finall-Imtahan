using BackEnd.DAL;
using BackEnd.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext _contex;
        public HomeController(AppDbContext context)
        {
            _contex = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Teams = _contex.Team.ToList()
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
