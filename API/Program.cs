using Caffe.Application;
using Caffe.Application.Common.Interfaces.Authentication;
using Caffe.Application.Common.Interfaces.Base;
using Caffe.Infrastructure;
using Caffe.Infrastructure.Services.Authentication;
using Caffe.Infrastructure.Services.Base;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 * ********************
 * ***   SeriLog   ***
 * ********************
 */
//builder.Host.UseSerilog();


/*
 * ********************
 * ***   Injection  ***
 * ********************
 */
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);


var app = builder.Build();

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
