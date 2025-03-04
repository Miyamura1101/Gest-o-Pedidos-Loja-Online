using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaOnline.Models
{
    public class Cliente
    {
        public Cliente()
        {

        }
        public Cliente(int id, string nome, string email, string telefone, string endereco)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }


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

        public static implicit operator Cliente(List<Cliente> v)
        {
            throw new NotImplementedException();
        }
    }
}