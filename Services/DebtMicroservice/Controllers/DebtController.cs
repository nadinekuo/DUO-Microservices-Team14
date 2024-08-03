using Microsoft.AspNetCore.Mvc;
using DebtMicroservice.Repository;
using DebtMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DebtMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtController : ControllerBase
    {
        private readonly IDebtRepository _repository;
        private readonly DebtContext _context;

        public DebtController(IDebtRepository repository, DebtContext context)
        {
            _repository = repository;
            _context = context;

            // Check if the database is empty
            if (!_context.Debt.Any())
            {
                // Add dummy data to the context
                _context.Debt.AddRange(new Debt(studentId: 12345, currTotal: 100.00m, interestRate: 1.0295m));
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Debt>>> GetAll()
        {
            var debt = await _repository.GetAllAsync();
            return Ok(debt);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Debt>> GetById(int id)
        {
            var debt = await _repository.GetByIdAsync(id);
            if (debt == null)
            {
                return NotFound();
            }
            return Ok(debt);
        }

        [HttpPost]
        public async Task<ActionResult<Debt>> Create(Debt debt)
        {
            if (debt == null)
            {
                return BadRequest();
            }
            await _repository.AddAsync(debt);
            return CreatedAtAction(nameof(GetById), new { id = debt.ID }, debt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Debt debt)
        {
            if (id != debt.ID)
            {
                return BadRequest();
            }

            var existingDebt = await _repository.GetByIdAsync(id);
            if (existingDebt == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(debt);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var debt = await _repository.GetByIdAsync(id);
            if (debt == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}