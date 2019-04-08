using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeForDummies.Models;

namespace RecipeForDummies.Controllers
{
    public class HomeController : Controller
    {
        private RoleManager<IdentityRole> roleManager;

        public HomeController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.CreateRoles();
        }

        private void CreateRoles()
        {
            if (!roleManager.RoleExistsAsync("Moderator").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Moderator"));
            }
        }

        public IActionResult Index()
        {

            return RedirectToAction("Browse", "Recipe");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Authorize]
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
