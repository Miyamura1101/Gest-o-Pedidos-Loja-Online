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
    public class Cliente
    {
        public Cliente(int id, string nome, string email, string telefone, string endereco)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }

        private int _id;
        private string _nome;
        private string _email;
        private string _telefone;
        private string _endereco;

        public int Id
        {
            get => _id;
            set => _id = value;
        }
        public string Nome
        {
            get => _nome;
            set => _nome = value;
        }
        public string Email
        {
            get => _email;
            set => _email = value;
        }
        public string Telefone
        {
            get => _telefone;
            set => _telefone = value;
        }
        public string Endereco
        {
            get => _endereco;
            set => _endereco = value;
        }

        public static void MostrarCliente(Cliente cliente)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Id ------- {cliente.Id}");
            Console.WriteLine($"Nome ----- {cliente.Nome}");
            Console.WriteLine($"Email ---- {cliente.Email}");
            Console.WriteLine($"Telefone - {cliente.Telefone}");
            Console.WriteLine($"Endereco - {cliente.Endereco}");
            Console.WriteLine("------------------------------");
        }
        public static Cliente AdicionarCliente()
        {
            try
            {
                int Id;
                do
                {
                    Console.WriteLine("Digite o Id do Cliente: ");
                    Id = Convert.ToInt32(Console.ReadLine());

                } while (Id <= 0);

                string Nome;
                do
                {
                    Console.WriteLine("Digite o nome do Cliente: ");
                    Nome = Console.ReadLine() ?? string.Empty;

                } while (string.IsNullOrEmpty(Nome));

                Console.Write("Digite o email do Cliente: ");
                string email = Console.ReadLine() ?? string.Empty;

                Console.Write("Digite o Telefone do Cliente: ");
                string telefone = Console.ReadLine() ?? string.Empty;

                Console.Write("Digite o Endereço do Cliente: ");
                string endereco = Console.ReadLine() ?? string.Empty;

                return new Cliente(Id, Nome, email, telefone, endereco);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"ERRo: {ex}");
                return null;
            }
        }
        public static async Task<List<Cliente>> ListarTodosCliente()
        {
            string url = "https://localhost:7092/Cliente/ListarTodosClientes";

            using (HttpClient cliente = new HttpClient())
            {
                try
                {
                    HttpResponseMessage resposta = await cliente.GetAsync(url);

                    if (resposta.IsSuccessStatusCode)
                    {
                        string json = await resposta.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Cliente>>(json) ?? new List<Cliente>();
                    }
                    else
                    {
                        Console.WriteLine("Não foi possivel fazer a conversão");
                        return new List<Cliente>();
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"ERRO: {ex.Message}");
                    return new List<Cliente>();
                }
            }
        }

        public static async Task<Cliente> AcharClientePorId(int id)
        {
            return await ClienteService.AcharCliente(id);
        }

        public static async Task<Cliente> AcharClientePorNome(string nome)
        {
           return await ClienteService.AcharCliente(nome);
        }
        
        public static async Task SalvarNoBanco(Cliente cliente)
        {
            string url = "https://localhost:7092/Cliente/AdicionarCliente";

            string Json = JsonConvert.SerializeObject(cliente);
            StringContent content = new StringContent(Json, Encoding.UTF8, "application/json");

            using (HttpClient clientProcura = new HttpClient())
            {
                try
                {
                    HttpResponseMessage resposta = await clientProcura.PostAsync(url, content);

                    if (resposta.IsSuccessStatusCode)
                    {
                        string respostaJson = await resposta.Content.ReadAsStringAsync();
                        Console.WriteLine("Clinete criado com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao enserir o Cliente no Banco de dados");
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"ERRO: {ex.Message}");
                }
            }
        }

        public static async Task AtualizarClienteNoBanco(int id, Cliente cliente)
        {
            string url = $"https://localhost:7092/AtualizarClienteId/{id}";

            string json = JsonConvert.SerializeObject(cliente);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage resposta = await client.PutAsync(url, content);

                    if (resposta.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Alteração realizada com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Não foi possivel realizar a alteração, pois não exite o id corespondente");
                    }

                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"ERRO: {ex.Message}");
                }
            }

        }
        public static async Task DeletarClientePorId(int id)
        {
            string url = $"https://localhost:7092/Cliente/Deletar/{id}";

            using (HttpClient cliente = new HttpClient())
            {
                try
                {
                    HttpResponseMessage resposta = await cliente.DeleteAsync(url);

                    if (resposta.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Cliente deletado com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Cliente não existe");
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"ERRO: {ex.Message}");
                }
            }
        }
    }
}