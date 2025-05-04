using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationSystem.Repositories
{
    public class TypeRepository : ITypeInterface
    {
        private readonly ManagerContext _context;

        public TypeRepository(ManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<TypeModel> GetAll()
        {
            return _context.Types.ToList();
        }

        public TypeModel Get(int id)
        {
            return _context.Types.Find(id);
        }

        public void Add(TypeModel type)
        {
            _context.Types.Add(type);
            _context.SaveChanges();
        }

        public void Update(int id, TypeModel type)
        {
            var existing = _context.Types.Find(id);
            if (existing != null)
            {
                existing.Name = type.Name;
                existing.BaseCost = type.BaseCost;
                existing.HighSeasonCost = type.HighSeasonCost;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var type = _context.Types.Find(id);
            if (type != null)
            {
                _context.Types.Remove(type);
                _context.SaveChanges();
            }
        }
    }
}
