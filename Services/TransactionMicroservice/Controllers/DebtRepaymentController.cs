using Microsoft.AspNetCore.Mvc;
using TransactionMicroservice.Repository;
using TransactionMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using TransactionMicroservice.Services;
namespace TransactionMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtRepaymentController : ControllerBase
    {
        private readonly IDebtRepaymentService _debtRepaymentService;

        public DebtRepaymentController(IDebtRepaymentService debtRepaymentService)
        {
            _debtRepaymentService = debtRepaymentService;
        }
        /// <summary>
        /// Gets all debt repayments.
        /// </summary>
        /// <returns>A list of all debt repayments.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DebtRepayment>>> GetAll()
        {
            var debtRepayments = await _debtRepaymentService.GetAllDebtRepaymentsAsync();
            return Ok(debtRepayments);
        }

        /// <summary>
        /// Gets a specific debt repayment by ID.
        /// </summary>
        /// <param name="id">The ID of the debt repayment.</param>
        /// <returns>The debt repayment with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DebtRepayment>> GetById(int id)
        {
            var debtRepayment = await _debtRepaymentService.GetDebtRepaymentByIdAsync(id);
            if (debtRepayment == null)
            {
                return NotFound();
            }
            return Ok(debtRepayment);
        }

        ///<summary>
        /// Creates a new debt repayment.
        /// </summary>
        /// <param name="debtRepayment">The debt repayment to create.</param>
        /// <returns>The created debt repayment.</returns>
        [HttpPost]
        public async Task<ActionResult<DebtRepayment>> Create([FromBody] DebtRepayment debtRepayment)
        {
            if (debtRepayment == null)
            {
                return BadRequest("Invalid debtRepayment data.");
            }
            var paymentResult = await _debtRepaymentService.CreateDebtRepaymentAsync(debtRepayment);
            return CreatedAtAction(nameof(GetById), new { id = paymentResult.ID }, paymentResult);
        }

        /// <summary>
        /// Updates an existing debt repayment by ID.
        /// </summary>
        /// <param name="id">The ID of the debt repayment to update.</param>
        /// <param name="debtRepayment">The updated debt repayment information.</param>
        /// <returns>An ActionResult indicating the result of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DebtRepayment debtRepayment)
        {
            var result = await _debtRepaymentService.UpdateDebtRepaymentAsync(id, debtRepayment);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a debt repayment by ID.
        /// </summary>
        /// <param name="id">The ID of the debt repayment to delete.</param>
        /// <returns>An ActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _debtRepaymentService.DeleteDebtRepaymentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}