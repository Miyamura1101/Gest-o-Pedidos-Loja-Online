using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaOnlineAPI.Entities
{
    public class Pedido
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        
        public DateTime DataPedido { get; set; }
        public decimal Total { get; set; }
        public bool? Status { get; set; }
    }
}