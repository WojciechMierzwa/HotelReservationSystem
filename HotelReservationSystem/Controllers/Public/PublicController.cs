using System.Globalization;
using HotelReservationSystem.Helpers;
using HotelReservationSystem.Models;
using HotelReservationSystem.Repositories;
using HotelReservationSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Controllers.Public
{
    public class PublicController : Controller
    {
        private readonly ManagerContext _context;
        private readonly IRoomInterface _roomRepo;
        private readonly IHotelInterface _hotelRepo;
        private readonly ITypeInterface _typeRepo;
        private readonly IReservationInterface _reservationRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PublicController(
    ManagerContext context,
    IRoomInterface roomRepo,
    IHotelInterface hotelRepo,
    ITypeInterface typeRepo,
    IReservationInterface reservationRepo,
    IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _roomRepo = roomRepo;
            _hotelRepo = hotelRepo;
            _typeRepo = typeRepo;
            _reservationRepo = reservationRepo;
            _httpContextAccessor = httpContextAccessor;

        }


        public IActionResult Reserve(int? beds, int? typeId, int? hotelId)
        {
            var rooms = _roomRepo.GetAll()
                .Where(r => r.Status == "Available");

            if (beds.HasValue)
                rooms = rooms.Where(r => r.Beds == beds.Value);

            if (typeId.HasValue)
                rooms = rooms.Where(r => r.Type_Id == typeId.Value);

            if (hotelId.HasValue)
                rooms = rooms.Where(r => r.Hotel_Id == hotelId.Value);

            var model = new RoomFilterViewModel
            {
                Rooms = rooms.ToList(),
                Beds = beds,
                TypeId = typeId,
                HotelId = hotelId,
                Types = _typeRepo.GetAll(),
                Hotels = _hotelRepo.GetAll()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddRoomToReservation(int id)
        {
            var reservedRooms = HttpContext.Session.GetObjectFromJson<List<int>>("ReservedRoomIds") ?? new List<int>();

            if (!reservedRooms.Contains(id))
                reservedRooms.Add(id);

            HttpContext.Session.SetObjectAsJson("ReservedRoomIds", reservedRooms);

            return RedirectToAction("Reserve"); // Wracamy na listę
        }

        [HttpGet]
        public IActionResult ContinueReservation()
        {
            var reservedRooms = HttpContext.Session.GetObjectFromJson<List<int>>("ReservedRoomIds");

            if (reservedRooms == null || !reservedRooms.Any())
            {
                TempData["Error"] = "Musisz najpierw wybrać co najmniej jeden pokój.";
                return RedirectToAction("Reserve");
            }

            return RedirectToAction("EnterGuestDetails"); // zakładamy kolejny krok
        }
        public IActionResult EnterGuestDetails()
        {
            var reservedRoomIds = HttpContext.Session.GetObjectFromJson<List<int>>("ReservedRoomIds") ?? new List<int>();

            var rooms = _roomRepo.GetAll()
                .Where(r => reservedRoomIds.Contains(r.Id))
                .ToList();

            var model = new GuestDetailsViewModel
            {
                SelectedRooms = rooms
            };

            return View(model);
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

        [HttpGet]
        public IActionResult RemoveRoomFromReservation(int id)
        {
            var reservedRooms = HttpContext.Session.GetObjectFromJson<List<int>>("ReservedRoomIds") ?? new List<int>();

            if (reservedRooms.Contains(id))
            {
                reservedRooms.Remove(id);
                HttpContext.Session.SetObjectAsJson("ReservedRoomIds", reservedRooms);
            }

            return RedirectToAction("EnterGuestDetails");
        }

        public IActionResult ConfirmReservation(string DateFrom, string DateTo)
        {
            var reservedRoomIds = HttpContext.Session.GetObjectFromJson<List<int>>("ReservedRoomIds");

            if (reservedRoomIds == null || !reservedRoomIds.Any())
            {
                TempData["Error"] = "Brak wybranych pokoi do rezerwacji.";
                return RedirectToAction("Reserve");
            }

            if (!DateTime.TryParseExact(DateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate)
                || !DateTime.TryParseExact(DateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate)
                || toDate <= fromDate)
            {
                TempData["Error"] = "Niepoprawny zakres dat.";
                return RedirectToAction("EnterGuestDetails");
            }

   
            var guestId = HttpContext.Session.GetInt32("GuestId");
            if (guestId == null)
            {
                TempData["Error"] = "Proszę się zalogować, aby dokonać rezerwacji.";
                return RedirectToAction("Login", "Public");
            }

            // Oblicz koszt całkowity (przykładowo)
            decimal totalCost = 0m;

            foreach (var roomId in reservedRoomIds)
            {
                var room = _context.Rooms.Include(r => r.Type).FirstOrDefault(r => r.Id == roomId);
                if (room != null && room.Type != null)
                {
                
                    int days = (toDate - fromDate).Days;
                    totalCost += room.Type.BaseCost * days; 
                }
            }

            
            var reservation = new ReservationModel
            {
                StartDate = fromDate,
                EndDate = toDate,
                TotalCost = totalCost,
                Guest_Id = guestId.Value
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges(); // Zapisujemy, by mieć Id rezerwacji

            // Tworzymy powiązania z pokojami
            foreach (var roomId in reservedRoomIds)
            {
                var reservationRoom = new ReservationRoomModel
                {
                    Reservation_Id = reservation.Id,
                    Room_Id = roomId
                };
                _context.ReservationRooms.Add(reservationRoom);

                // Aktualizujemy status pokoju na "Occupied" lub inny
                /*var roomToUpdate = _context.Rooms.Find(roomId);
                if (roomToUpdate != null)
                {
                    roomToUpdate.Status = "Occupied";
                }*/
            }

            _context.SaveChanges();

            HttpContext.Session.Remove("ReservedRoomIds");

            TempData["Success"] = $"Rezerwacja od {fromDate.ToShortDateString()} do {toDate.ToShortDateString()} została zapisana.";
          
            var guest = _context.Guests.FirstOrDefault(g => g.GuestId == guestId);
            var rooms = _context.Rooms.Include(r => r.Type).Where(r => reservedRoomIds.Contains(r.Id)).ToList();

  
            var mailer = new Mail();
            mailer.SendReservationConfirmation(guest, reservation, rooms);


            return RedirectToAction("Confirm");
        }

        public ActionResult Confirm()
        {
            return View();
        }


        public ActionResult MyReservations()
        {
            // Debug: Log all session keys and values
            var session = _httpContextAccessor.HttpContext.Session;
            var sessionData = new Dictionary<string, string>();
            foreach (var key in session.Keys)
            {
                var value = session.GetString(key) ?? session.GetInt32(key)?.ToString() ?? "null";
                sessionData.Add(key, value);
                Console.WriteLine($"Session Key: {key}, Value: {value}");
            }
            TempData["SessionDebug"] = string.Join("; ", sessionData.Select(kv => $"{kv.Key}: {kv.Value}"));

            // Retrieve GuestId using the correct session key
            var guestId = _httpContextAccessor.HttpContext.Session.GetInt32("GuestId");
            if (guestId == null)
            {
                TempData["Error"] = "Please log in to view your reservations.";
                return RedirectToAction("Login");
            }

            var reservations = _reservationRepo.GetAllReservationsWithDetailsOfUser(guestId.Value);
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
                var mailer = new Mail();
                mailer.WelcomeEmail(model);

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
