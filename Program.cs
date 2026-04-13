var builder = WebApplication.CreateBuilder(args);

// 1. Activar los controladores que acabas de crear
builder.Services.AddControllers();

// 2. Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Activar la interfaz gráfica de Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

// 4. Conectar las rutas
app.MapControllers();

app.Run();