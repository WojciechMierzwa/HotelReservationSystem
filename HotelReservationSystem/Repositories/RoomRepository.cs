using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationSystem.Repositories
{
    public class RoomRepository : IRoomInterface
    {
        private readonly GuestManagerContext _context;

        public RoomRepository(GuestManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomModel> GetAll()
        {
            return _context.Rooms
                .Include(r => r.Type)
                .Include(r => r.Hotel)
                .ToList();
        }

        public RoomModel Get(int id)
        {
            return _context.Rooms
                .Include(r => r.Type)
                .Include(r => r.Hotel)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Add(RoomModel room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void Update(int id, RoomModel room)
        {
            var existing = _context.Rooms.Find(id);
            if (existing == null) return;

            existing.Floor = room.Floor;
            existing.AirConditioned = room.AirConditioned;
            existing.Beds = room.Beds;
            existing.Status = room.Status;
            existing.RoomNumber = room.RoomNumber;
            existing.Type_Id = room.Type_Id;
            existing.Hotel_Id = room.Hotel_Id;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null) return;

            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}
