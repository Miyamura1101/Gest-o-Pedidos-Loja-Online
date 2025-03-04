using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaOnlineAPI.Context;
using LojaOnlineAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LojaOnlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly LojaContext _context;
        private readonly string _caminhoArquivoListaCliente = "E:/C#/Gestão-Pedidos-Loja-Online/LojaOnline/Arquivos/ListaCliente.json";

        public ClienteController(LojaContext context)
        {
            _context = context;
        }

        [HttpPost("AdicionarCliente")]
        public IActionResult AdicionarClienteTabela()
        {
            Console.WriteLine("Método AdicionarClienteTabela foi chamado!");

            if (!System.IO.File.Exists(_caminhoArquivoListaCliente))
            {
                return NotFound("Arquivo Json não encontrado");
            }
            try
            {
                string json = System.IO.File.ReadAllText(_caminhoArquivoListaCliente);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return BadRequest("Não a nada no Arquivo Json");
                }

                List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(json) ?? new List<Cliente>();

                if (clientes.Count == 0)
                {
                    return BadRequest("Não foi encontrado nenhum valor valido no arquivo JSON");
                }

                foreach (var cliente in clientes)
                {
                    if (!_context.Clientes.Any(c => c.Id == cliente.Id))
                    {
                        _context.Clientes.Add(cliente);
                    }
                }

                _context.SaveChanges();

                Console.WriteLine("Clientes adicionados ao banco de dados com sucesso!");

                return Ok("Cliente importados com Sucesso");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("AcharPorID")]
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