using System;

namespace PEC.Models
{
    public class PedidoCampanha
    {
        public int ID { get; set; }
        public int ID_CampanhaSaldoCliente { get; set; }
        public string ID_Cliente { get; set; }
        public Boolean FL_End_Repres { get; set; }
        public int CD_Status { get; set; }
    }
}
