using tech_test_payment.application;
using tech_test_payment.infrastructure;
using tech_test_payment.infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrasctructureDependencies();
builder.Services.AddApplicationDependencies();

var app = builder.Build();

app.Services.EnsureDbCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
