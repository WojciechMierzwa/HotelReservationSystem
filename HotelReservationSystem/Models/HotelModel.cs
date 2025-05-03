using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystem.Models
{
    [Table("Hotel")]
    public class HotelModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Adress1 { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }
    }
}
