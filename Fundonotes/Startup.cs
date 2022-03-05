using BusinessLayer.interfaces;
using BusinessLayer.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.context;
using RepositoryLayer.interfaces;
using RepositoryLayer.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundonotes
{
    public class Startup // program starts from here.
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;// configuration data may come from Json file.
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // All seevices are added here .
        {
            {
                // conneting with Database.
                services.AddDbContext<FundoContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:FundoDB"])); 
                services.AddControllers();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Welcome to FundooNotes" });

                    var securitySchema = new OpenApiSecurityScheme
                    {
                        Description = "Using the Authorization header with the Bearer scheme.",

                        Name = "Authorization",

                        In = ParameterLocation.Header,

                        Type = SecuritySchemeType.Http,

                        Scheme = "bearer",

                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,

                            Id = "Bearer"
                        }
                    };
                    c.AddSecurityDefinition("Bearer", securitySchema);
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }

                });
                });
                services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,

                        ValidateAudience = false,

                        ValidateLifetime = false,

                        ValidateIssuerSigningKey = true,
                        //Configuration["JwtToken:SecretKey"]
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecKey"])) 
                    };

                });
                services.AddTransient<IUserBL, UserBL>(); //services are created each time when they are requested for Business Layer.
                services.AddTransient<IUserRL, UserRL>(); //services are created each time when they are requested for Repository Layer.
                services.AddTransient<INotesBL, NotesBL>();
                services.AddTransient<INotesRL, NotesRL>();
                services.AddTransient<ICollaboratorBL,CollaboratorBL>();
                services.AddTransient<ICollaboratorRL, CollaboratorRL>();
                services.AddTransient<ILabelBL, LabelBL>();
                services.AddTransient<ILabelRL, LabelRL>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // Middlewares are added in configure.
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();// for Authentication @ resetpaswword 
            app.UseAuthorization();// for Authorize the token 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>

            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fundoo");

            });
        }
    }
}
