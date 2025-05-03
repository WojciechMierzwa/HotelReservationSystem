using HotelReservationSystem.Models;

namespace HotelReservationSystem.Repositories
{
    public interface IGuestInterface
    {
        GuestModel Get(int guestId);
        IEnumerable<GuestModel> GetAll();
        void Add(GuestModel guest);
        void Update(int guestId, GuestModel guest);
        void Delete(int guestId);
    }
}
