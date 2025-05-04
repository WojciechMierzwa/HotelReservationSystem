using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationSystem.Repositories
{
    public class HotelRepository : IHotelInterface
    {
        private readonly ManagerContext _context;

        public HotelRepository(ManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelModel> GetAll()
        {
            return _context.Hotels.ToList();
        }

        public HotelModel Get(int id)
        {
            return _context.Hotels.Find(id);
        }

        public void Add(HotelModel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public void Update(int id, HotelModel hotel)
        {
            var existing = _context.Hotels.Find(id);
            if (existing == null) return;

            existing.Name = hotel.Name;
            existing.City = hotel.City;
            existing.PostalCode = hotel.PostalCode;
            existing.Adress1 = hotel.Adress1;
            existing.Address2 = hotel.Address2;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel == null) return;

            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
        }
    }
}
