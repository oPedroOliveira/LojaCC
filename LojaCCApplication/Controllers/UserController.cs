using LojaCCDomain.Models;
using LojaCCInfrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaCCApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly LojaCCApplicationContext _context;

        public UserController(LojaCCApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Login(User user)
        {
            if (user == null)
            {
                return NotFound("Usuário inválido.");
            }

            var result = await _context.User.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (user == null)
            {
                return NotFound("Ops... Não foi encontrado nenhum usuário!");
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            if (id == null)
            {
                return NotFound("Id inválido.");
            }

            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound("Ops... Não foi encontrado nenhum cliente com o Id informado!");
            }

            return user;
        }
    }
}
