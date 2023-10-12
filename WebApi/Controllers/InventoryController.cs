using DCR.Helper.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepos _inventoryRepos;

        private readonly IAuthenticationService _authenticationService;

        public InventoryController(IInventoryRepos inventoryRepos, IAuthenticationService authenticationService)
        {
            _inventoryRepos = inventoryRepos;
            _authenticationService = authenticationService;
        }



        [HttpPost]
        public async Task<ActionResult<CustomerInventoryViewModel>> GetCustomer()
        {
            try
            {
                return Ok(await _inventoryRepos.GetCustomerInventoryQuery());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");
            }

            
        }


    }
}
