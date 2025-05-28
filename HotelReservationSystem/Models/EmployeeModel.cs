using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystem.Models
{
    [Table("Employee")]
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [ForeignKey("Hotel")]
        public int Hotel_Id { get; set; }

        public HotelModel? Hotel { get; set; }  // optional navigation property

    }
}
