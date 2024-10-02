using Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
using SaveLogAPI.Infrastructure;
using System.Reflection;

namespace UserAuthenApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserAuthenApi(this IServiceCollection services)
        {
            services.AddHealthChecks().AddDbContextCheck<EmployeeDbContext>("Check-ConnectDB");
            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddSwaggerGen(option =>
            {
                //Generate comment
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AuthentucateApi",
                    Version = "v1",
                });

                //set Authen in Swagger
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });
                //set authen controller in Swagger
                option.AddSecurityRequirement(new OpenApiSecurityRequirement{
               {
                 new OpenApiSecurityScheme{
                 Reference = new OpenApiReference{
                 Id = "Bearer",
                 Type = ReferenceType.SecurityScheme,
                },
               },
               new List<string>()
         }
    });
            });

            return services;
        }
    }
}
