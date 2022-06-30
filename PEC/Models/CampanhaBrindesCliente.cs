using System;

namespace PEC.Models
{
    public class CampanhaBrindesCliente
    {
        public int ID { get; set; }
        public int ID_Campanha { get; set; }
        public string ID_Brinde { get; set; }
        public int ID_Cliente { get; set; }
        public DateTime DT_Brinde { get; set; }
        public int Qtde { get; set; }
        public string Motivo { get; set; }
    }
}
