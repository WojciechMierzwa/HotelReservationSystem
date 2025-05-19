using HotelReservationSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Controllers.Public
{
    public class PublicController : Controller
    {
        private readonly ManagerContext _context;

        public PublicController(ManagerContext context)
        {
            _context = context;
        }

        // GET: PublicController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Guests
                .FirstOrDefault(g => g.Login == username && g.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("GuestId", user.GuestId);
                HttpContext.Session.SetString("GuestName", user.FullName);

                return RedirectToAction("Profile");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }
        [HttpGet]
        public IActionResult LoginEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginEmployee(string username, string password)
        {
            var employee = _context.Employees
                .FirstOrDefault(e => e.Login == username && e.Password == password);

            if (employee != null)
            {
                HttpContext.Session.SetInt32("EmployeeId", employee.Id);
                HttpContext.Session.SetString("EmployeeName", $"{employee.Name} {employee.Surname}");
                HttpContext.Session.SetString("UserRole", employee.IsAdmin ? "Admin" : "Employee");

                return RedirectToAction("ProfileEmployee");
            }

            ViewBag.Error = "Nieprawidłowy login lub hasło.";
            return View();
        }

        public IActionResult ProfileEmployee()
        {
            var employeeId = HttpContext.Session.GetInt32("EmployeeId");

            if (employeeId == null)
                return RedirectToAction("LoginEmployee");

            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("LoginEmployee");
            }

            return View(employee);
        }





        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(GuestModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Guests.Add(model);     
                _context.SaveChanges();  

                return RedirectToAction("Login");
            }

            return View(model);
        }


        public IActionResult Profile()
        {
            var guestId = HttpContext.Session.GetInt32("GuestId");

            if (guestId == null)
                return RedirectToAction("Login");

            var user = _context.Guests.FirstOrDefault(g => g.GuestId == guestId);
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }





    }
}
