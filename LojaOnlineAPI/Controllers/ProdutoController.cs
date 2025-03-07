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

        [HttpPatch("Desconto/{Id}")]
        public async Task<IActionResult> DescontoPorId(int Id, [FromBody] decimal Desconto)
        {
            var produto = await _context.Produtos.FindAsync(Id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            produto.Desconto = Desconto;

            produto.Preco = produto.Preco - (produto.Preco * (Desconto / 100));

            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Messagem = "Desconto atualizado con sucesso",
                PrecoNovo = produto.Preco
            });
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