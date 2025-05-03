using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystem.Models
{
    [Table("Type")]
    public class TypeModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal BaseCost { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal HighSeasonCost { get; set; }
    }
}
