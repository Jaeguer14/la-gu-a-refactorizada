

using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Section
    {
        [Key]
        public int SectionID { get; set; }
    
        [Display(Name = "Nombre de la seccion")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? SectionName { get; set; }
        

        public virtual ICollection<Movie>? Movies { get; set; }
    }
}