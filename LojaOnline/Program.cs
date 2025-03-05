using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using LojaOnline.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class Program
{
    public static async Task Main(string[] args)
    {
        bool saida_Programa = true;
        int Opição = 0;

        do
        {
            bool saida = true;
            Console.WriteLine("1 - Clientes");
            Console.WriteLine("2 - Sair Programa");
            Console.WriteLine("Opição: ");
            Opição = Convert.ToInt32(Console.ReadLine());

            switch (Opição)
            {
                case 1:
                    do
                    {
                        Console.WriteLine("1 - Listagem de Clientes");
                        Console.WriteLine("2 - Adicionar Cliente");
                        Console.WriteLine("3 - AChar Cliente Por Id");
                        Console.WriteLine("4 - AChar Cliente Por Nome"); // Checar depois
                        Console.WriteLine("5 - Deletar Cliente");
                        Console.WriteLine("6 - Atualualizar Cliente"); // Checar depois
                        Console.WriteLine("7 - Sair da Organização Cliente");
                        Console.WriteLine("Opição: ");
                        Opição = Convert.ToInt32(Console.ReadLine());
                        switch (Opição)
                        {
                            case 1:

                                List<Cliente> clientes = await Cliente.ListarTodosCliente();

                                foreach (var cliente in clientes)
                                {
                                    Cliente.MostrarCliente(cliente);
                                }

                                break;
                            case 2:

                                Cliente clienteAdicionar = Cliente.AdicionarCliente();
                                await Cliente.SalvarNoBanco(clienteAdicionar);

                                break;
                            case 3:

                                Console.WriteLine("Informe o id do Cliente que deseja procurar: ");
                                int id_Procura = Convert.ToInt32(Console.ReadLine());

                                Cliente clienteProcuraId = await Cliente.AcharClientePorId(id_Procura); // await para aguardar o resultado antes de utiliza-lo
                                Cliente.MostrarCliente(clienteProcuraId);

                                break;
                            case 4:

                                Console.WriteLine("Informe o Nome do Clinete que deseja procurar: ");
                                string nome_Procura = Console.ReadLine() ?? " ";

                                Cliente clienteProcuraNome = await Cliente.AcharClientePorNome(nome_Procura);
                                Cliente.MostrarCliente(clienteProcuraNome);

                                break;
                            case 5:

                                Console.WriteLine("Digite o Id do Cliente que deseja Deletar: ");
                                int id_Deletar = Convert.ToInt32(Console.ReadLine());

                                await Cliente.DeletarClientePorId(id_Deletar);

                                break;
                            case 6:

                                Console.WriteLine("Digite o Id do Cliente que deseja Atualizar: ");
                                int id_Atualizar = Convert.ToInt32(Console.ReadLine());

                                Cliente cliente_Atualizar = Cliente.AdicionarCliente(); // Altera essa parte, pois está muito mau configurada

                                await Cliente.AtualizarClienteNoBanco(id_Atualizar, cliente_Atualizar);
                                break;
                            case 7:

                                saida = false;

                                break;
                            default:

                                break;
                        }
                    } while (saida);

                    break;
                case 2:

                    saida_Programa = false;
                
                    break;
                default:
                    break;
            }

        } while (saida_Programa);

    }
}