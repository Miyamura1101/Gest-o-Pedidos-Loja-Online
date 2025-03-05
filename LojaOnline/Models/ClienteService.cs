using System;
using System.Net.Http;
using System.Threading.Tasks;
using LojaOnline.Models;
using Newtonsoft.Json;

namespace LojaOnline.Services
{
    /// <summary>
    /// Utilizada para a ação de procura de clientes
    /// </summary>
    public class ClienteService
    {
        /// <summary>
        /// Busca um cliente pelo ID ou outro critério genérico.
        /// </summary>
        /// <typeparam name="T">Tipo do parâmetro de busca (int ou string)</typeparam>
        /// <param name="procura">Valor usado para a busca</param>
        /// <returns>Retorna um Cliente encontrado ou um objeto padrão caso não exista</returns>
        public static async Task<Cliente> AcharCliente<T>(T procura)
        {
            string url = $"https://localhost:7092/Cliente/AcharPorID/{procura}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Cliente>(json) ?? ClientePadrao(-1);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("Cliente não encontrado.");
                        return ClientePadrao(-2);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    return ClientePadrao(-3);
                }

                return ClientePadrao(-4);
            }
        }

        /// <summary>
        /// Retorna um Cliente padrão com um ID específico para indicar erros.
        /// </summary>
        private static Cliente ClientePadrao(int id)
        {
            return new Cliente(id, "Desconhecido", " ", " ", " ");
        }
    }
}
