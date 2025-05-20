namespace HotelReservationSystem.ViewModels
{
    public class ReservationViewModel
    {
        public int RoomId { get; set; }
        public string GuestName { get; set; }
        public string GuestSurname { get; set; }
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public int Beds { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
