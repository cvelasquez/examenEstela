
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();


builder.Logging.AddConsole(); // Log en la consola
builder.Logging.AddDebug(); // Opcional: Agrega logging en la ventana de depuración (Debug)
builder.Services.AddControllers();

var app = builder.Build();

// Registrar el middleware de manejo de errores
app.UseMiddleware<ExceptionHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
