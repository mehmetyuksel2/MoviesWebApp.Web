using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoviesWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)//bu metotda kullan�lacak servis ayarlar� yap�l�r
        {//AddDbContext her request'te bir kez olu�turur. scope servis
//            services.AddDbContext<MovieContext>(options =>
//            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));


//------------------------------------------------------------------------------------------------
            services.AddDbContext<MovieContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MsSQLConnection")));
            services.AddControllersWithViews();//48. ders
//------------------------------------------------------------------------------------------------------

            services.AddControllersWithViews().AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled=true);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//Bu metot uygulamam�za gelen HTTP isteklerini hangi a�amalardan ge�irerek bir HTTP cevab� olu�turaca��m�z� belirtti�imiz metottur. 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DataSeeding.Seed(app);// uygulama her debug edildi�inde database silindiyse , dataseeding s�n�f�ndaki veriler database e tekrar eklenir.
            }
            app.UseStaticFiles(); // wwwroot klas�r�n� kullan�ma a�ar
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=Home}/{action=Index}/{id?}"//uygulama ba�lad��� anda default olarak home controller�n alt�ndaki index actionuna gidiyor        
                    
                    );// D�NAM�K HAL� BUDUR.

                ////localhost:5000
                //endpoints.MapControllerRoute(
                //        name: "home",
                //        pattern: "",
                //        defaults: new { controller = "Home", action = "Index" }



                //    );
                ////localhost:5000/about
                //endpoints.MapControllerRoute(
                //        name: "about",
                //        pattern: "hakkimizda",
                //        defaults: new { controller = "Home", action = "About" }



                //    );
                ////localhost:5000/movies/list
                //endpoints.MapControllerRoute(
                //        name: "movieList",
                //        pattern: "movies/list",
                //        defaults: new { controller="Movies", action="List" }
                
                    
                    
                //    );
                ////localhost:5000/movies/details
                //endpoints.MapControllerRoute(
                //        name: "moviesDetails",
                //        pattern: "movies/details",
                //        defaults: new { controller = "Movies", action = "details" }



                //    ); bu a��klama sat�rlar�nda, url k�sm�nda ne yaz�ld���n� kontrol eder ve ona g�re controller alt�nda actiona y�nlendirir. D�NAM�K DE��LD�R



            });
        }
    }
}
