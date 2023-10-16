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
    public class IMEIController : ControllerBase
    {
        private readonly IIMEIRepos _iMEIRepos;

        public IMEIController(IIMEIRepos iMEIRepos)
        {
            _iMEIRepos = iMEIRepos;
        }


        [HttpPost]
        public async Task<ActionResult> GetIMEIs()
        {

            try
            {
                return Ok(await _iMEIRepos.GetIMEIs());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<Imei>> GetIMEI(int IMEIId)
        {
            try
            {
                var result = await _iMEIRepos.GetIMEI(IMEIId);
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
        public async Task<ActionResult<Imei>> CreateIMEI([FromBody] IMEIViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _iMEIRepos.AddIMEI(model);
                return CreatedAtAction(nameof(GetIMEI), new { id = CreatedUser.ImeiNumber}, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<Imei>> UpdateImei(int IMEIId, [FromBody] IMEIViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid IMEI data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var UpdateImei = await _iMEIRepos.UpdateIMEI(IMEIId, model);

                if (UpdateImei != null)
                {
                    return Ok(UpdateImei); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("IMEI not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<Imei>> DeleteIMEI([FromBody] int IMEIId)
        {
            try
            {
                if (IMEIId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _iMEIRepos.DeleteIMEI(IMEIId);
                return CreatedAtAction(nameof(GetIMEI), new { id = CreatedUser.ImeiId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }


    }
}
