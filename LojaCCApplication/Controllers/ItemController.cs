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
    public class ItemController : ControllerBase
    {
        private readonly LojaCCApplicationContext _context;

        public ItemController(LojaCCApplicationContext context)
        {
            _context = context;
        }

        // GET: Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItem()
        {
            return await _context.Item.OrderBy(i => i.Nome).ToListAsync();
        }

        // GET: Item/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(string id)
        {
            if (id == null)
            {
                return NotFound("Ops... O id não pode ser nulo.");
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemId == id);
            
            if (item == null)
            {
                return NotFound("Ops... O não foi encontrado nenhum item cadastrado com este id.");
            }

            return item;
        }

        // GET: Item/Create
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            if (ItemExists(item.ItemId))
            {
                return Conflict("Já existe um item com este mesmo ID cadastrado");
            }
            
            _context.Item.Add(item);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
            }

            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Item>> PostItemSafe([Bind("ItemId,Nome,Email,CPF,CEP,Cidade,UF,Logradouro")] Item Item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            throw new NotImplementedException();
        }*/

        // GET: Item/Edit/5
        [HttpPut]
        public async Task<IActionResult> PutItem(string id, Item item)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != item.ItemId)
            {
                return BadRequest();
            }

            var result = await _context.Item.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Update(item);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutItemSafe(string id, [Bind("ItemId,Nome,Email,CPF,CEP,Cidade,UF,Logradouro")] Item Item)
        {
            if (id != Item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(Item.ItemId))
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

        // GET: Item/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemId == id);
            
            if (item == null)
            {
                return NotFound();
            }

            _context.Item.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
