using System;

namespace PEC.Models
{
    public class CampanhaSaldoClienteBrinde
    {
        public int ID { get; set; }
        public int ID_CampanhaSaldoCliente { get; set; }
        public string ID_Brinde { get; set; }
        public DateTime DT_Brinde { get; set; }
        public int Qtde { get; set; }
        public string Pontos { get; set; }
    }
}
