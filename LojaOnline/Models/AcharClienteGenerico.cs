using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LojaOnline.Models
{
    /// <summary>
    /// Essa função ira fazer a procura dependendo do valor passado
    /// </summary>
    /// <typeparam name="T">Tipo da Variavel, podendo ser String/Inteiro</typeparam>
    public class AcharClienteGenerico<T>
    {
        public static async Task<Cliente> AcharCliente(T procura)
        {
            string url = $"https://localhost:7092/Cliente/AcharPorID/{procura}";

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