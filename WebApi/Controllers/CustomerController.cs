using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepos _customerRepos;

        public CustomerController(ICustomerRepos customerRepos)
        {
            _customerRepos = customerRepos;
        }



        [HttpGet]
        public async Task<ActionResult> GetCustomers()
        {

            try
            {
                return Ok(await _customerRepos.GetCustomers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }



        [HttpPost("{CustomerId}")]
        public async Task<ActionResult<Customer>> GetCustomer(int CustomerId)
        {
            try
            {
                var result = await _customerRepos.GetCustomer(CustomerId);
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
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CustomerViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _customerRepos.AddCustomer(model);
                return CreatedAtAction(nameof(GetCustomer), new { id = CreatedUser.CustomerName }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost("")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int CustomerId, [FromBody] CustomerViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid customer data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var updatedCustomer = await _customerRepos.UpdateCustomer(CustomerId, model);
                
                if (updatedCustomer != null)
                {
                    return Ok(updatedCustomer); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("Customer not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost("")]
        public async Task<ActionResult<Customer>> DeleteCustomer([FromBody] int CustomerId)
        {
            try
            {
                if (CustomerId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _customerRepos.DeleteCustomer(CustomerId);
                return CreatedAtAction(nameof(GetCustomer), new { id = CreatedUser.CustomerId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
