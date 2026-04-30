var builder = WebApplication.CreateBuilder(args);

// 1. Activar Controladores
builder.Services.AddControllers();

// 2. CONFIGURAR CORS (Permitir que el ESP32 y Wokwi se conecten)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Activar Swagger
app.UseSwagger();
app.UseSwaggerUI();

// 4. USAR CORS (Debe ir antes de MapControllers)
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();