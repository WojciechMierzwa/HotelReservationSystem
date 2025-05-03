using HotelReservationSystem.Models;
using HotelReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelInterface _hotelRepo;

        public HotelController(IHotelInterface hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        public IActionResult Index()
        {
            var hotels = _hotelRepo.GetAll();
            return View(hotels);
        }

        public IActionResult Details(int id)
        {
            var hotel = _hotelRepo.Get(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HotelModel hotel)
        {
            if (ModelState.IsValid)
            {
                _hotelRepo.Add(hotel);
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        public IActionResult Edit(int id)
        {
            var hotel = _hotelRepo.Get(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, HotelModel hotel)
        {
            if (id != hotel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _hotelRepo.Update(id, hotel);
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        public IActionResult Delete(int id)
        {
            var hotel = _hotelRepo.Get(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _hotelRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
