using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaOnlineAPI.Context;
using Microsoft.AspNetCore.Mvc;
using LojaOnlineAPI.Entities;

namespace LojaOnlineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly LojaContext _context;

        public ProdutoController(LojaContext context)
        {
            _context = context;
        }

        [HttpPost("AdicionarProduto")]
        public IActionResult AdicionarPodruto(Produto produto)
        {
            _context.Add(produto);
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpGet("ListarTodosProdutos")]
        public IActionResult ListarItensEstoque()
        {
            var produtos = _context.Produtos.ToList();

            if (produtos == null)
            {
                return NotFound("Não foi possivel checar o estoque");
            }

            return Ok(produtos);
        }

        [HttpGet("ListarProdutoPorId/{id}")]
        public IActionResult ListarItensEstoquePorId(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound("Não foi possivel checar o estoque");
            }

            return Ok(produto);
        }

        [HttpDelete("RetirarProdutoEstoqueId/{Id}")]
        public IActionResult RetiraProdutoEstoqueId(int Id)
        {
            var produto = _context.Produtos.Find(Id);

            if (produto == null)
            {
                return NotFound("Não foi achado no Estoque");
            }

            _context.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }


    }
}