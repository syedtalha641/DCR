﻿using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepos;
using Repository.Repos;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaleOrderController : ControllerBase
    {

        private readonly ISaleOrderRepos _saleOrderRepos;

        public SaleOrderController(ISaleOrderRepos saleOrderRepos)
        {
            _saleOrderRepos = saleOrderRepos;
        }


        [HttpPost]
        public async Task<ActionResult> GetSaleOrders()
        {

            try
            {
                return Ok(await _saleOrderRepos.GetSaleOrders());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }



        [HttpPost]
        public async Task<ActionResult<SalesOrder>> GetSaleOrder(int SaleOrderId)
        {
            try
            {
                var result = await _saleOrderRepos.GetSaleOrder(SaleOrderId);
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
        public async Task<ActionResult<SalesOrder>> CreateSaleOrder([FromBody] SaleOrderViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _saleOrderRepos.AddSaleOrder(model);
                return CreatedAtAction(nameof(GetSaleOrder), new { id = CreatedUser.CreateDate}, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<SalesOrder>> UpdateSaleOrder(int SaleOrderId, [FromBody] SaleOrderViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid Sale Order data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var UpdateSaleOrder = await _saleOrderRepos.UpdateSaleOrder(SaleOrderId, model);

                if (UpdateSaleOrder != null)
                {
                    return Ok(UpdateSaleOrder); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("Sale Order not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<SalesOrder>> DeleteSaleOrder([FromBody] int SaleOrderId)
        {
            try
            {
                if (SaleOrderId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _saleOrderRepos.DeleteSaleOrder(SaleOrderId);
                return CreatedAtAction(nameof(GetSaleOrder), new { id = CreatedUser.SalesOrderId}, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }



    }
}
