﻿using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Text;

namespace DCRConsumeWebApi.Controllers
{
    public class CustomerController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7169/api");
        private readonly HttpClient _httpClient;
        private readonly IToastNotification _toastNotification;

        public CustomerController(IToastNotification toastNotification)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
            _toastNotification = toastNotification;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetCustomers(CustomerViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Customer/GetCustomers", content);


            // Handle successful response
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON content into a list
            List<CustomerViewModel> customeritems = JsonConvert.DeserializeObject<List<CustomerViewModel>>(responseContent);

            return Json(responseContent);

        }






        //[HttpPost]
        //public async Task<JsonResult> AddCustomer(CustomerViewModel model)
        //{
        //    string data = JsonConvert.SerializeObject(model);
        //    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Customer/CreateCustomer", content);


        //    // Handle successful response
        //    var responseContent = await response.Content.ReadAsStringAsync();

           
        //    return Json(responseContent);

        //}





    }
}