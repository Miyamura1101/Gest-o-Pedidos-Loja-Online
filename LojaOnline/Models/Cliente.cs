using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        //public abstract T AcharClienteGenerico<T>(int id);

        public static void MostrarCliente(Cliente cliente)
        {
            Console.WriteLine($"Id ------- {cliente.Id}");
            Console.WriteLine($"Nome ----- {cliente.Nome}");
            Console.WriteLine($"Email ---- {cliente.Email}");
            Console.WriteLine($"Telefone - {cliente.Telefone}");
            Console.WriteLine($"Endereco - {cliente.Endereco}");
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
                        Console.WriteLine(respostaJson);
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
            string url = $"https://localhost:7092/AtualizarClienteID/{id}";

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
        public static async Task<bool> DeletarClientePorId(int id)
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
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Cliente não existe");
                        return false;
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"ERRO: {ex.Message}");
                    return false;
                }
            }
        }
    }
}