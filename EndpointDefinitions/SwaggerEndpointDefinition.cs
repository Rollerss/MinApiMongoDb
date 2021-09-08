using Microsoft.OpenApi.Models;

namespace MinApiMongoDb.EndpointDefinitions;
public class SwaggerEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(s =>
            s.SwaggerEndpoint("/swagger/v1/swagger.json", "MinApiMongoDb v1")
        );
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(a =>
        {
            a.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MinApiMongoDb",
                Version = "v1"
            });
        });
    }
}
