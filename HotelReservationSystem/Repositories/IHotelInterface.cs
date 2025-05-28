using HotelReservationSystem.Models;
using System.Collections.Generic;

namespace HotelReservationSystem.Repositories
{
    public interface IHotelInterface
    {
        IEnumerable<HotelModel> GetAll();
        HotelModel Get(int id);
        void Add(HotelModel hotel);
        void Update(int id, HotelModel hotel);
        void Delete(int id);
    }
}
