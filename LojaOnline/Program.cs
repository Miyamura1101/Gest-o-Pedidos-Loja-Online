using System.Threading.Tasks;
using LojaOnline.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class Program
{
    public static async Task Main(string[] args)
    {
        string p = " ";

        do
        {
            Cliente cliente = Cliente.AdicionarCliente();

            await Cliente.SalvarNoBanco(cliente);

            p = Console.ReadLine();

        } while (p == "y");

    }
}