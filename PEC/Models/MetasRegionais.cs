﻿namespace PEC.Models
{
    public class MetasRegionais
    {
        public int ID { get; set; }
        public int ID_Metas { get; set; }
        public int ID_Meta_Regional { get; set; }
        
        public int ID_Grupo_Produto { get; set; }
        public string ID_Regional { get; set; }

        public string Qtde { get; set; }
        public double Valor { get; set; }
    }
}
