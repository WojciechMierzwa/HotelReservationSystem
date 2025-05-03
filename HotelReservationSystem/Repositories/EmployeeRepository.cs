using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationSystem.Repositories
{
    public class EmployeeRepository : IEmployeeInterface
    {
        private readonly GuestManagerContext _context;

        public EmployeeRepository(GuestManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeModel> GetAll()
        {
            return _context.Employees.Include(e => e.Hotel).ToList();
        }

        public EmployeeModel Get(int id)
        {
            return _context.Employees.Include(e => e.Hotel).FirstOrDefault(e => e.Id == id);
        }

        public void Add(EmployeeModel employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Update(int id, EmployeeModel employee)
        {
            var existing = _context.Employees.Find(id);
            if (existing == null) return;

            existing.IsAdmin = employee.IsAdmin;
            existing.Name = employee.Name;
            existing.Surname = employee.Surname;
            existing.Login = employee.Login;
            existing.Password = employee.Password;
            existing.Hotel_Id = employee.Hotel_Id;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return;

            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}
