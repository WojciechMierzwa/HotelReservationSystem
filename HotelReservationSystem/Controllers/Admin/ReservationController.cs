using HotelReservationSystem.Models;
using HotelReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelReservationSystem.Controllers.Admin
{
    public class ReservationController : Controller
    {
        private readonly IReservationInterface _reservationRepo;
        private readonly IGuestInterface _guestRepo;

        public ReservationController(IReservationInterface reservationRepo, IGuestInterface guestRepo)
        {
            _reservationRepo = reservationRepo;
            _guestRepo = guestRepo;
        }

        // GET: ReservationController
        public IActionResult Index()
        {
            var reservations = _reservationRepo.GetAll();  // Nie trzeba wywoływać Include tutaj, ponieważ to już robi repozytorium
            return View(reservations);
        }

        // GET: ReservationController/Details/5
        public IActionResult Details(int id)
        {
            var reservation = _reservationRepo.Get(id);
            if (reservation == null)
                return NotFound();
            return View(reservation);
        }

        // GET: ReservationController/Create
        public IActionResult Create()
        {
            // Zachowujemy oryginalną wersję, tylko zmieniamy Login na FullName
            ViewBag.Guest_Id = new SelectList(_guestRepo.GetAll(), "GuestId", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReservationModel reservation)
        {
            // Diagnostyka:
            System.Diagnostics.Debug.WriteLine($"Otrzymany model: StartDate={reservation.StartDate}, EndDate={reservation.EndDate}, TotalCost={reservation.TotalCost}, Guest_Id={reservation.Guest_Id}");
            foreach (var state in ModelState)
            {
                System.Diagnostics.Debug.WriteLine($"Klucz: {state.Key}, Walidacja: {state.Value.ValidationState}");
                foreach (var error in state.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"- Błąd: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                _reservationRepo.Add(reservation);
                return RedirectToAction(nameof(Index));
            }
            // Jeżeli model jest niepoprawny, przekazujemy ponownie listę gości
            ViewBag.Guest_Id = new SelectList(_guestRepo.GetAll(), "GuestId", "FullName", reservation.Guest_Id);
            return View(reservation);
        }

        // GET: ReservationController/Edit/5
        public IActionResult Edit(int id)
        {
            var reservation = _reservationRepo.Get(id);
            if (reservation == null)
                return NotFound();
            // Zachowujemy oryginalną wersję, tylko zmieniamy Login na FullName
            ViewBag.Guest_Id = new SelectList(_guestRepo.GetAll(), "GuestId", "FullName", reservation.Guest_Id);
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ReservationModel reservation)
        {
            if (id != reservation.Id)
                return NotFound();

            // Diagnostyka błędów walidacji
            foreach (var state in ModelState)
            {
                System.Diagnostics.Debug.WriteLine($"Klucz: {state.Key}, Walidacja: {state.Value.ValidationState}");
                foreach (var error in state.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"- Błąd: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                _reservationRepo.Update(id, reservation);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Guest_Id = new SelectList(_guestRepo.GetAll(), "GuestId", "FullName", reservation.Guest_Id);
            return View(reservation);
        }
        public IActionResult Delete(int id)
        {
            var reservation = _reservationRepo.Get(id);
            if (reservation == null)
                return NotFound();
            return View(reservation);
        }

        // POST: ReservationController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _reservationRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}