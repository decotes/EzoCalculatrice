using EzoCalculatrice.API.Operators;
using EzoCalculatrice.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<NotationConverter>();
builder.Services.AddSingleton<ExpressionEvaluator>();
builder.Services.AddSingleton<IEnumerable<IOperator>>(sp => new List<IOperator>
{
    new AdditionOperator(),
    new SubtractionOperator(),
    new MultiplicationOperator(),
    new DivisionOperator()
    // Aquí puedes añadir más operadores.
});

// Agrega CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();

app.UseCors("MyAllowSpecificOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
