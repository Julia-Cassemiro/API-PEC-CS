using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PEC.Models
{
    [Table("Usuario", Schema = "Acesso")]
    public partial class Usuario
    {

        /// <summary>
        /// Identificação do Usuário.
        /// </summary>
        [Key]
        [Column("ID_Usuario")]
        public int IdUsuario { get; set; }
        /// <summary>
        /// Nome do Usuário.
        /// </summary>
        [Column("NM_Usuario")]
        [StringLength(128)]
        public string NmUsuario { get; set; }
        /// <summary>
        /// Nome da Skin do Usuário.
        /// </summary>
        [Column("NM_Skin")]
        [StringLength(128)]
        public string NmSkin { get; set; }
        /// <summary>
        /// E-mail do Usuário.
        /// </summary>
        [Column("DS_Email")]
        [StringLength(256)]
        public string DsEmail { get; set; }
        [Column("NM_Senha")]
        [StringLength(20)]
        public string NmSenha { get; set; }
        [Column("ID_Acesso")]
        public int? IdAcesso { get; set; }
        [Column("DT_FINAL_FERIAS", TypeName = "date")]
        public DateTime? DtFinalFerias { get; set; }
        [Column("ID_Func")]
        public int? IdFunc { get; set; }
        [Column("NR_CPF")]
        [StringLength(20)]
        public string NrCpf { get; set; }

    }

    [Table("Usuario_Repres", Schema = "PEC")]
    public class Usuarios_Repres
    {
        [Key]
        public int ID_Repres { get; set; }
        public int ID_Usuario { get; set; }
        public string NM_Senha { get; set; }
        public string CPF_CNPJ { get; set; }
    }

    [Table("Menu", Schema = "PEC")]
    public class Menu
    {
        [Key]
        public int ID_Menu { get; set; }
        public int ID_Sistema { get; set; }
        public string NM_Descricao { get; set; }
        public int NR_Menu_Pai { get; set; }
        public string NM_Url { get; set; }
        public int ID_Menu_Tipo { get; set; }

    }

    [Table("Menu_Usuario", Schema = "PEC")]
    public class Menu_Usuario
    {
        [Key]
        public int ID_Sistema { get; set; }
        public int ID_Menu { get; set; }
        public int ID_Usuario { get; set; }
    }

    public class Get
    {
        [Key]
        public string NR_Url { get; set; }
        public int ID_Usuario { get; set; }
    }
}