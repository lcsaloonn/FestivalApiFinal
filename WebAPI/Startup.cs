using System;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Infrastructure.SqlServer.Data;
using Infrastructure.SqlServer.Repositories.Artiste;
using Infrastructure.SqlServer.Repositories.Hoodies;
using Infrastructure.SqlServer.Repositories.Music;
using Infrastructure.SqlServer.Repositories.Schedules;
using Infrastructure.SqlServer.Repositories.Ticket;
using Infrastructure.SqlServer.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using WebAPI.configuration;





namespace WebAPI
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
            services.AddControllers();
                
            //Swagger Configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FestivalAPI", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    In = ParameterLocation.Header, 
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { 
                        new OpenApiSecurityScheme 
                        { 
                            Reference = new OpenApiReference 
                            { 
                                Type =  ReferenceType.SecurityScheme,
                                Id = "Bearer" 
                            } 
                        },
                        new string[] { } 
                    } 
                });
            });
         
           
            
            //ToDo change her   
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHoodiesRepository, HoodieRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IMusicRepository, MusicRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            
            //Enable CORS for ip
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200", "https://localhost:5001",""https://localhost:5001","https://helha-festival.herokuapp.com").AllowAnyMethod().AllowAnyHeader();
            }));
            
            services.AddAuthorization(options => 
            {
                options.AddPolicy("DepartmentPolicy",
                    policy => policy.RequireClaim("department"));
            });
            //JWT
            //map the config in appsettings to configuration.jwtconfig
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddDbContext<Context>(opt => opt.UseSqlServer(Configuration.GetConnectionString("FestivalConnection")));
           
            
            
            
            //JWT
            services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt => {
                    var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = false // TOdo a modifier
                    };
                });
            services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Context>();
            
          


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FestivalAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MyPolicy"); //CORS policy
            app.UseAuthentication();//JWT
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        
        
    }
    
    
}
