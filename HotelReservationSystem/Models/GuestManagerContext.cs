using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationSystem.Models
{
    public class GuestManagerContext : DbContext
    {
        public GuestManagerContext(DbContextOptions<GuestManagerContext> options) : base(options)
        {
        }
        public DbSet<GuestModel> Guests { get; set; }
    }
}
