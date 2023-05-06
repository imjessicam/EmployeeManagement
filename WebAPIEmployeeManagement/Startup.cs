using EmployeeManagement.Repositories;
using Microsoft.EntityFrameworkCore;

using DAO.Department;
using EmployeeManagement.Database;
using DAO.MappingProfiles;

namespace WebAPIEmployeeManagement
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)        //konfiguracja serwisow w aplikacji
        {
            

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpClient();
            services.AddCors();

            // Context
            services.AddDbContextFactory<DatabaseContext>(options => options.UseSqlServer("server=DESKTOP-9FN9TUU\\SQLEXPRESS; database=EmployeeManagement; Trusted_Connection=True; Connection Timeout=30"));

            // Repositories
            services.AddTransient<DepartmentRepository>();

            // Validators
            
            services.AddTransient<DepartmentDao>();


            services.AddTransient<DepartmentDao>();
            services.AddAutoMapper(typeof(DepartmentProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting(); // wskazywanie odpowiednich adresow

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin                
                .AllowCredentials()); // allow credentials

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

            



        }
    }
}