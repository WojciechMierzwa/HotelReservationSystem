using HotelReservationSystem.Repositories;
using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodaj konfiguracjê po³¹czenia z baz¹ danych
builder.Services.AddDbContext<GuestManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GuestManagerDatabase"))
);

// Zarejestruj repozytorium GuestRepository
builder.Services.AddTransient<IGuestRepository, IGuestRepository>();

// Dodaj kontrolery z widokami
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Skonfiguruj potok ¿¹dañ HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Guest}/{action=Index}/{id?}");

app.Run();
