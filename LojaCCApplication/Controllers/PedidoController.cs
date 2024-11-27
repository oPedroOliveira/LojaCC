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
    public class PedidoController : ControllerBase
    {
        private readonly LojaCCApplicationContext _context;

        public PedidoController(LojaCCApplicationContext context)
        {
            _context = context;
        }

        // GET: Pedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedido()
        {
            return await _context.Pedido.OrderBy(p => p.PedidoId).ToListAsync();
        }

        // GET: Pedido/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(p => p.PedidoId == id);
            
            if (pedido == null)
            {
                return NotFound();
            }

            await _context
                .Entry(pedido)
                .Collection(p => p.ItensPedido)
                .LoadAsync();

            if (pedido.ItensPedido.Count > 0 && pedido.ItensPedido != null)
            {
                foreach (var itemPedido in pedido.ItensPedido)
                {
                    _context.Item.Where(i => i.ItemId == itemPedido.ItemId).FirstAsync();
                }
            }

            return pedido;
        }

        // POST: Pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            _context.Pedido.Add(pedido);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PedidoExists(pedido.PedidoId))
                {
                    return Conflict();
                }
            }

            return CreatedAtAction("GetPedido", new { id = pedido.PedidoId }, pedido);
        }

        // POST: Pedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Pedido>> PostPedidoSafe([Bind("PedidoId,Nome,Email,CPF,CEP,Cidade,UF,Logradouro")] Pedido Pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            throw new NotImplementedException();
        }*/

        // GET: Pedido/Edit/5
        [HttpPut]
        public async Task<IActionResult> PutPedido(string id, Pedido pedido)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != pedido.PedidoId)
            {
                return BadRequest();
            }

            var result = await _context.Pedido.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Update(pedido);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutPedidoSafe(string id, [Bind("PedidoId,Nome,Email,CPF,CEP,Cidade,UF,Logradouro")] Pedido Pedido)
        {
            if (id != Pedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(Pedido.PedidoId))
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

        // GET: Pedido/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(p => p.PedidoId == id);
            
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedido.Remove(pedido);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(string id)
        {
            return _context.Pedido.Any(e => e.PedidoId == id);
        }
    }
}
