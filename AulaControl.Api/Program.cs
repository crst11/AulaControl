var builder = WebApplication.CreateBuilder(args);

// Agrega controladores
builder.Services.AddControllers();

// Agrega Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activa Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();