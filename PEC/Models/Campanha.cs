using System;

namespace PEC.Models
{
    public class Campanha
    {
        public int ID { get; set; }
        public int ID_Tipo_Campanha { get; set; }
        public string Nome { get; set; }
        public bool Fl_Ativo { get; set; }
        public DateTime DT_Criacao { get; set; }

    }
}
