using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InstaFoodAPI.Models;

namespace InstaFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogBooksController : ControllerBase
    {
        private readonly InstaFoodDBContext _context;

        public LogBooksController(InstaFoodDBContext context)
        {
            _context = context;
        }

        // GET: api/LogBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogBook>>> GetLogBooks()
        {
            return await _context.LogBooks.ToListAsync();
        }

        // GET: api/LogBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogBook>> GetLogBook(int id)
        {
            var logBook = await _context.LogBooks.FindAsync(id);

            if (logBook == null)
            {
                return NotFound();
            }

            return logBook;
        }

        // PUT: api/LogBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogBook(int id, LogBook logBook)
        {
            if (id != logBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(logBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogBookExists(id))
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

        // POST: api/LogBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LogBook>> PostLogBook(LogBook logBook)
        {
            _context.LogBooks.Add(logBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogBook", new { id = logBook.Id }, logBook);
        }

        // DELETE: api/LogBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogBook(int id)
        {
            var logBook = await _context.LogBooks.FindAsync(id);
            if (logBook == null)
            {
                return NotFound();
            }

            _context.LogBooks.Remove(logBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogBookExists(int id)
        {
            return _context.LogBooks.Any(e => e.Id == id);
        }
    }
}
