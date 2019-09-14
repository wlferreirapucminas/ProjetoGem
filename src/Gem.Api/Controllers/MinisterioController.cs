using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GemApi.Models;

namespace GemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinisterioController : ControllerBase
    {
        private readonly GemDbContext _context;

        public MinisterioController(GemDbContext context)
        {
            _context = context;

            //if (_context.MinisterioItens.Count() == 0)
            //{
            //    _context.MinisterioItens.Add(new MinisterioItem { Nome = "Teste" });
            //    _context.SaveChanges();
            //}
        }
        // GET: api/Ministerio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MinisterioItem>>> GetMinisterioItens()
        {
            return await _context.MinisterioItens.ToListAsync();
        }
        // GET: api/Ministerio
        [HttpGet("{id}")]
        public async Task<ActionResult<MinisterioItem>> GetMinisterioItem(int id)
        {
            var ministerioItem = await _context.MinisterioItens.FindAsync(id);

            if (ministerioItem == null)
            {
                return NotFound();
            }

            return ministerioItem;
        }
        // POST: api/Ministerio
        [HttpPost]
        public async Task<ActionResult<MinisterioItem>> PostMinisterioItem(MinisterioItem item)
        {
            _context.MinisterioItens.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMinisterioItem), new { id = item.Id }, item);
        }
        // PUT: api/Ministerio
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMinisterioItem(int id, MinisterioItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/Ministerio
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMinisterioItem(int id)
        {
            var ministerioItem = await _context.MinisterioItens.FindAsync(id);

            if (ministerioItem == null)
            {
                return NotFound();
            }

            _context.MinisterioItens.Remove(ministerioItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}