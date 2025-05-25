using Microsoft.OpenApi.Models;
using PizzaStore.DB;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => { });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "Keep track of your tasks", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
    });
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/products", () => PizzaDB.GetPizzas());
app.MapGet("/products/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapGet("/product", () => new { id = 1 });

app.MapPost("/products", (Pizza product) => PizzaDB.CreatePizza(product));

app.MapPut("/products/{id}", (Pizza product) => PizzaDB.UpdatePizza(product));

app.MapDelete("/products/{id}", (int id) => PizzaDB.RemovePizza(id));

app.Run();