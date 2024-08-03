using Microsoft.AspNetCore.Mvc;
using IdentityMicroservice.Repository;
using IdentityMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityRepository _repository;
        private readonly IdentityContext _context;

        public IdentityController(IIdentityRepository repository, IdentityContext context)
        {
            _repository = repository;
            _context = context;

            // Check if the database is empty
            // if (!_context.Identity.Any())
            // {
                // Add dummy data to the context
                _context.Identity.AddRange(new Identity(
                   bsn: 123456,
                   higherEducationStartDate: new DateTime(2022, 1, 1),
                   grantStartDate: new DateTime(2022, 2, 1),
                   bankAccount: "1234567890",
                   livesWithParents: true
                ));
                _context.SaveChanges();
            // }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Identity>>> GetAll()
        {
            var identities = await _repository.GetAllAsync();
            return Ok(identities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Identity>> GetById(int id)
        {
            var identity = await _repository.GetByIdAsync(id);
            if (identity == null)
            {
                return NotFound();
            }
            return Ok(identity);
        }

        [HttpPost]
        public async Task<ActionResult<Identity>> Create(Identity identity)
        {
            if (identity == null)
            {
                return BadRequest();
            }
            await _repository.AddAsync(identity);
            return CreatedAtAction(nameof(GetById), new { id = identity.ID }, identity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Identity identity)
        {
            if (id != identity.ID)
            {
                return BadRequest();
            }

            var existingIdentity = await _repository.GetByIdAsync(id);
            if (existingIdentity == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(identity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var identity = await _repository.GetByIdAsync(id);
            if (identity == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
