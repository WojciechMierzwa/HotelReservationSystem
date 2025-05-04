using HotelReservationSystem.Models;
using HotelReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelReservationSystem.Controllers
{
    public class ReservationRoomController : Controller
    {
        private readonly IReservationRoomInterface _reservationRoomRepo;
        private readonly IReservationInterface _reservationRepo;
        private readonly IRoomInterface _roomRepo;

        public ReservationRoomController(
            IReservationRoomInterface reservationRoomRepo,
            IReservationInterface reservationRepo,
            IRoomInterface roomRepo)
        {
            _reservationRoomRepo = reservationRoomRepo;
            _reservationRepo = reservationRepo;
            _roomRepo = roomRepo;
        }

        public IActionResult Index()
        {
            var list = _reservationRoomRepo.GetAll();
            return View(list);
        }

        public IActionResult Edit(int id)
        {
            var rr = _reservationRoomRepo.Get(id);
            if (rr == null)
                return NotFound();

            ViewBag.Reservations = new SelectList(_reservationRepo.GetAll(), "Id", "Id", rr.Reservation_Id);
            ViewBag.Rooms = new SelectList(_roomRepo.GetAll(), "Id", "RoomNumber", rr.Room_Id);
            return View(rr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ReservationRoomModel rr)
        {
            if (id != rr.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _reservationRoomRepo.Update(id, rr);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Reservations = new SelectList(_reservationRepo.GetAll(), "Id", "Id", rr.Reservation_Id);
            ViewBag.Rooms = new SelectList(_roomRepo.GetAll(), "Id", "RoomNumber", rr.Room_Id);
            return View(rr);
        }

        public IActionResult Details(int id)
        {
            var item = _reservationRoomRepo.Get(id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        public IActionResult Create()
        {
            ViewBag.Reservations = new SelectList(_reservationRepo.GetAll(), "Id", "Id");
            ViewBag.Rooms = new SelectList(_roomRepo.GetAll(), "Id", "RoomNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReservationRoomModel model)
        {
            if (ModelState.IsValid)
            {
                _reservationRoomRepo.Add(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Reservations = new SelectList(_reservationRepo.GetAll(), "Id", "Id", model.Reservation_Id);
            ViewBag.Rooms = new SelectList(_roomRepo.GetAll(), "Id", "RoomNumber", model.Room_Id);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var item = _reservationRoomRepo.Get(id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _reservationRoomRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
