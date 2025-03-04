using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}