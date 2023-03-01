﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PEC.Context
{
    public partial class Especialidade
    {
        [Key]
        [Column("ID_Especialidade")]
        public int IdEspecialidade { get; set; }
        [Required]
        [Column("NM_Especialidade")]
        [StringLength(60)]
        public string NmEspecialidade { get; set; }
        [Column("FL_Geral")]
        public bool? FlGeral { get; set; }
        [Column("FL_Secao")]
        public bool? FlSecao { get; set; }
        [Column("FL_Cobrar_Consulta")]
        public bool? FlCobrarConsulta { get; set; }
        [Column("ID_Exame_Sessao")]
        public int? IdExameSessao { get; set; }
        [Column("CD_Amb")]
        [StringLength(10)]
        public string CdAmb { get; set; }
        [Column("CBO")]
        [StringLength(10)]
        public string Cbo { get; set; }
        [Column("FL_Ativa")]
        public bool? FlAtiva { get; set; }
        [Column("FL_Masculino")]
        public bool? FlMasculino { get; set; }
        [Column("FL_Feminino")]
        public bool? FlFeminino { get; set; }
        public int? Idade { get; set; }
        public int? Idadeate { get; set; }
        [Column("Codigo_Receita")]
        [StringLength(50)]
        public string CodigoReceita { get; set; }
    }
}