using HotelReservationSystem.Models;

namespace HotelReservationSystem.ViewModels
{
    public class RoomFilterViewModel
    {
        public IEnumerable<RoomModel> Rooms { get; set; }

        public int? Beds { get; set; }
        public int? TypeId { get; set; }
        public int? HotelId { get; set; }

        public IEnumerable<TypeModel> Types { get; set; }
        public IEnumerable<HotelModel> Hotels { get; set; }
    }

}
