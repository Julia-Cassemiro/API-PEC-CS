using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEC.Models
{
    public class MetasRepreClientes
    {
        public int ID_Meta_Regional { get; set; }
        public int ID_Repres { get; set; }
        public int ID_Grupo_Cliente { get; set; }
        public string Qtde { get; set; }
        public double Valor { get; set; }
    }
}
