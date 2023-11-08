﻿using Microsoft.AspNetCore.Mvc;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountRepos _accountRepos;

        public AccountController(IAccountRepos accountRepos)
        {
            _accountRepos = accountRepos;
        }

        [HttpPost]
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

        [HttpPost]
        public async Task<ActionResult<LoginViewModel>> GetUser([FromBody] string UserLoginId)
        {
            var result = await _accountRepos.GetUser(UserLoginId);
            try
            {   
                if (result == null)
                {
                    throw new Exception("UserLoginID Not Found");
                }
            }
            catch (Exception)
            {
                throw new Exception("API Not Respond");

            }
            return result;
        }

        [HttpPost]
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
        public async Task<ActionResult<string>> GetUserPhoneNumber([FromBody] string UserLoginId)
        {
            try
            {
                var result = await _accountRepos.GetUserPhoneNumber(UserLoginId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retreiving!");
            }
        }



        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] LoginViewModel model )
        {
            try
            {

                if (User == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _accountRepos.AddUser(model);
                return StatusCode(StatusCodes.Status201Created, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<bool>> LoginUser([FromBody] PasswordUpdateViewModel model)
        {

            try
            {
                if (User == null)
                {
                   throw new Exception("Object Cannot be Null!"); // Return a BadRequest with a message
                }

                var loggedInUser = await _accountRepos.LoginUser(model);

                if (loggedInUser != null)
                {
                    // Login successful, return a success message along with the user object
                    throw new Exception("Values Cannot be Null!");
                }
                else
                {
                    // Login failed, return a 401 Unauthorized status with a message
                    throw new Exception("Invalid input");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Login Failed");
            }
        }




        [HttpPost]
        public async Task<ActionResult> UpdateUserPassword([FromBody] PasswordUpdateViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.UserLoginId) || string.IsNullOrWhiteSpace(model.UserPassword))
            {
                return BadRequest("Invalid request");
            }

            var passwordUpdated = await _accountRepos.UpdateUserPassword(model);

            if (passwordUpdated != null)
            {
                return Ok("Password updated successfully.");
            }
            else
            {
                return NotFound("User not found.");
            }
        }


        [HttpPost]

        public async Task<ActionResult<bool>> DeleteUser([FromHeader] string UserLoginId)
        {
            try
            {

                var userDelete = await _accountRepos.GetUser(UserLoginId);
                if (userDelete == null)
                {
                    return NotFound($"User Id ={UserLoginId} not found!");
                }
                return Ok(await _accountRepos.DeleteUser(UserLoginId));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Deleting!");

            }
        }

    }
}
