using System.Collections.Generic;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.Repositories
{
    public interface IReservationRoomInterface
    {
        IEnumerable<ReservationRoomModel> GetAll();
        ReservationRoomModel Get(int id);
        void Add(ReservationRoomModel reservationRoom);
        void Delete(int id);
        void Update(int id, ReservationRoomModel reservationRoom);

    }
}
