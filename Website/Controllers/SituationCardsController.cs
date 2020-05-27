/*
 * Group 6
 * Rasmus, Joseph, Tony and Frederik
 * Class type: ControllerBase
 * - ControllerBase class for the SituationCards API
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SituationCardsController : ControllerBase
    {
        private readonly GameContext _context;

        public SituationCardsController(GameContext context)
        {
            _context = context;
        }

        // GET: api/SituationCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SituationCard>>> GetSituationCard()
        {
            return await _context.SituationCard.ToListAsync();
        }

        // GET: api/SituationCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SituationCard>> GetSituationCard(int id)
        {
            var situationCard = await _context.SituationCard.FindAsync(id);

            if (situationCard == null)
            {
                return NotFound();
            }

            return situationCard;
        }

        // PUT: api/SituationCards/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSituationCard(int id, SituationCard situationCard)
        {
            if (id != situationCard.SituationCardID)
            {
                return BadRequest();
            }

            _context.Entry(situationCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SituationCardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SituationCards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SituationCard>> PostSituationCard(SituationCard situationCard)
        {
            _context.SituationCard.Add(situationCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSituationCard", new { id = situationCard.SituationCardID }, situationCard);
        }

        // DELETE: api/SituationCards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SituationCard>> DeleteSituationCard(int id)
        {
            var situationCard = await _context.SituationCard.FindAsync(id);
            if (situationCard == null)
            {
                return NotFound();
            }

            _context.SituationCard.Remove(situationCard);
            await _context.SaveChangesAsync();

            return situationCard;
        }

        private bool SituationCardExists(int id)
        {
            return _context.SituationCard.Any(e => e.SituationCardID == id);
        }
    }
}
