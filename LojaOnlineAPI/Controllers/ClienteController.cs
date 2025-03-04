using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaOnlineAPI.Context;
using LojaOnlineAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LojaOnlineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly LojaContext _context;

        public ClienteController(LojaContext context)
        {
            _context = context;
        }

        [HttpPost("AdicionarCliente")]
        public IActionResult AdicionarClienteTabela(Cliente cliente)
        {
            _context.Add(cliente);
            _context.SaveChanges();
            return Ok(cliente);
        }

        [HttpGet("")]
        public IActionResult AcharClientePeloId(int Id)
        {
            var cliente = _context.Clientes.Find(Id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

    }
}