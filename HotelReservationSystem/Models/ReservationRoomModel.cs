namespace HotelReservationSystem.Models
{
    public class ReservationRoomModel
    {
        public int Id { get; set; }

        public int Reservation_Id { get; set; }
        public ReservationModel Reservation { get; set; }

        public int Room_Id { get; set; }
        public RoomModel Room { get; set; }
    }

}
