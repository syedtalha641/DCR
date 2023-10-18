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
    public class ReceiveController : ControllerBase
    {

        private readonly IReceiveRepos _receiveRepos;

        public ReceiveController(IReceiveRepos receiveRepos)
        {
            _receiveRepos = receiveRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetReceives()
        {

            try
            {
                return Ok(await _receiveRepos.GetReceives());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<Receive>> GetReceive(int ReceiveId)
        {
            try
            {
                var result = await _receiveRepos.GetReceive(ReceiveId);
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
        public async Task<ActionResult<Receive>> CreateReceive([FromBody] ReceiveViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _receiveRepos.AddReceive(model);
                return CreatedAtAction(nameof(GetReceive), new { id = CreatedUser.DistributorId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<Receive>> UpdateReceive(int ReceiveId, [FromBody] ReceiveViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid customer data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var updatedReceive = await _receiveRepos.UpdateReceive(ReceiveId, model);

                if (updatedReceive != null)
                {
                    return Ok(updatedReceive); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound(" not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<Receive>> DeleteReceive([FromBody] int ReceiveId)
        {
            try
            {
                if (ReceiveId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _receiveRepos.DeleteReceive(ReceiveId);
                return CreatedAtAction(nameof(GetReceive), new { id = CreatedUser.ReceiveId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
