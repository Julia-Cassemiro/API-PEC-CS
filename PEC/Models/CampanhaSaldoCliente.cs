using System;

namespace PEC.Models
{
    public class CampanhaSaldoCliente
    {
        public int ID { get; set; }
        public int ID_Campanha { get; set; }
        public string ID_Cliente { get; set; }
        public DateTime DT_Inicio { get; set; }
        public int Saldo { get; set; }
        public int Saldo_Apropriado { get; set; }
        public int Saldo_Disponivel { get; set; }

    }
}
