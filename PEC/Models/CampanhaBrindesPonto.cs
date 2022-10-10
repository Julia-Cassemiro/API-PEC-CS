using System;

namespace PEC.Models
{
    public class CampanhaBrindesPonto
    {
        public int ID { get; set; }
        public int ID_Campanha { get; set; }
        public string ID_Produto { get; set; }
        public bool FL_Ativo { get; set; }
        public DateTime DT_Criacao { get; set; }
        public double Pontos { get; set; }
    }
}
