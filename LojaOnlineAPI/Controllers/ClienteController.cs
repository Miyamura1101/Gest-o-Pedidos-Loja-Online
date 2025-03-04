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
                    return NotFound("Não a nada no Arquivo Json");
                }

                List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(json) ?? new List<Cliente>();

                if (clientes.Count == 0)
                {
                    return NotFound("Não foi encontrado nenhum valor valido no arquivo JSON");
                }

                foreach (var cliente in clientes)
                {
                    if (!_context.Clientes.Any(c => c.Id == cliente.Id))
                    {
                        _context.Clientes.Add(cliente);
                    }
                    else
                    {
                        Console.WriteLine($"Não foi possivel a inserção do cliente com o id {cliente.Id}, pois foi encontrado um cliente com o mesmo id");
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
            var _cliente = _context.Clientes.Find(Id);

            if (_cliente == null)
            {
                return NotFound();
            }

            return Ok(_cliente);
        }

        [HttpGet("AcharPorNome")]
        public IActionResult AcharPorNome(string Nome)
        {
            var _cliente = _context.Clientes.Find(Nome);

            if (_cliente == null)
            {
                return NotFound("Não a Cliente com esse nome");
            }

            return Ok(_cliente);
        }

        [HttpPut("AtualizarClienteID/{id}")]
        public IActionResult Atualizar(int id, Cliente cliente)
        {
            var _cliente = _context.Clientes.Find(id);

            if (_cliente == null)
            {
                return NotFound("Id de modificação não exite");
            }

            _cliente.Telefone = cliente.Telefone;
            _cliente.Endereco = cliente.Endereco;
            _cliente.Email = cliente.Email;
            _cliente.Nome = cliente.Nome;

            _context.Clientes.Update(_cliente);
            _context.SaveChanges();

            return Ok("Alteração realizadas com sucesso");
        }

        [HttpDelete("Deletar")]
        public IActionResult DeletarCliente(int id)
        {
            var _cliente = _context.Clientes.Find(id);

            if (_cliente == null)
            {
                return NotFound("Não existe cliente com esse Id");
            }

            return Ok("Cliente deletado com sucesso");
        }
    }
}