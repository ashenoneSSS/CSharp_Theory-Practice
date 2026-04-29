using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using Hospital.Services;

namespace Hospital
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<HospitalDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection_via_AddDbContext")));
            builder.Services.AddScoped<PatientService>();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
