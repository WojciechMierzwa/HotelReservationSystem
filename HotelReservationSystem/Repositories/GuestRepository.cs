using HotelReservationSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repositories
{
    public class GuestRepository : IGuestInterface
    {
        private readonly GuestManagerContext _context;
        public GuestRepository(GuestManagerContext context)
        {
            _context = context;
        }

        public GuestModel Get(int guestId)
        {
            return _context.Guests.SingleOrDefault(x => x.GuestId == guestId);
        }

        public IEnumerable<GuestModel> GetAll()
        {
            return _context.Guests.ToList();
        }

        public void Add(GuestModel guest)
        {
            _context.Guests.Add(guest);
            _context.SaveChanges();
        }
        public void Update(int guestId, GuestModel guest)
        {
            var result = _context.Guests.SingleOrDefault(x => x.GuestId == guestId);
            if (result != null)
            {
                result.Login = guest.Login;
                result.Password = guest.Password;
                result.Name = guest.Name;
                result.Surname = guest.Surname;
                result.Mail = guest.Mail;
                result.Phone = guest.Phone;
                _context.SaveChanges();
            }
        }
        public void Delete(int guestId)
        {
            var result = _context.Guests.SingleOrDefault(x => x.GuestId == guestId);
            if (result != null)
            {
                _context.Guests.Remove(result);
                _context.SaveChanges();
            }
        }
    }
}