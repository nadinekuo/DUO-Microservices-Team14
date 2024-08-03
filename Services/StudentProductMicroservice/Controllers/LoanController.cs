using Microsoft.AspNetCore.Mvc;
using StudentProductMicroservice.Models;
using StudentProductMicroservice.Services;

namespace StudentProductMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public ILoanFactory _loanFactory;
        public LoanController(ILoanService loanService, ILoanFactory loanFactory)
        {
            _loanService = loanService;
            _loanFactory = loanFactory;
        }

        /// <summary>
        /// Retrieves all loans.
        /// </summary>
        /// <returns>A list of loans.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetAll()
        {
            return Ok(await _loanService.GetAllLoansAsync());
        }

        /// <summary>
        /// Retrieves a specific loan by ID
        /// </summary>
        /// <param name="id">The identifier of the loan.</param>
        /// <returns>The loan with the specified identifier.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetById(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);
        }

        /// <summary>
        /// Creates a new loan.
        /// </summary>
        /// <param name="request">The loan creation request data.</param>
        /// <returns>A newly created loan.</returns>
        [HttpPost]
        public async Task<ActionResult<Loan>> Create([FromBody] LoanCreationRequest request)
        {
            try
            {
                var loan = _loanFactory.CreateLoan(request.LoanType, request.StudentId, request.MonthlyAmount, request.StartDate, request.EndDate);
                var createdLoan = await _loanService.CreateLoanAsync(loan);
                var payout = new Payout(createdLoan.StudentId, 1, DateTime.UtcNow, createdLoan.MonthlyAmount);
                //TODO: change productID, create a converter from type to typeId.
                //TODO: change the date to the next 20th of the month.
                try
                {
                    // Send the Payout object to the transaction microservice
                    string response = await _loanService.SendPayoutToTransactionMicroservice(payout);
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine($"Error sending payout to transaction microservice: {ex.Message}");
                    throw;
                }
                return CreatedAtAction(nameof(GetById), new { id = createdLoan.ID }, createdLoan);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing loan by ID.
        /// </summary>
        /// <param name="id">The identifier of the loan to update.</param>
        /// <param name="request">The request data for updating the loan.</param>
        /// <returns>An ActionResult indicating the result of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoanCreationRequest request)
        {
            try
            {
                var loanToUpdate = _loanFactory.CreateLoan(request.LoanType, request.StudentId, request.MonthlyAmount, request.StartDate, request.EndDate);
                loanToUpdate.ID = id;
                var success = await _loanService.UpdateLoanAsync(id, loanToUpdate);

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
        /// Deletes a loan by ID.
        /// </summary>
        /// <param name="id">The identifier of the loan to delete.</param>
        /// <returns>An ActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _loanService.DeleteLoanAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
