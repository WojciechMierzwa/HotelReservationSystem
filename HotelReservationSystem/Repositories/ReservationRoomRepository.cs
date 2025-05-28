using System.Collections.Generic;
using System.Linq;
using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Repositories
{
    public class ReservationRoomRepository : IReservationRoomInterface
    {
        private readonly ManagerContext _context;

        public ReservationRoomRepository(ManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<ReservationRoomModel> GetAll()
        {
            return _context.ReservationRooms
                .Include(rr => rr.Reservation)
                .Include(rr => rr.Room)
                .ToList();
        }

        public ReservationRoomModel Get(int id)
        {
            return _context.ReservationRooms
                .Include(rr => rr.Reservation)
                .Include(rr => rr.Room)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Add(ReservationRoomModel reservationRoom)
        {
            _context.ReservationRooms.Add(reservationRoom);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var rr = _context.ReservationRooms.Find(id);
            if (rr != null)
            {
                _context.ReservationRooms.Remove(rr);
                _context.SaveChanges();
            }
        }
        public void Update(int id, ReservationRoomModel reservationRoom)
        {
            var existing = _context.ReservationRooms.Find(id);
            if (existing != null)
            {
                existing.Reservation_Id = reservationRoom.Reservation_Id;
                existing.Room_Id = reservationRoom.Room_Id;
                _context.SaveChanges();
            }
        }

    }
}
