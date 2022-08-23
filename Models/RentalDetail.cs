

using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailID { get; set; }
        
        public int RentalID { get; set;}
        public virtual Rental? Rental { get; set; }

        public int MovieID { get; set; }
        public virtual Movie? Movie { get; set; }

        [Display(Name = "Nombre de la Pelicula")]        
        public string? MovieName { get; set; }
       
    }
}