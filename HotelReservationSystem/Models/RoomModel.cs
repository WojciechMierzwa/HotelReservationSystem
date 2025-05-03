using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.Models
{
    [Table("Room")]
    public class RoomModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public bool AirConditioned { get; set; }

        [Required]
        public int Beds { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [ForeignKey("Type")]
        public int Type_Id { get; set; }

        [ForeignKey("Hotel")]
        public int Hotel_Id { get; set; }

        public TypeModel? Type { get; set; }
        public HotelModel? Hotel { get; set; }
    }
}