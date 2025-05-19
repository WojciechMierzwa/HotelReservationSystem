using HotelReservationSystem.Models;
using HotelReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Controllers.Admin
{
    public class TypeController : Controller
    {
        private readonly ITypeInterface _typeRepository;

        public TypeController(ITypeInterface typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public IActionResult Index()
        {
            return View(_typeRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            var type = _typeRepository.Get(id);
            if (type == null) return NotFound();
            return View(type);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TypeModel type)
        {
            if (ModelState.IsValid)
            {
                _typeRepository.Add(type);
                return RedirectToAction(nameof(Index));
            }
            return View(type);
        }

        public IActionResult Edit(int id)
        {
            var type = _typeRepository.Get(id);
            if (type == null) return NotFound();
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TypeModel type)
        {
            if (id != type.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _typeRepository.Update(id, type);
                return RedirectToAction(nameof(Index));
            }
            return View(type);
        }

        public IActionResult Delete(int id)
        {
            var type = _typeRepository.Get(id);
            if (type == null) return NotFound();
            return View(type);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _typeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
