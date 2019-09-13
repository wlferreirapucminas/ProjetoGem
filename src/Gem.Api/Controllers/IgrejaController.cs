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
    public class IgrejaController : ControllerBase
    {
        private readonly GemDbContext _context;

        public IgrejaController(GemDbContext context)
        {
            _context = context;

            //if (_context.IgrejaItens.Count() == 0)
            //{
            //    _context.IgrejaItens.Add(new IgrejaItem { Nome = "Teste" });
            //    _context.SaveChanges();
            //}
        }
        // GET: api/Igreja
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IgrejaItem>>> GetIgrejaItens()
        {
            return await _context.IgrejaItens.ToListAsync();
        }
        // GET: api/Igreja
        [HttpGet("{id}")]
        public async Task<ActionResult<IgrejaItem>> GetIgrejaItem(int id)
        {
            var igrejaItem = await _context.IgrejaItens.FindAsync(id);

            if (igrejaItem == null)
            {
                return NotFound();
            }

            return igrejaItem;
        }
        // POST: api/Igreja
        [HttpPost]
        public async Task<ActionResult<IgrejaItem>> PostIgrejaItem(IgrejaItem item)
        {
            _context.IgrejaItens.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIgrejaItem), new { id = item.Id }, item);
        }
        // PUT: api/Igreja
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIgrejaItem(int id, IgrejaItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/Igreja
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIgrejaItem(int id)
        {
            var igrejaItem = await _context.IgrejaItens.FindAsync(id);

            if (igrejaItem == null)
            {
                return NotFound();
            }

            _context.IgrejaItens.Remove(igrejaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}