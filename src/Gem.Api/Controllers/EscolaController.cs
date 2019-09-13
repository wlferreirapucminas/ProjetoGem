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
    public class EscolaController : ControllerBase
    {
        private readonly GemDbContext _context;

        public EscolaController(GemDbContext context)
        {
            _context = context;

            //if (_context.EscolaItens.Count() == 0)
            //{
            //    _context.EscolaItens.Add(new EscolaItem { Nome = "Teste" });
            //    _context.SaveChanges();
            //}
        }
        // GET: api/Escola
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscolaItem>>> GetEscolaItens()
        {
            return await _context.EscolaItens.ToListAsync();
        }
        // GET: api/Escola
        [HttpGet("{id}")]
        public async Task<ActionResult<EscolaItem>> GetEscolaItem(int id)
        {
            var escolaItem = await _context.EscolaItens.FindAsync(id);

            if (escolaItem == null)
            {
                return NotFound();
            }

            return escolaItem;
        }
        // POST: api/Escola
        [HttpPost]
        public async Task<ActionResult<EscolaItem>> PostEscolaItem(EscolaItem item)
        {
            _context.EscolaItens.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEscolaItem), new { id = item.Id }, item);
        }
        // PUT: api/Escola
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEscolaItem(int id, EscolaItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/Escola
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEscolaItem(int id)
        {
            var escolaItem = await _context.EscolaItens.FindAsync(id);

            if (escolaItem == null)
            {
                return NotFound();
            }

            _context.EscolaItens.Remove(escolaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}