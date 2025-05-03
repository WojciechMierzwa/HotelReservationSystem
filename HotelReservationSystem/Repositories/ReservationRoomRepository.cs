using HotelReservationSystem.Models;

namespace HotelReservationSystem.Repositories
{
    public class ReservationRoomRepository : IReservationRoomInterface
    {
        private readonly GuestManagerContext _context;

        public ReservationRoomRepository(GuestManagerContext context)
        {
            _context = context;
        }

        public void Add(ReservationRoomModel reservationRoom)
        {
            _context.ReservationRooms.Add(reservationRoom);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.ReservationRooms.Find(id);
            if (item != null)
            {
                _context.ReservationRooms.Remove(item);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ReservationRoomModel> GetAll()
        {
            return _context.ReservationRooms
                .Include(rr => rr.Room)
                .Include(rr => rr.Reservation)
                .ToList();
        }

        public IEnumerable<ReservationRoomModel> GetByReservationId(int reservationId)
        {
            return _context.ReservationRooms
                .Where(rr => rr.Reservation_Id == reservationId)
                .Include(rr => rr.Room)
                .ToList();
        }
    }

}
