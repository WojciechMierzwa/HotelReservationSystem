using HotelReservationSystem.Models;
using System.Collections.Generic;

namespace HotelReservationSystem.Repositories
{
    public interface IRoomInterface
    {
        IEnumerable<RoomModel> GetAll();
        RoomModel Get(int id);
        void Add(RoomModel room);
        void Update(int id, RoomModel room);
        void Delete(int id);
    }
}
