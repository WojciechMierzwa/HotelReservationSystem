using HotelReservationSystem.Models;
using HotelReservationSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationSystem.Repositories
{
    public class ReservationRepository : IReservationInterface
    {
        private readonly ManagerContext _context;

        public ReservationRepository(ManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<ReservationModel> GetAll()
        {
            // Pobieramy dane rezerwacji razem z danymi gości
            return _context.Reservations.Include(r => r.Guest).ToList();
        }

        public ReservationModel Get(int id)
        {
            return _context.Reservations.Include(r => r.Guest).SingleOrDefault(r => r.Id == id);
        }

        public void Add(ReservationModel reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void Update(int id, ReservationModel reservation)
        {
            var existing = _context.Reservations.SingleOrDefault(r => r.Id == id);
            if (existing != null)
            {
                existing.StartDate = reservation.StartDate;
                existing.EndDate = reservation.EndDate;
                existing.TotalCost = reservation.TotalCost;
                existing.Guest_Id = reservation.Guest_Id;
                _context.SaveChanges();
            }
        }
        // Dodaj tę metodę do istniejącej klasy ReservationRepository, która implementuje IReservationInterface

        public List<ReservationViewModel> GetAllReservationsWithDetails()
        {
            var reservations = (from rr in _context.ReservationRooms
                                join r in _context.Rooms on rr.Room_Id equals r.Id
                                join res in _context.Reservations on rr.Reservation_Id equals res.Id
                                join g in _context.Guests on res.Guest_Id equals g.GuestId
                                select new ReservationViewModel
                                {
                                    RoomId = r.Id,
                                    GuestName = g.Name,
                                    GuestSurname = g.Surname,
                                    RoomNumber = r.RoomNumber.ToString(),
                                    Floor = r.Floor,
                                    Beds = r.Beds,
                                    TotalCost = res.TotalCost,
                                    StartDate = res.StartDate,
                                    EndDate = res.EndDate
                                }).ToList();

            return reservations;
        }

        public void Delete(int id)
        {
            var reservation = _context.Reservations.SingleOrDefault(r => r.Id == id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }
        }
    }
}