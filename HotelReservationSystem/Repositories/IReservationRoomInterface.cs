using HotelReservationSystem.Models;

namespace HotelReservationSystem.Repositories
{
    public interface IReservationRoomInterface
    {
        void Add(ReservationRoomModel reservationRoom);
        void Delete(int id);
        IEnumerable<ReservationRoomModel> GetAll();
        IEnumerable<ReservationRoomModel> GetByReservationId(int reservationId);
    }

}
