using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using astute.Models;
using astute.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace astute
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddDbContext<AstuteDbContext>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ITermsService, TermsService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProcessService, ProcessService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IPointerService, PointerService>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddCookie()
                .AddJwtBearer("Bearer", options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtToken:Issuer"],
                    ValidAudience = Configuration["JwtToken:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtToken:SecretKey"]))
                };
            });
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("corsapp");
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
