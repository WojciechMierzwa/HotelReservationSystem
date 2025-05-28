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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var reservation = _reservationRepo.GetDetailed(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ReservationViewModel viewModel)
        {
            if (id != viewModel.ReservationID)
            {
                return BadRequest("Reservation ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _reservationRepo.Update2(id, viewModel);
                    return RedirectToAction(nameof(ManageAllReservations));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while updating the reservation: {ex.Message}");
                }
            }

            // If validation fails or an error occurs, return to the edit view with the model
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var reservation = _reservationRepo.GetDetailed(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _reservationRepo.Delete(id);
                return RedirectToAction(nameof(ManageAllReservations));
            }
            catch (Exception ex)
            {
              
                TempData["ErrorMessage"] = $"An error occurred while deleting the reservation: {ex.Message}";
                return RedirectToAction(nameof(Delete), new { id });
            }
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