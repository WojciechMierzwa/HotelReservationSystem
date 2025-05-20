using HotelReservationSystem.Repositories;
using HotelReservationSystem.ViewModels;
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

        public IActionResult PhoneReservation()
        {
            return View();
        }

        public IActionResult ManageAllReservations()
        {
            var reservations = _reservationRepo.GetAllReservationsWithDetails();
            return View(reservations);
        }

        public IActionResult ReservationDetails(int id)
        {
            var reservation = _reservationRepo.GetDetailed(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        public IActionResult CheckIn()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}