using HotelReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Controllers.Employee
{
    public class BookController : Controller
    {
        private readonly IReservationInterface _reservationRepo;

        public BookController(IReservationInterface reservationRepo)
        {
            _reservationRepo = reservationRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CheckIn()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        public IActionResult PhoneReservation()
        {
            return View();
        }
        public IActionResult ManageAllReservations()
        {
            var reservations = _reservationRepo.GetAllReservationsWithDetails();
            return View(reservations);
        }
    }
}
