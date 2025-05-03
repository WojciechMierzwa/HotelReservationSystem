using System.Collections.Generic;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.Repositories
{
    public interface ITypeInterface
    {
        IEnumerable<TypeModel> GetAll();
        TypeModel Get(int id);
        void Add(TypeModel type);
        void Update(int id, TypeModel type);
        void Delete(int id);
    }
}
