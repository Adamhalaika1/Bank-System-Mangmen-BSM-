using Bank.IOC;
using Bank.Repositories;
using Bank.Repositories.IRepository;
using Bank.Repositories.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IOContainer.RegisterServices(builder.Services);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


