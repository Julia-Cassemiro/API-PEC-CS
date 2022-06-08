using System;

namespace PEC.Models
{
    public class Campanha
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool Fl_Ativo { get; set; }
        public DateTime DT_Criacao { get; set; }
        public DateTime DT_Alteracao { get; set; }
        public bool FL_Removido { get; set; }

    }
}
