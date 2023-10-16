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
    public class ContactPersonController : ControllerBase
    {

        private readonly IContactPersonRepos _contactPersonRepos;

        public ContactPersonController(IContactPersonRepos contactPersonRepos)
        {
            _contactPersonRepos = contactPersonRepos;
        }



        [HttpPost]
        public async Task<ActionResult> GetPersons()
        {

            try
            {
                return Ok(await _contactPersonRepos.GetPersons());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }



        [HttpPost]
        public async Task<ActionResult<ContactPerson>> GetPerson(int ContactPersonId)
        {
            try
            {
                var result = await _contactPersonRepos.GetPerson(ContactPersonId);
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
        public async Task<ActionResult<ContactPerson>> CreatePerson([FromBody] ContactPersonViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _contactPersonRepos.AddPerson(model);
                return CreatedAtAction(nameof(GetPerson), new { id = CreatedUser.FirstName }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult<ContactPerson>> UpdatePerson(int ContactPersonId, [FromBody] ContactPersonViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid Person data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var UpdatePerson = await _contactPersonRepos.UpdatePerson(ContactPersonId, model);

                if (UpdatePerson != null)
                {
                    return Ok(UpdatePerson); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("Person not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<ContactPerson>> DeletePerson([FromBody] int ContactPersonId)
        {
            try
            {
                if (ContactPersonId == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _contactPersonRepos.DeletePerson(ContactPersonId);
                return CreatedAtAction(nameof(GetPerson), new { id = CreatedUser.ContactPersonId }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
