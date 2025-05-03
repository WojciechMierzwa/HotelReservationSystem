using HotelReservationSystem.Repositories;
using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodaj konfiguracjê po³¹czenia z baz¹ danych
builder.Services.AddDbContext<GuestManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GuestManagerDatabase"))
);

// Zarejestruj repozytorium dla rezerwacji
builder.Services.AddScoped<IReservationInterface, ReservationRepository>();
builder.Services.AddScoped<ITypeInterface, TypeRepository>();

builder.Services.AddScoped<IHotelInterface, HotelRepository>();

builder.Services.AddScoped<IEmployeeInterface, EmployeeRepository>();



// Zarejestruj repozytorium dla goœci
builder.Services.AddScoped<IGuestInterface, GuestRepository>();  // <-- Fix here

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
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
