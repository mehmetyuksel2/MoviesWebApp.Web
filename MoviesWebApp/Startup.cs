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
        public void ConfigureServices(IServiceCollection services)//bu metotda kullanýlacak servis ayarlarý yapýlýr
        {//AddDbContext her request'te bir kez oluþturur. scope servis
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//Bu metot uygulamamýza gelen HTTP isteklerini hangi aþamalardan geçirerek bir HTTP cevabý oluþturacaðýmýzý belirttiðimiz metottur. 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DataSeeding.Seed(app);// uygulama her debug edildiðinde database silindiyse , dataseeding sýnýfýndaki veriler database e tekrar eklenir.
            }
            app.UseStaticFiles(); // wwwroot klasörünü kullanýma açar
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=Home}/{action=Index}/{id?}"//uygulama baþladýðý anda default olarak home controllerýn altýndaki index actionuna gidiyor        
                    
                    );// DÝNAMÝK HALÝ BUDUR.

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



                //    ); bu açýklama satýrlarýnda, url kýsmýnda ne yazýldýðýný kontrol eder ve ona göre controller altýnda actiona yönlendirir. DÝNAMÝK DEÐÝLDÝR



            });
        }
    }
}
