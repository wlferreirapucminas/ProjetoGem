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
    public class MembroController : ControllerBase
    {
        private readonly GemDbContext _context;

        public MembroController(GemDbContext context)
        {
            _context = context;

            //if (_context.MembroItens.Count() == 0)
            //{
            //    _context.MembroItens.Add(new MembroItem { Nome = "Teste" });
            //    _context.SaveChanges();
            //}
        }
        // GET: api/Membro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembroItem>>> GetMembroItens()
        {
            return await _context.MembroItens.ToListAsync();
        }
        // GET: api/Membro
        [HttpGet("{id}")]
        public async Task<ActionResult<MembroItem>> GetMembroItem(int id)
        {
            var membroItem = await _context.MembroItens.FindAsync(id);

            if (membroItem == null)
            {
                return NotFound();
            }

            return membroItem;
        }
        // POST: api/Membro
        [HttpPost]
        public async Task<ActionResult<MembroItem>> PostMembroItem(MembroItem item)
        {
            _context.MembroItens.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMembroItem), new { id = item.Id }, item);
        }
        // PUT: api/Membro
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembroItem(int id, MembroItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/Membro
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembroItem(int id)
        {
            var membroItem = await _context.MembroItens.FindAsync(id);

            if (membroItem == null)
            {
                return NotFound();
            }

            _context.MembroItens.Remove(membroItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}