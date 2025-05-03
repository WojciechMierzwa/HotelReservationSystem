using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystem.Models
{
    [Table("Reservation")]
    public class ReservationModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

        [ForeignKey("Guest")]
        public int Guest_Id { get; set; }

        public string FullName => $"{Guest?.Name} {Guest?.Surname}".Trim();

        public GuestModel? Guest { get; set; }
    }
}
