

using System.ComponentModel.DataAnnotations;

namespace AppMovie.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }

        [Display(Name = "Fecha de Alquiler")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }

        [Display(Name = "Socio")]
        public int PartnerID { get; set; }
        
        [Display(Name = "Socio")]
        public virtual Partner? Partner { get; set; }

        public virtual ICollection<RentalDetail>? RentalDetails { get; set; }


    }
}