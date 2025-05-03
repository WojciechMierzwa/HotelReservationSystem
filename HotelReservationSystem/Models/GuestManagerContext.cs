using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HotelReservationSystem.Models
{
    public class GuestManagerContext : DbContext
    {
        public GuestManagerContext(DbContextOptions<GuestManagerContext> options) : base(options)
        {
        }

        public DbSet<GuestModel> Guests { get; set; }
        public DbSet<ReservationModel> Reservations { get; set; }
        public DbSet<TypeModel> Types { get; set; }
        public DbSet<HotelModel> Hotels { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }


    }
}
