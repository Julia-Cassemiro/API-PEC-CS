using System;

namespace PEC.Models
{
    public class CampanhaProduto
    {
        public int ID { get; set; }
        public int ID_InvestCampanha { get; set; }
        public string Nome { get; set; }
        public bool Fl_Ativo { get; set; }
        public DateTime DT_Criacao { get; set; }
        public DateTime DT_Alteracao { get; set; }
        public bool FL_Removido { get; set; }
    }
}
