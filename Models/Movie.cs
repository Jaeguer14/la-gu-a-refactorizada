


using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Movie
    {
        [Key]

        public int MovieID { get; set; }


        [Display(Name = "Nombre de la Película")]
        [Required(ErrorMessage = "Este valor es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El largo maximo es de {0} caracteres.")]
        public string? MovieName { get; set; }

        
        [Display(Name = "Descripcion de la Película")]
        public string? MovieDescription { get; set; }


        [Display(Name = "Fecha de lanzamineto")]
        [DataType(DataType.Date)]
        public DateTime MovieDate { get; set; }


        [Display(Name = "Seccion")]
        public int SectionID { get; set; }

        [Display(Name = "Seccion")]
        public virtual Section? Section { get; set; }


        [Display(Name = "Género")]
        public int GenderID { get; set; }

        [Display(Name = "Género")]
        public virtual Gender? Gender { get; set; }

       
        [Display(Name = "Productora")]
        public int ProducerID { get; set; }

        
        public virtual Producer? Producer { get; set; }

        public bool EstaAlquilada  { get; set; }

        public virtual ICollection<RentalDetail>? RentalDetails  { get; set; }


}

}