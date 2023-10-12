using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.EntityModels;
using Repository;
using Microsoft.AspNetCore.Authentication;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using System.Drawing;
using Twilio.Http;
using Twilio.Jwt.AccessToken;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DCR.Helper.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepos _accountRepos;

        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAccountRepos accountRepos, IAuthenticationService authenticationService)
        {
            _accountRepos = accountRepos;
            _authenticationService = authenticationService;
        }




        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {

            try
            {
                return Ok(await _accountRepos.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost("{UserLoginId}")]
        public async Task<ActionResult<User>> GetUser(string UserLoginId)
        {
            try
            {
                var result = await _accountRepos.GetUser(UserLoginId);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }


        [HttpPost("")]
        public async Task<ActionResult<string>> GetUserEmail([FromBody] string UserLoginId)
        {
            try
            {
                var result = await _accountRepos.GetUserEmail(UserLoginId);
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error in Retreiving!");
            }
        }


        [HttpPost]
        public async Task<ActionResult<string>> CreateUser(string UserLoginId, string UserName, string UserEmail, string UserPassword)
        {
            try
            {

                if (User == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _accountRepos.AddUser(UserLoginId, UserName, UserEmail, UserPassword);
                return CreatedAtAction(nameof(GetUsers), new { id = CreatedUser.UserId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost("")]
        public async Task<ActionResult<User>> LoginUser([FromBody] PasswordUpdateViewModel model)
        {

            try
            {
                if (User == null)
                {
                    return BadRequest("Invalid input"); // Return a BadRequest with a message
                }

                var loggedInUser = await _accountRepos.LoginUser(model.UserLoginId,model.UserPassword);

                if (loggedInUser != null)
                {
                    // Login successful, return a success message along with the user object

                   
                    return Ok(new { Message = "Login successful", User = loggedInUser,});
                }
                else
                {
                    // Login failed, return a 401 Unauthorized status with a message
                    return Unauthorized("Invalid username or password");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Login Failed");
            }
        }




        [HttpPost("")]
        public async Task<ActionResult<string>> UpdateUserPassword([FromBody] PasswordUpdateViewModel updateViewModel)
        {
            if (updateViewModel == null || string.IsNullOrWhiteSpace(updateViewModel.UserLoginId) || string.IsNullOrWhiteSpace(updateViewModel.UserPassword))
            {
                return BadRequest("Invalid request");
            }

            var passwordUpdated = await _accountRepos.UpdateUserPassword(updateViewModel.UserLoginId, updateViewModel.UserPassword);

            if (passwordUpdated != null)
            {
                return Ok("Password updated successfully.");
            }
            else
            {
                return NotFound("User not found.");
            }
        }




        [HttpDelete("")]
        public async Task<ActionResult<User>> DeleteUser([FromBody] string UserLoginId)
        {
            try
            {

                var userDelete = await _accountRepos.GetUser(UserLoginId);
                if (userDelete == null)
                {
                    return NotFound($"User Id ={UserLoginId} not found!");
                }
                return await _accountRepos.DeleteUser(UserLoginId);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Deleting!");

            }
        }

    }
}
