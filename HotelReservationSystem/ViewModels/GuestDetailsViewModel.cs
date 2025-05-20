using HotelReservationSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.ViewModels
{
    public class GuestDetailsViewModel
    {
        public List<RoomModel> SelectedRooms { get; set; } = new List<RoomModel>();

        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }

        public decimal? TotalCost { get; set; }
    }

}
