using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Gender
    {
        [Key]
        public int GenderID { get; set; }


        [Display(Name = "Nombre del Genero")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? GenderName { get; set; }


        public virtual ICollection<Movie>? Movies { get; set; }
    }
}