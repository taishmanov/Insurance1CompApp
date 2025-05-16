
using Insurance1CompApp.Repositories;
using Insurance1CompApp.Services;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Add framework services.
        services.AddControllers();

        // Register application services.
        services.AddScoped<ICountryGwpRepository, CountryGwpRepository>();
        services.AddScoped<CountryGwpService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}