using Microsoft.AspNetCore.Mvc;
using StudentProductMicroservice.Factories;
using StudentProductMicroservice.Models;
using StudentProductMicroservice.Services;

namespace StudentProductMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrantController : ControllerBase
    {
        private readonly IGrantService _grantService;

        public IGrantFactory _grantFactory;
        /// <summary>
        /// Retrieves all grants.
        /// </summary>
        /// <returns>A list of grants.</returns>
        public GrantController(IGrantService grantService, IGrantFactory grantFactory)
        {
            _grantService = grantService;
            _grantFactory = grantFactory;
        }
        /// <summary>
        /// Retrieves all grants.
        /// </summary>
        /// <returns>A list of grants.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grant>>> GetAll()
        {
            return Ok(await _grantService.GetAllGrantsAsync());
        }

        /// <summary>
        /// Retrieves a specific grant by ID.
        /// </summary>
        /// <param name="id">The ID of the grant to retrieve.</param>
        /// <returns>The grant with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Grant>> GetById(int id)
        {
            var grant = await _grantService.GetGrantByIdAsync(id);
            if (grant == null)
            {
                return NotFound();
            }
            return Ok(grant);
        }


        /// <summary>
        /// Creates a new grant.
        /// </summary>
        /// <param name="request">The grant creation request.</param>
        /// <returns>The created grant.</returns>
        [HttpPost]
        public async Task<ActionResult<Grant>> Create([FromBody] GrantCreationRequest request)
        {
            try
            {
                var grant = _grantFactory.CreateGrant(request.GrantType, request.StudentId, request.MonthlyAmount, request.StartDate, request.EndDate);
                var createdGrant = await _grantService.CreateGrantAsync(grant);
                var payout = new Payout(grant.StudentId, 1, DateTime.UtcNow, grant.MonthlyAmount);
                //TODO: change productID, create a converter from type to typeId.
                //TODO: change the date to the next 20th of the month.
                try
                {
                    // Send the Payout object to the transaction microservice
                    string response = await _grantService.SendPayoutToTransactionMicroservice(payout);
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine($"Error sending payout to transaction microservice: {ex.Message}");
                    throw;
                }

                return CreatedAtAction(nameof(GetById), new { id = createdGrant.ID }, createdGrant);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing grant.
        /// </summary>
        /// <param name="id">The ID of the grant to update.</param>
        /// <param name="request">The update request details.</param>
        /// <returns>A status indicating the result of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GrantCreationRequest request)
        {
            try
            {
                var grantToUpdate = _grantFactory.CreateGrant(request.GrantType, request.StudentId, request.MonthlyAmount, request.StartDate, request.EndDate);
                grantToUpdate.ID = id;
                var success = await _grantService.UpdateGrantAsync(id, grantToUpdate);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Deletes a grant by ID.
        /// </summary>
        /// <param name="id">The ID of the grant to delete.</param>
        /// <returns>A status indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _grantService.DeleteGrantAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
