using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empresa.Models
{
    [Table("Empregados_PX")]
    public class Empregado
    {
        [Key]
        public int EmpId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Genero { get; set; }
        [Required]
        public string FotoUrl { get; set; }
        [ForeignKey("Departamento")]
        public int DepId { get; set; }  
        public Departamento Departamento { get; set; }  
    }
}
