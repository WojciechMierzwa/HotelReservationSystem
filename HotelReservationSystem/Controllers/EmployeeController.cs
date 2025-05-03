using HotelReservationSystem.Models;
using HotelReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelReservationSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeInterface _employeeRepo;
        private readonly IHotelInterface _hotelRepo;

        public EmployeeController(IEmployeeInterface employeeRepo, IHotelInterface hotelRepo)
        {
            _employeeRepo = employeeRepo;
            _hotelRepo = hotelRepo;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepo.GetAll();
            return View(employees);
        }

        public IActionResult Details(int id)
        {
            var employee = _employeeRepo.Get(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        public IActionResult Create()
        {
            ViewBag.Hotel_Id = new SelectList(_hotelRepo.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepo.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Hotel_Id = new SelectList(_hotelRepo.GetAll(), "Id", "Name", employee.Hotel_Id);
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _employeeRepo.Get(id);
            if (employee == null) return NotFound();

            ViewBag.Hotel_Id = new SelectList(_hotelRepo.GetAll(), "Id", "Name", employee.Hotel_Id);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeModel employee)
        {
            if (id != employee.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _employeeRepo.Update(id, employee);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hotel_Id = new SelectList(_hotelRepo.GetAll(), "Id", "Name", employee.Hotel_Id);
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _employeeRepo.Get(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeeRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
