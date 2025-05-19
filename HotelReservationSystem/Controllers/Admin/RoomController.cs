using HotelReservationSystem.Models;
using HotelReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelReservationSystem.Controllers.Admin
{
    public class RoomController : Controller
    {
        private readonly IRoomInterface _roomRepo;
        private readonly IHotelInterface _hotelRepo;
        private readonly ITypeInterface _typeRepo;

        public RoomController(IRoomInterface roomRepo, IHotelInterface hotelRepo, ITypeInterface typeRepo)
        {
            _roomRepo = roomRepo;
            _hotelRepo = hotelRepo;
            _typeRepo = typeRepo;
        }

        public IActionResult Index()
        {
            return View(_roomRepo.GetAll());
        }

        public IActionResult Details(int id)
        {
            var room = _roomRepo.Get(id);
            if (room == null) return NotFound();
            return View(room);
        }

        public IActionResult Create()
        {
            ViewBag.Hotel_Id = new SelectList(_hotelRepo.GetAll(), "Id", "Name");
            ViewBag.Type_Id = new SelectList(_typeRepo.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomModel room)
        {
            if (ModelState.IsValid)
            {
                _roomRepo.Add(room);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hotel_Id = new SelectList(_hotelRepo.GetAll(), "Id", "Name", room.Hotel_Id);
            ViewBag.Type_Id = new SelectList(_typeRepo.GetAll(), "Id", "Name", room.Type_Id);
            return View(room);
        }

        public IActionResult Edit(int id)
        {
            var room = _roomRepo.Get(id);
            if (room == null) return NotFound();

            ViewBag.Hotel_Id = new SelectList(_hotelRepo.GetAll(), "Id", "Name", room.Hotel_Id);
            ViewBag.Type_Id = new SelectList(_typeRepo.GetAll(), "Id", "Name", room.Type_Id);
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RoomModel room)
        {
            if (id != room.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _roomRepo.Update(id, room);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hotel_Id = new SelectList(_hotelRepo.GetAll(), "Id", "Name", room.Hotel_Id);
            ViewBag.Type_Id = new SelectList(_typeRepo.GetAll(), "Id", "Name", room.Type_Id);
            return View(room);
        }

        public IActionResult Delete(int id)
        {
            var room = _roomRepo.Get(id);
            if (room == null) return NotFound();
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _roomRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
