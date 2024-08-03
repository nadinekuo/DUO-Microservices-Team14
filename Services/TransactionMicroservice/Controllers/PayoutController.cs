using Microsoft.AspNetCore.Mvc;
using TransactionMicroservice.Models;
using TransactionMicroservice.Services;
namespace TransactionMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayoutController : ControllerBase
    {
        private readonly IPayoutService _payoutService;

        public PayoutController(IPayoutService payoutService)
        {
            _payoutService = payoutService;
        }

        /// <summary>
        /// Gets all payouts.
        /// </summary>
        /// <returns>A list of payouts.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payout>>> GetAll()
        {
            var payouts = await _payoutService.GetAllPayoutsAsync();
            return Ok(payouts);
        }

        /// <summary>
        /// Gets a payout by ID.
        /// </summary>
        /// <param name="id">The ID of the payout.</param>
        /// <returns>The payout with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Payout>> GetById(int id)
        {
            var payout = await _payoutService.GetPayoutByIdAsync(id);
            if (payout == null)
            {
                return NotFound();
            }
            return Ok(payout);
        }

        /// <summary>
        /// Creates a new payout.
        /// </summary>
        /// <param name="payout">The payout to create.</param>
        /// <returns>A newly created payout.</returns>
        [HttpPost]
        public async Task<ActionResult<Payout>> Create([FromBody] Payout payout)
        {
            if (payout == null)
            {
                return BadRequest("Invalid payout data.");
            }
            var paymentResult = await _payoutService.CreatePayoutAsync(payout);
            return CreatedAtAction(nameof(GetById), new { id = paymentResult.ID }, paymentResult);
        }

        /// <summary>
        /// Updates an existing payout by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the payout to update.</param>
        /// <param name="payout">The updated payout information.</param>
        /// <returns>An ActionResult indicating the result of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Payout payout)
        {
            var result = await _payoutService.UpdatePayoutAsync(id, payout);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a payout by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the payout to delete.</param>
        /// <returns>An ActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _payoutService.DeletePayoutAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }


        /// <summary>
        /// Sends a payout using the provided transaction details.
        /// </summary>
        /// <param name="transactionDetails">The transaction details to use for sending the payout.</param>
        /// <returns>An IActionResult indicating the result of the send operation.</returns>
        [HttpPost("stub")]
        public async Task<IActionResult> SendPayout([FromBody] TransactionDetails transactionDetails)
        {
            var result = await _payoutService.SendPaymentAsync(transactionDetails);
            return Ok(result);
        }

    }
}