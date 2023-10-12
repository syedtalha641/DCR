using DCRConsumeWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DCRConsumeWebApi.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Dashboard()
        {
            return View();
        }

      
    }
}