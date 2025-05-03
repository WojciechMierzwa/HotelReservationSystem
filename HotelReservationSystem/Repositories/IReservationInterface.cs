using HotelReservationSystem.Models;
using System.Collections.Generic;

namespace HotelReservationSystem.Repositories
{
    public interface IReservationInterface
    {
        IEnumerable<ReservationModel> GetAll();
        ReservationModel Get(int id);
        void Add(ReservationModel reservation);
        void Update(int id, ReservationModel reservation);
        void Delete(int id);
    }
}
