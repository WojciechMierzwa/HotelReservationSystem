using HotelReservationSystem.Repositories;
using HotelReservationSystem.Models;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Dodaj konfiguracjê po³¹czenia z baz¹ danych
builder.Services.AddDbContext<ManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GuestManagerDatabase"))
);
//cookies dla trackowania kto jest zalogowane etc...
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache(); 
//
var supportedCultures = new[] { "pl-PL" };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture("pl-PL")
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

builder.Services.AddControllersWithViews();


// Rejestracja repozytoriów
builder.Services.AddScoped<IReservationInterface, ReservationRepository>();
builder.Services.AddScoped<ITypeInterface, TypeRepository>();
builder.Services.AddScoped<IHotelInterface, HotelRepository>();
builder.Services.AddScoped<IEmployeeInterface, EmployeeRepository>();
builder.Services.AddScoped<IRoomInterface, RoomRepository>();
builder.Services.AddScoped<IReservationRoomInterface, ReservationRoomRepository>();
builder.Services.AddScoped<IGuestInterface, GuestRepository>();
builder.Services.AddHttpContextAccessor(); //odpowiedzialny za middleware 
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
app.UseSession();

app.UseAuthorization();


//USER 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Public}/{action=Index}/{id?}");

//CRUD ADMIN
app.MapControllerRoute(
    name: "ReservationRoom",
    pattern: "{controller=ReservationRoom}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "reservation",
    pattern: "{controller=Reservation}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "room",
    pattern: "{controller=Room}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "employee",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "guest",
    pattern: "{controller=Guest}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "hotel",
    pattern: "{controller=Hotel}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "type",
    pattern: "{controller=Type}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
