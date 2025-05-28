using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystem.Models
{
    [Table("ReservationRoom")]
    public class ReservationRoomModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Reservation")]
        public int Reservation_Id { get; set; }

        [ForeignKey("Room")]
        public int Room_Id { get; set; }

        public ReservationModel? Reservation { get; set; }
        public RoomModel? Room { get; set; }
    }
}
