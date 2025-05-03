using HotelReservationSystem.Models;
using HotelReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelReservationSystem.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestRepository _guestRepository;

        // Konstruktor wstrzykujący zależność repozytorium
        public GuestController(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        // GET: GuestController
        public ActionResult Index()
        {
            var guests = _guestRepository.GetAll();
            return View(guests);
        }

        // GET: GuestController/Details/5
        public ActionResult Details(int id)
        {
            var guest = _guestRepository.Get(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        // GET: GuestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GuestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GuestModel guest)
        {
            if (ModelState.IsValid)
            {
                _guestRepository.Add(guest);
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        // GET: GuestController/Edit/5
        public ActionResult Edit(int id)
        {
            var guest = _guestRepository.Get(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        // POST: GuestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GuestModel guest)
        {
            if (id != guest.GuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _guestRepository.Update(id, guest);
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        // GET: GuestController/Delete/5
        public ActionResult Delete(int id)
        {
            var guest = _guestRepository.Get(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        // POST: GuestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _guestRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
