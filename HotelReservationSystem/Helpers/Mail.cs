using HotelReservationSystem.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;

namespace HotelReservationSystem.Helpers
{
    public class Mail
    {
        private readonly string smtpHost = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string smtpUser = "kapibara2000hd@gmail.com";
        private readonly string smtpPass = "fjau zncp sxsp tgge";

        public void WelcomeEmail(GuestModel guest)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hotel Reservation", smtpUser));
            message.To.Add(new MailboxAddress($"{guest.Name} {guest.Surname}", guest.Mail));
            message.Subject = "Witamy w naszym hotelu!";

            message.Body = new TextPart("html")
            {
                Text = $@"
                    <h2>Witamy, {guest.Name}!</h2>
                    <p>Dziękujemy za rejestrację w naszym systemie rezerwacji hoteli.</p>
                    <p>Mamy nadzieję, że będziesz zadowolony z naszych usług!</p>"
            };

            Send(message);
        }

        public void SendReservationConfirmation(GuestModel guest, ReservationModel reservation, List<RoomModel> rooms)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hotel Reservation", smtpUser));
            message.To.Add(new MailboxAddress($"{guest.Name} {guest.Surname}", guest.Mail));
            message.Subject = "Potwierdzenie rezerwacji";

            var roomDetails = string.Join("<br/>", rooms.Select(r => $"Pokój #{r.Id} - {r.Type?.Name} - {r.Beds} łóżka"));

            message.Body = new TextPart("html")
            {
                Text = $@"
            <h2>Potwierdzenie rezerwacji</h2>
            <p>Drogi {guest.Name},</p>
            <p>Twoja rezerwacja została potwierdzona.</p>
            <p><strong>Od:</strong> {reservation.StartDate:dd.MM.yyyy} <br/>
            <strong>Do:</strong> {reservation.EndDate:dd.MM.yyyy} <br/>
            <strong>Koszt całkowity:</strong> {reservation.TotalCost:C}</p>
            <p><strong>Pokoje:</strong><br/>{roomDetails}</p>
            <p>Dziękujemy za dokonanie rezerwacji!</p>"
            };

            Send(message);
        }


        private void Send(MimeMessage message)
        {
            using var client = new SmtpClient();
            client.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            client.Authenticate(smtpUser, smtpPass);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
