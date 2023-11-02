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
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRolesRepos _userRolesRepos;

        public UserRoleController(IUserRolesRepos userRolesRepos)
        {
            _userRolesRepos = userRolesRepos;
        }


        [HttpPost]
        public async Task<ActionResult> GetUserRoles()
        {

            try
            {
                return Ok(await _userRolesRepos.GetUserRoles());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }


        [HttpPost]
        public async Task<ActionResult<UserRole>> GetUserRole(int UserRoleId)
        {
            try
            {
                var result = await _userRolesRepos.GetUserRole(UserRoleId);
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
        public async Task<ActionResult<UserRole>> CreateUserRole([FromBody] UserRoleViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _userRolesRepos.AddUserRole(model);
                return CreatedAtAction(nameof(GetUserRole), new { id = CreatedUser.UserId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<UserRole>> UpdateUserRole(int UserRoleId, [FromBody] UserRoleViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid User Role data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var UpdateUserRole = await _userRolesRepos.UpdateUserRole(UserRoleId, model);

                if (UpdateUserRole != null)
                {
                    return Ok(UpdateUserRole); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("User Role not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<UserRole>> DeleteUserRole([FromBody] int UserRoleId)
        {
            try
            {
                if (UserRoleId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _userRolesRepos.DeleteUserRole(UserRoleId);
                return CreatedAtAction(nameof(GetUserRole), new { id = CreatedUser.UserRoleId}, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
