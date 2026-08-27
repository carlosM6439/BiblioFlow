using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioFlow.API.Data;
using BiblioFlow.API.Models;

namespace BiblioFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibrosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            return await _context.Libros.ToListAsync();
        }

        // POST: api/libros/reservar/5 -> Descuenta stock en PostgreSQL
        [HttpPost("reservar/{id}")]
        public async Task<IActionResult> ReservarLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound(new { mensaje = "El libro no existe." });
            if (libro.StockDisponible <= 0) return BadRequest(new { mensaje = "Sin stock." });

            libro.StockDisponible -= 1;
            await _context.SaveChangesAsync(); // PERSISTE EN POSTGRESQL

            return Ok(new { mensaje = "Reserva guardada en PostgreSQL", stock = libro.StockDisponible });
        }

        // PUT: api/libros/devolver/5 -> Incrementa stock en PostgreSQL
        [HttpPut("devolver/{id}")]
        public async Task<IActionResult> DevolverLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound(new { mensaje = "El libro no existe." });

            if (libro.StockDisponible < libro.StockTotal)
            {
                libro.StockDisponible += 1;
                await _context.SaveChangesAsync(); // PERSISTE EN POSTGRESQL
            }

            return Ok(new { mensaje = "Devolución guardada en PostgreSQL", stock = libro.StockDisponible });
        }
    }
}