using DAL.EntityModels;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepos;
using Repository.Repos;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseOrderLineController : ControllerBase
    {
        private readonly IPurchaseOrderline _purchaseOrderline;

        public PurchaseOrderLineController(IPurchaseOrderline purchaseOrderline)
        {
            _purchaseOrderline = purchaseOrderline;
        }

        [HttpPost]
        public async Task<ActionResult> GetPurchaseOrderLines()
        {

            try
            {
                return Ok(await _purchaseOrderline.GetPurchaseOrderLines());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrderLine>> GetPurchaseOrderLine(int PurchaseOrderLineId)
        {
            try
            {
                var result = await _purchaseOrderline.GetPurchaseOrderLine(PurchaseOrderLineId);
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
        public async Task<ActionResult<PurchaseOrderLine>> CreatePurchaseOrderLine([FromBody] PurchaseOrderLineViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _purchaseOrderline.AddPurchaseOrderLine(model);
                return CreatedAtAction(nameof(GetPurchaseOrderLine), new { id = CreatedUser.ProductId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<PurchaseOrderLine>> UpdatePurchaseOrderLine(int PurchaseOrderLineId, [FromBody] PurchaseOrderLineViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid customer data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var updatedPurchaseOrderLine = await _purchaseOrderline.UpdatePurchaseOrderLine(PurchaseOrderLineId, model);

                if (updatedPurchaseOrderLine != null)
                {
                    return Ok(updatedPurchaseOrderLine); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("!Not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrderLine>> DeletePurchaseOrderLine([FromBody] int PurchaseOrderLineId)
        {
            try
            {
                if (PurchaseOrderLineId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _purchaseOrderline.DeletePurchaseOrderLine(PurchaseOrderLineId);
                return CreatedAtAction(nameof(GetPurchaseOrderLine), new { id = CreatedUser.ProductId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
