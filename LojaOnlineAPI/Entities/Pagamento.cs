using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaOnlineAPI.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        
        public bool Status { get; set; }
        public string MetodoPagamento { get; set; }
    }
}