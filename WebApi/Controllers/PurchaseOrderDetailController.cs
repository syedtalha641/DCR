﻿using DAL.EntityModels;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepos;
using Repository.Repos;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseOrderDetailController : ControllerBase
    {
        private readonly IPurchaseOrderDetailRepos _purchaseOrderDetailRepos;

        public PurchaseOrderDetailController(IPurchaseOrderDetailRepos purchaseOrderDetailRepos)
        {
            _purchaseOrderDetailRepos = purchaseOrderDetailRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetPurchaseOrderDetails()
        {

            try
            {
                return Ok(await _purchaseOrderDetailRepos.GetPurchaseOrderDetails());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrderDetail>> GetPurchaseOrderDetail(int PurchaseOrderDetailId)
        {
            try
            {
                var result = await _purchaseOrderDetailRepos.GetPurchaseOrderDetail(PurchaseOrderDetailId);
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
        public async Task<ActionResult<PurchaseOrderDetail>> CreatePurchaseOrderDetail([FromBody] PurchaseOrderDetailViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _purchaseOrderDetailRepos.AddPurchaseOrderDetail(model);
                return CreatedAtAction(nameof(GetPurchaseOrderDetail), new { id = CreatedUser.ProductId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<PurchaseOrderDetail>> UpdatePurchaseOrderDetail(int PurchaseOrderDetailId, [FromBody] PurchaseOrderDetailViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid customer data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var updatedPurchaseOrderDetail = await _purchaseOrderDetailRepos.UpdatePurchaseOrderDetail(PurchaseOrderDetailId, model);

                if (updatedPurchaseOrderDetail != null)
                {
                    return Ok(updatedPurchaseOrderDetail); // Return 200 OK with the updated customer
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
        public async Task<ActionResult<PurchaseOrderDetail>> DeletePayment([FromBody] int PurchaseOrderDetailId)
        {
            try
            {
                if (PurchaseOrderDetailId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _purchaseOrderDetailRepos.DeletePurchaseOrderDetail(PurchaseOrderDetailId);
                return CreatedAtAction(nameof(GetPurchaseOrderDetail), new { id = CreatedUser.ProductId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
