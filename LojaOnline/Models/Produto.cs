using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LojaOnline.Services;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace LojaOnline.Models
{
    public class Produto
    {

        public Produto(int id, string nome, string descricao, decimal preco, decimal desconto, int estoque)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Desconto = desconto;
            Estoque = estoque;
        }

        private int _id;
        private string _nome;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        private string _descricao;
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        private decimal _preco;
        public decimal Preco
        {
            get { return _preco; }
            set { _preco = value; }
        }
        private decimal _desconto;
        public decimal Desconto
        {
            get { return _desconto; }
            set { _desconto = value; }
        }
        private int _estoque;
        public int Estoque
        {
            get { return _estoque; }
            set { _estoque = value; }
        }

        public static Cliente AdicionarProduto()
        {

        }
        
    }
}