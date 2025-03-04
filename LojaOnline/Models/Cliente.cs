using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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


        public static Cliente AdicionarCliente()
        {
            Console.WriteLine("Digite o Id do Cliente: ");
            int Id = Convert.ToInt32(Console.ReadLine());

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
                    }else
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

        public static async Task<Cliente> AcharClientePorID(int id)
        {
            string url = "https://localhost:7092/Cliente/AcharPorID//%7Bid%7D";

            using (HttpClient cliente = new HttpClient())
            {
                try
                {
                    HttpResponseMessage requestMessage = await cliente.GetAsync(url);

                    if (requestMessage.IsSuccessStatusCode)
                    {
                        string json = await requestMessage.Content.ReadAsStringAsync();
                        Cliente clienteID = JsonConvert.DeserializeObject<Cliente>(json) ?? new Cliente(-1, " ", " ", " ", " ");

                        return clienteID;
                    }
                    else
                    {
                        if (requestMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            Console.WriteLine("Cliente não Foi encontrado");
                            return new Cliente(-2, " ", " ", " ", " "); // Como posso fazer esse return de uma forma legal
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"ERRO: {ex.Message}");
                    return new Cliente(-3, " ", " ", " ", " ");
                }
            }

            return new Cliente(-4, " ", " ", " ", " ");
        }
    }
}