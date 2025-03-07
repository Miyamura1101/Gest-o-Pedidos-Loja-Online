using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaOnlineAPI.Context;
using LojaOnlineAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LojaOnlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly LojaContext _context;
        // private readonly string _caminhoArquivoListaCliente = "E:/C#/Gestão-Pedidos-Loja-Online/LojaOnline/Arquivos/ListaCliente.json";

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

        [HttpGet("ListarTodosClientes")]
        public IActionResult ListarTodosClientes()
        {
            var _cliente = _context.Clientes.ToList();

            if (_cliente == null)
            {
                return NotFound();
            }

            return Ok(_cliente);
        }

        [HttpGet("AcharPorId/{id}")]
        public IActionResult AcharCliente(int id)
        {
            var _cliente = _context.Clientes.Find(id);

            if (_cliente == null)
            {
                return NotFound();
            }

            return Ok(_cliente);
        }

        [HttpGet("AcharPorNome/{nome}")]
        public IActionResult AcharPorNome(string nome)
        {
            var _cliente = _context.Clientes.FirstOrDefault(c => c.Nome == nome);

            if (_cliente == null)
            {
                return NotFound("Não a Cliente com esse nome");
            }

            return Ok(_cliente);
        }

        [HttpPut("AtualizarClienteId/{id}")]
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

        [HttpDelete("Deletar/{id}")]
        public IActionResult DeletarCliente(int id)
        {
            var _cliente = _context.Clientes.Find(id);

            if (_cliente == null)
            {
                return NotFound("Não existe cliente com esse Id");
            }

            _context.Clientes.Remove(_cliente); ;
            _context.SaveChanges();

            return Ok("Cliente deletado com sucesso");
        }
    }
}