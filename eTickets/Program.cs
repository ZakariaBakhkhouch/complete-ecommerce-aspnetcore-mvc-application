using eTickets.Data;
using eTickets.Data.Repository;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// configre the App DbContext
string cs = builder.Configuration.GetConnectionString(name: "DefaultConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(cs));


//Services configuration
builder.Services.AddScoped<IActorsRepository, ActorRepository>();
builder.Services.AddScoped<IProducersRepository, ProducersRepository>();
builder.Services.AddScoped<ICinemasRepository, CinemasRepository>();
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// seed database
AppDbInitializer.Seed(app);

app.Run();
