using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiConsume.Models;

namespace WebApiConsume.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7169/api/account") // Replace with the actual login API URL
            };

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
           

            try
            {
                // Send a POST request to the login API
                var response = await _httpClient.PostAsJsonAsync("api/account/LoginUser", loginViewModel);

                if (response.IsSuccessStatusCode)
                {
                    // Authentication successful
                    // Redirect the user to a dashboard or home page
                    return RedirectToAction("Dashboard");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Invalid credentials
                    ModelState.AddModelError("", "Invalid username or password");
                }
                else
                {
                    // Handle other error cases
                    ModelState.AddModelError("", "An error occurred while processing your request");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
            }
            return View(loginViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
    }
}

