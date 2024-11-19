using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaCCDomain.Models;
using LojaCCInfrastructure.Data;

namespace LojaCCApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly LojaCCApplicationContext _context;

        public ClienteController(LojaCCApplicationContext context)
        {
            _context = context;
        }

        // GET: Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            return await _context.Cliente.OrderBy(c => c.Nome).ToListAsync();
        }

        // GET: Cliente/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            
            if (cliente == null)
            {
                return NotFound();
            }

            await _context
                .Entry(cliente)
                .Collection(c => c.Pedidos)
                .LoadAsync();

            return cliente;
        }

        // GET: Cliente/Create
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.ClienteId))
                {
                    return Conflict();
                }
            }

            return CreatedAtAction("GetCliente", new { id = cliente.ClienteId }, cliente);
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Cliente>> PostClienteSafe([Bind("ClienteId,Nome,Email,CPF,CEP,Cidade,UF,Logradouro")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            throw new NotImplementedException();
        }*/

        // GET: Cliente/Edit/5
        [HttpPut]
        public async Task<IActionResult> PutCliente(string id, Cliente cliente)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            var result = await _context.Cliente.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Update(cliente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutClienteSafe(string id, [Bind("ClienteId,Nome,Email,CPF,CEP,Cidade,UF,Logradouro")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            throw new NotImplementedException();
        }*/

        // GET: Cliente/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(string id)
        {
            return _context.Cliente.Any(e => e.ClienteId == id);
        }
    }
}
