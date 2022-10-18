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
}