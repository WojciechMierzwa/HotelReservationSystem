using HotelReservationSystem.Models;
using System.Collections.Generic;

namespace HotelReservationSystem.Repositories
{
    public interface IEmployeeInterface
    {
        IEnumerable<EmployeeModel> GetAll();
        EmployeeModel Get(int id);
        void Add(EmployeeModel employee);
        void Update(int id, EmployeeModel employee);
        void Delete(int id);
    }
}
