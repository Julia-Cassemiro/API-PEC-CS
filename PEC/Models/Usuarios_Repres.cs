using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PEC.Models
{
    [Table("Usuario", Schema = "Acesso")]
    public class Usuarios
    {
        [Key]
        public int ID_Usuario { get; set; }
        public string NM_Usuario { get; set; }
        public string NM_Senha { get; set; }
    }

    [Table("Usuario_Repres", Schema = "SIA")]
    public class Usuarios_Repres
    {
        [Key]
        public int ID_Repres { get; set; }
        public int ID_Usuario { get; set; }
        public string NM_Senha { get; set; }
        public string CPF_CNPJ { get; set; }
    }

    [Table("Menu", Schema = "Acesso")]
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

    [Table("Menu_Usuario", Schema = "Acesso")]
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