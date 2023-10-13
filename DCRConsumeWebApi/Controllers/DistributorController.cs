using Microsoft.AspNetCore.Mvc;

namespace DCRConsumeWebApi.Controllers
{
    public class DistributorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
