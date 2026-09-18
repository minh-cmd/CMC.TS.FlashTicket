
using CMC.TS.FT.Api.Data;
using CMC.TS.FT.Api.Repositories;
using CMC.TS.FT.Api.Repositories.GenericRepository;
using CMC.TS.FT.Api.Repositories.IRepositories;
using CMC.TS.FT.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Microsoft.OpenApi ;
using System.Runtime;
using System.Text;

namespace CMC.TS.FT.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //phần builder.Services.AddSwaggerGen gen 100% từ AI, không hiểu
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CMC.TS.FT.Api", Version = "v1" });

                // 1. Pass the concrete OpenApiSecurityScheme object directly
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter your JWT Bearer token below.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                options.AddSecurityDefinition("Bearer", securityScheme);

                // 2. Pass a lambda delegate (doc) => ... for AddSecurityRequirement
                options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("Bearer", doc),
                        new List<string>()
                    }
                });
            });
            builder.Services.AddDbContext<SQLServerDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<PermissionService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<RoleService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UserService>();

            builder.Services.AddScoped<AuthService>();

            builder.Services.AddAuthentication(ops =>
            {
                ops.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                ops.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JwtSetting:Issuer"],
                ValidAudience = builder.Configuration["JwtSetting:Audience"],
                // If using RS256, pass your RSA Public Key here instead of Symmetric Security Key
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JwtSetting:SecretKey"]!))
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
