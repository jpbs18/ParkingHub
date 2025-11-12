using ParkingHub.Extensions;
using ParkingHub.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

app.ApplyMigrationsAndSeed();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();
