using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{

    public class Location
    {

        [Key]
        public int LocationID { get; set; }


        [Display(Name = "Nombre de la Localidad")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? LocationName { get; set; }
        // el signo de pregunta quiere decir que acepta valores nulos o espacios vacios


        [Display(Name = "Pais")]
        public int CountryID { get; set; }

        [Display(Name = "Pais")]
        public virtual Country? Countries { get; set; }
        public virtual ICollection<Partner>? Partners { get; set; }



    }
}


