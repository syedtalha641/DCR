using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepos;
using Repository.Repos;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepos _userProfile;


        public UserProfileController(IUserProfileRepos userProfile)
        {
            _userProfile = userProfile;
        }

        [HttpPost]
        public async Task<ActionResult> GetUserProfiles()
        {

            try
            {
                return Ok(await _userProfile.GetUserprofiles());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }



        [HttpPost]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int UserProfileId)
        {
            try
            {
                var result = await _userProfile.GetUserProfile(UserProfileId);
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



        [HttpPost]
        public async Task<ActionResult<UserProfile>> CreateUserProfile([FromBody] UserProfileViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _userProfile.AddUserProfile(model);
                return CreatedAtAction(nameof(GetUserProfile), new { id = CreatedUser.FirstName}, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<UserProfile>> UpdateUserProfile(int UserProfileId, [FromBody] UserProfileViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid UserProfile data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var UpdateUserProfile = await _userProfile.UpdateUserProfile(UserProfileId, model);

                if (UpdateUserProfile != null)
                {
                    return Ok(UpdateUserProfile); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("user Profile not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<UserProfile>> DeleteUserProfile([FromBody] int UserProfileId)
        {
            try
            {
                if (UserProfileId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _userProfile.DeleteUserProfile(UserProfileId);
                return CreatedAtAction(nameof(GetUserProfile), new { id = CreatedUser.UserProfileId}, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
