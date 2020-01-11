using System;
using CadastroDeFornecedores.Application.Services;
using CadastroDeFornecedores.Data.Context;
using CadastroDeFornecedores.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CadastroDeFornecedores.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<CadastroDeFornecedoresContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region Services

            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();

            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            UpdateDatabase(app);

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

            Console.WriteLine("A aplicação está pronta para uso!");
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            Console.WriteLine("Criando a base de dados, por favor aguarde...");

            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CadastroDeFornecedoresContext>())
                {
                    if (!context.Database.CanConnect())
                        context.Database.Migrate();
                }
            }

            Console.WriteLine("Base de dados criada com sucesso!");
        }
    }
}
