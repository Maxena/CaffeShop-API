using Caffe.API;
using Caffe.Application;
using Caffe.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


/*
 * ********************
 * ***   Injection  ***
 * ********************
 */
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);


/*
 * ********************
 * ***  MiddleWares ***
 * ********************
 */
var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Caffe API V1");
    c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "Caffe API V2");
});

//var provider = app..GetService<IApiVersionDescriptionProvider>();
//app.UseSwaggerUI(
//    options =>
//    {
//        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
//        // build a swagger endpoint for each discovered API version  
//        foreach (var description in provider.ApiVersionDescriptions)
//            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
//    });

app.UseApiVersioning();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();