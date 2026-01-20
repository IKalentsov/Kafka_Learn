using Messaging.Kafka.Extensions;
using Messaging.Kafka.Models;
using Messaging.Kafka.Producer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProducer<Order>(builder.Configuration.GetSection("Kafka:Order"));

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/create-order", async (IKafkaProducer<Order> kafkaProducer) =>
{
    await kafkaProducer.ProduceAsync(new Order
    {
        Id = Guid.NewGuid().ToString(),
        Name = "New order"
    }, default);
});

//app.UseAuthorization();

app.MapControllers();

app.Run();
