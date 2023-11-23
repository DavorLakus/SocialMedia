using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        Console.Write("------------");
        services.AddHttpClient<IApiService, ApiService>();
        services.AddControllersWithViews();    
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
        // Configure the request processing pipeline (middleware, routing, etc.)
        app.UseRouting();

        // Add other middleware and configuration as needed...
    }
}
