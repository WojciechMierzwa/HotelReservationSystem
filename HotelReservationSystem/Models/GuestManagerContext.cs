using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HotelReservationSystem.Models
{
    public class ManagerContext : DbContext
    {
        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {
        }

        public DbSet<GuestModel> Guests { get; set; }
        public DbSet<ReservationModel> Reservations { get; set; }
        public DbSet<TypeModel> Types { get; set; }
        public DbSet<HotelModel> Hotels { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<ReservationRoomModel> ReservationRooms { get; set; }



    }
}
