

using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class RentalDetailTemp{

        [Key]
        public int RentalDetailTempID { get; set; }


        public int MovieID { get; set; }


        [Display(Name = "Nombre de la Pelicula")]
        public string? MovieName { get; set; }


    }

}