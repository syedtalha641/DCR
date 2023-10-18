using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepos;
using Repository.Repos;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchrepos _branchrepos;

        public BranchController(IBranchrepos branchrepos)
        {
            _branchrepos = branchrepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetBranches()
        {

            try
            {
                return Ok(await _branchrepos.GetBranches());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }


        [HttpPost]
        public async Task<ActionResult<Branch>> GetBranch(int BranchId)
        {
            try
            {
                var result = await _branchrepos.GetBranch(BranchId);
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
        public async Task<ActionResult<Branch>> CreateBranch([FromBody] BranchViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _branchrepos.AddBranch(model);
                return CreatedAtAction(nameof(GetBranch), new { id = CreatedUser.Name }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> UpdateBranch(int BranchId, [FromBody] BranchViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid customer data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var updatedBranch = await _branchrepos.UpdateBranch(BranchId, model);

                if (updatedBranch != null)
                {
                    return Ok(updatedBranch); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("Branch not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<Branch>> DeleteBranch([FromBody] int BranchId)
        {
            try
            {
                if (BranchId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _branchrepos.DeleteBranch(BranchId);
                return CreatedAtAction(nameof(GetBranch), new { id = CreatedUser.BranchId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
