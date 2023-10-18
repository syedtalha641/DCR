﻿using DAL.EntityModels;
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
    public class SaleOrderLineController : ControllerBase
    {
        private readonly ISaleOrderLineRepos _saleOrderLineRepos;

        public SaleOrderLineController(ISaleOrderLineRepos saleOrderLineRepos)
        {
            _saleOrderLineRepos = saleOrderLineRepos;
        }


        [HttpPost]
        public async Task<ActionResult> GetSaleOrderLines()
        {

            try
            {
                return Ok(await _saleOrderLineRepos.GetSaleOrderLines());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }



        [HttpPost]
        public async Task<ActionResult<SaleOrderLine>> GetSaleOrderLine(int SaleOrderLineId)
        {
            try
            {
                var result = await _saleOrderLineRepos.GetSaleOrderLine(SaleOrderLineId);
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
        public async Task<ActionResult<SaleOrderLine>> CreateSaleOrderLine([FromBody] SaleOrderLineViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _saleOrderLineRepos.AddSaleOrderLine(model);
                return CreatedAtAction(nameof(GetSaleOrderLine), new { id = CreatedUser.ProductId}, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<SaleOrderLine>> UpdateSaleOrderLine(int SaleOrderLineId, [FromBody] SaleOrderLineViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid Sale Order Line data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var UpdateSaleOrderLine = await _saleOrderLineRepos.UpdateSaleOrderLine(SaleOrderLineId, model);

                if (UpdateSaleOrderLine != null)
                {
                    return Ok(UpdateSaleOrderLine); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("Sale Order Line Data not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<SaleOrderLine>> DeleteSaleOrderLine([FromBody] int SaleOrderLineId)
        {
            try
            {
                if (SaleOrderLineId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _saleOrderLineRepos.DeleteSaleOrderline(SaleOrderLineId);
                return CreatedAtAction(nameof(GetSaleOrderLine), new { id = CreatedUser.SalesOrderLineId}, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }



    }
}
