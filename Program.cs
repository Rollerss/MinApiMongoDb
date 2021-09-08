using MinApiMongoDb.EndpointDefinitions;
using MinApiMongoDb.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointDefinitions(typeof(CompanyTicker));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.MapGet("/", () => "Hello World!");
app.UseEndpointDefinitions();
app.Run();
