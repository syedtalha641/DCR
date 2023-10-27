
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using DCR.Helper.ViewModel;
using NToastNotify;
using System.Drawing.Drawing2D;

namespace DCRConsumeWebApi.Controllers
{

    public class AccountController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7169/api");
        private readonly HttpClient _httpClient;
        private readonly IToastNotification _toastNotification;

        public AccountController(IToastNotification toastNotification)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
            _toastNotification = toastNotification;
        }

        //// Success Toast
        //_toastNotification.AddSuccessToastMessage("Woo hoo - it works!");

        // Info Toast
        //    _toastNotification.AddInfoToastMessage("Here is some information.");

        // Error Toast
        //    _toastNotification.AddErrorToastMessage("Woops an error occured.");

        // Warning Toast
        //    _toastNotification.AddWarningToastMessage("Here is a simple warning!");




        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(CombinedViewModel model)
        {
            try
            {

                // Create an object with user login ID and new password
                var LoginModel = new PasswordUpdateViewModel
                {

                    UserLoginId = model.LoginViewModel.UserLoginId,
                    UserPassword = model.LoginViewModel.UserPassword

                };



                string data = JsonConvert.SerializeObject(LoginModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Account/LoginUser", content);

                if (response.IsSuccessStatusCode)
                {
                    // Redirect to the dashboard
                    return RedirectToAction("Dashboard", "Home");
                }

                else if (!response.IsSuccessStatusCode)
                {
                    // Handle specific Bad Request response
                    string responseContent = await response.Content.ReadAsStringAsync();
                    TempData["errorMessage"] = responseContent;
                    return View();
                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

            return View();

        }



        [HttpPost]
        public async Task<JsonResult> JSONSendOTP(CombinedViewModel combinedModel)
        {
            JSONRsponse resp = new JSONRsponse();
            try
            {
                string data = JsonConvert.SerializeObject(combinedModel.LoginViewModel.UserLoginId);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Account/GetUserEmail", content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        combinedModel.OTPViewModel = new OTPViewModel();
                        combinedModel.OTPViewModel.To = jsonResponse;
                        combinedModel.OTPViewModel.From = "malikdaniyal681@gmail.com";
                        combinedModel.OTPViewModel.Password = "bzbw ense qpcr ticq";
                        combinedModel.OTPViewModel.RandomCode = (new Random()).Next(999999).ToString();
                        combinedModel.OTPViewModel.MessageBody = "You Are Hacked Please Stay Clam:" + combinedModel.OTPViewModel.RandomCode;

                        MailMessage mailMessage = new MailMessage();
                        mailMessage.To.Add(combinedModel.OTPViewModel.To);
                        mailMessage.From = new MailAddress(combinedModel.OTPViewModel.From);
                        mailMessage.Body = combinedModel.OTPViewModel.MessageBody;
                        mailMessage.Subject = "Password Resetting Code";

                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(combinedModel.OTPViewModel.From, combinedModel.OTPViewModel.Password);

                        try
                        {
                            await smtp.SendMailAsync(mailMessage);
                            resp.response = true;
                            resp.erorMessage = "OTP Sent Successfully";

                            // Store OTP in session
                            HttpContext.Session.SetString("OTP", combinedModel.OTPViewModel.RandomCode);

                            // Store UserLoginId in session
                            HttpContext.Session.SetString("UserLoginId", combinedModel.LoginViewModel.UserLoginId);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);

                        }
                    }
                    else
                    {
                        resp.response = false;
                        resp.erorMessage = "Invalid Login ID";

                    }
                }
                else
                {
                    resp.hasError = true;
                    resp.erorMessage = "Please Enter LoginId";

                }
            }
            catch (Exception ex)
            {
                resp.hasError = true;
                resp.erorMessage = ex.Message;

            }


            return Json(resp);
        }







        [HttpPost]
        public async Task<IActionResult> SendOTP(CombinedViewModel combinedModel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(combinedModel.LoginViewModel.UserLoginId);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Account/GetUserEmail", content);


                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();


                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        combinedModel.OTPViewModel.To = jsonResponse;
                        combinedModel.OTPViewModel.From = "malikdaniyal681@gmail.com";
                        combinedModel.OTPViewModel.Password = "bzbw ense qpcr ticq";
                        combinedModel.OTPViewModel.RandomCode = (new Random()).Next(999999).ToString();
                        combinedModel.OTPViewModel.MessageBody = "You Are Hacked Please Stay Clam:" + combinedModel.OTPViewModel.RandomCode;

                        MailMessage mailMessage = new MailMessage();
                        mailMessage.To.Add(combinedModel.OTPViewModel.To);
                        mailMessage.From = new MailAddress(combinedModel.OTPViewModel.From);
                        mailMessage.Body = combinedModel.OTPViewModel.MessageBody;
                        mailMessage.Subject = "Password Resetting Code";

                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(combinedModel.OTPViewModel.From, combinedModel.OTPViewModel.Password);


                        try
                        {
                            await smtp.SendMailAsync(mailMessage);
                            // Success Toast
                            _toastNotification.AddSuccessToastMessage("OTP Send Successfully To Your Email");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        // Error Toast
                        _toastNotification.AddErrorToastMessage("Email address not found in the response.");
                        return View("Login");
                    }
                }
                else
                {
                    // Error Toast
                    _toastNotification.AddErrorToastMessage("HTTP request was not successful.");
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View("Login");
            }

            // Store OTP in session
            HttpContext.Session.SetString("OTP", combinedModel.OTPViewModel.RandomCode);

            // Store UserLoginId in session
            HttpContext.Session.SetString("UserLoginId", combinedModel.LoginViewModel.UserLoginId);

            return View("Login");
        }







        [HttpPost]
        public async Task<IActionResult> VerifyOTP(CombinedViewModel model)
        {

            string storedOTP = HttpContext.Session.GetString("OTP");

            if (storedOTP == model.OTPViewModel.OTP)
            {
                //// Success Toast
                _toastNotification.AddSuccessToastMessage("OTP Matched");
            }
            else
            {
                // Error Toast
                _toastNotification.AddErrorToastMessage("OTP Did Not Matched");
            }

            return View("Login");
        } 
        



        
        
        [HttpPost]
        public async Task<JsonResult> JSONVerifyOTP(CombinedViewModel model)
        {
            JSONRsponse resp = new JSONRsponse();
            try
            {
                string storedOTP = HttpContext.Session.GetString("OTP");

                if (storedOTP == model.OTPViewModel.OTP)
                {
                    resp.response = storedOTP;
                }
                else
                {
                    resp.hasError = true;
                }

            }
            catch (Exception ex)
            {
                resp.hasError = true;
                resp.erorMessage = ex.Message;
            }
            return Json(resp);
        }




        [HttpPost]
        public async Task<JsonResult> JsonMatchPassword(CombinedViewModel combinedModel)
        {

            JSONRsponse resp = new JSONRsponse();

            try
            {
                if (combinedModel.LoginViewModel.UserPassword == combinedModel.LoginViewModel.ConfirmPassword)
                {
                    string storedUserLoginId = HttpContext.Session.GetString("UserLoginId");
                    if (!string.IsNullOrEmpty(storedUserLoginId))
                    {
                        // Create an object with user login ID and new password
                        var UpdateModel = new PasswordUpdateViewModel
                        {

                            UserLoginId = storedUserLoginId,
                            UserPassword = combinedModel.LoginViewModel.UserPassword

                        };

                        // Serialize the object to JSON
                        string data = JsonConvert.SerializeObject(UpdateModel);
                        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");


                        // Send a POST request to UpdateUserPassword API
                        HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Account/UpdateUserPassword", content);

                        if (!response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            int statusCode = (int)response.StatusCode;

                            // Log or debug the response content and status code for troubleshooting
                            Console.WriteLine($"Response Status Code: {statusCode}");
                            Console.WriteLine($"Response Content: {responseContent}");
                        }



                        if (response.IsSuccessStatusCode)
                        {
                            resp.response = true;
                        }
                        else
                        {
                            resp.erorMessage = "Password Not Matched";
                        }
                    }
                }
                else
                {
                    resp.hasError = true;
                }
            }
            catch (Exception ex)
            {

                resp.hasError = true;
                resp.erorMessage = ex.Message;
            }

            return Json(resp);
        }





        [HttpPost]
        public async Task<IActionResult> MatchPassword(CombinedViewModel combinedModel)
        {
            if (combinedModel.LoginViewModel.UserPassword == combinedModel.LoginViewModel.ConfirmPassword)
            {
                string storedUserLoginId = HttpContext.Session.GetString("UserLoginId");
                if (!string.IsNullOrEmpty(storedUserLoginId))
                {
                    // Create an object with user login ID and new password
                    var UpdateModel = new PasswordUpdateViewModel
                    {

                        UserLoginId = storedUserLoginId,
                        UserPassword = combinedModel.LoginViewModel.UserPassword

                    };

                    // Serialize the object to JSON
                    string data = JsonConvert.SerializeObject(UpdateModel);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");


                    // Send a POST request to UpdateUserPassword API
                    HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Account/UpdateUserPassword", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        int statusCode = (int)response.StatusCode;

                        // Log or debug the response content and status code for troubleshooting
                        Console.WriteLine($"Response Status Code: {statusCode}");
                        Console.WriteLine($"Response Content: {responseContent}");
                    }



                    if (response.IsSuccessStatusCode)
                    {
                        // Success Toast
                        //_toastNotification.AddSuccessToastMessage("Password Updated Successfully");
                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        // Handle the case where the API call was not successful (e.g., display an error message).
                    }
                }
            }
            else
            {
                // Password and confirm password do not match, handle this case as needed.
            }

            return View("Login");
        }




   




        [HttpPost]
        public async Task<IActionResult> Logout()
        {
          
            return RedirectToAction("Login", "Account");
        }



















    }
}

