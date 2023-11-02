using DCR.ViewModel.ViewModel;
using DCRConsumeWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Diagnostics;
using System.Text;

namespace DCRConsumeWebApi.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7169/api");
        private readonly HttpClient _httpClient;
        private readonly IToastNotification _toastNotification;

        public HomeController(IToastNotification toastNotification)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Dashboard(MenuListViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/MenuList/GetMenuLists", content);


            // Handle successful response
            //var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON content into a list
            string responseContent = await consumeapi(data, "/MenuList/GetMenuLists");

            List<MenuListViewModel> menuItems = JsonConvert.DeserializeObject<List<MenuListViewModel>>(responseContent);

            return Json(responseContent);

        }


        private async Task<string> consumeapi(string body = "", string apiPath = "")
        {
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + apiPath, content);

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }


    }
}