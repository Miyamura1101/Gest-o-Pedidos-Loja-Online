using LojaOnline.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

string op;

do
{
    Cliente cliente = Cliente.AdicionarCliente();

    string caminhoArquivo = "Arquivos/ListaCliente.json";
    
    List<Cliente> clientes = new List<Cliente>();

    if (File.Exists(caminhoArquivo))
    {
        string output = File.ReadAllText(caminhoArquivo);

        if (!string.IsNullOrWhiteSpace(output))
        {
            try
            {
                clientes = JsonConvert.DeserializeObject<List<Cliente>>(output) ?? new List<Cliente>();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"ERRO {ex.Message}");
                clientes = new List<Cliente>();
            }
        }
    }

    clientes.Add(cliente);

    string serialization = JsonConvert.SerializeObject(clientes, Formatting.Indented);

    File.WriteAllText(caminhoArquivo, serialization);

    Console.WriteLine(serialization);

    op = Console.ReadLine().ToLower();
} while (op == "y");

