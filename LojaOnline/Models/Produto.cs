using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
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

        public static Produto AdicionarProduto()
        {
            try
            {
                Console.WriteLine("Digite o id do produto: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Digite o nome do produto: ");
                string nome = Console.ReadLine() ?? string.Empty;

                Console.WriteLine("Descrição do Produto: ");
                string descricao = Console.ReadLine() ?? string.Empty;

                Console.WriteLine("Insira o valor do produto: ");
                decimal preco = Convert.ToDecimal(Console.Read());

                char desc;
                do
                {
                    Console.WriteLine("Gostaria de adicionar algum desconto (y/s): ");
                    desc = Convert.ToChar((Console.ReadLine() ?? string.Empty).ToUpper());

                } while (desc != 'Y' || desc != 'N');

                decimal desconto;
                if (desc == 'Y')
                {
                    Console.WriteLine("Adicione o desconto: ");
                    desconto = Convert.ToDecimal(Console.ReadLine());
                }
                else
                {
                    desconto = 0M;
                }

                int estoque;
                do
                {
                    Console.WriteLine("Insira a quantidade de produtos do estoque: ");
                    estoque = Convert.ToInt32(Console.ReadLine());

                } while (estoque < 0);

                return new Produto(id, nome, descricao, preco, desconto, estoque);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public static void MostrarProduto(Produto produto)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Id -------- {produto._id}");
            Console.WriteLine($"Nome ------ {produto._nome}");
            Console.WriteLine($"Preço ----- {produto._preco}");
            Console.WriteLine($"Desconto -- {produto._desconto}");
            Console.WriteLine($"Descrição - {produto._descricao}");
            Console.WriteLine($"Estoque --- {produto._estoque}");
            Console.WriteLine("------------------------------");
        }

        public static async Task SalvarNoBanco(Produto produto)
        {
            string url = "https://localhost:7092/Produto/AdicionarProduto";

            string Json = JsonConvert.SerializeObject(produto);
            StringContent content = new StringContent(Json, Encoding.UTF8, "application/json");

            using (HttpClient produtoProcura = new HttpClient())
            {
                try
                {
                    HttpResponseMessage resposta = await produtoProcura.PostAsync(url, content);

                    if (resposta.IsSuccessStatusCode)
                    {
                        string respostaJson = await resposta.Content.ReadAsStringAsync();
                        Console.WriteLine("O Produto Salvo com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao salvar o cliente no Banco de dados");
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }
    }
}