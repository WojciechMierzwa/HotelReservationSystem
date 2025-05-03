using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationSystem.Models
{
    [Table("Guest")]  // Określenie nazwy tabeli w bazie danych
    public class GuestModel
    {
        [Key]  // Określenie, że GuestId jest kluczem głównym
        [Column("Id")]
        public int GuestId { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "Pole Login jest wymagane.")]
        [MaxLength(50, ErrorMessage = "Login nie może mieć więcej niż 50 znaków.")]
        public string Login { get; set; }

        [DisplayName("Hasło")]
        [Required(ErrorMessage = "Pole Hasło jest wymagane.")]
        [MaxLength(255, ErrorMessage = "Hasło nie może mieć więcej niż 255 znaków.")]
        public string Password { get; set; }

        [DisplayName("Imię")]
        [Required(ErrorMessage = "Pole Imię jest wymagane.")]
        [MaxLength(30, ErrorMessage = "Imię nie może mieć więcej niż 30 znaków.")]
        public string Name { get; set; }

        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Pole Nazwisko jest wymagane.")]
        [MaxLength(50, ErrorMessage = "Nazwisko nie może mieć więcej niż 50 znaków.")]
        public string Surname { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Pole Email jest wymagane.")]
        [MaxLength(50, ErrorMessage = "Email nie może mieć więcej niż 50 znaków.")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres email.")]
        public string Mail { get; set; }

        [DisplayName("Telefon")]
        [MaxLength(15, ErrorMessage = "Telefon nie może mieć więcej niż 15 znaków.")]
        public string Phone { get; set; }
    }
}
