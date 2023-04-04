
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Services;
using Services.Auth;
using Services.Image;
using System.Text;
using System.Text.Json.Serialization;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000");
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyHeader();
                                  });
            });

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(c =>
                {
                    c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddDbContext<PBContext>(config =>
            {
                config.UseSqlServer(builder.Configuration.GetConnectionString("PBConnection"));
            });

            builder.Services.AddScoped<PBContext, PBContext>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                var key = Encoding.Default.GetBytes(builder.Configuration["TokenSecret"]);

                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = "hortagram",
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                };
            });

            builder.Services.AddAzureClients(client =>
            {
                client.AddBlobServiceClient(builder.Configuration["ConnectionStrings:Storage:blob"]);
                client.AddQueueServiceClient(builder.Configuration["ConnectionStrings:Storage:queue"]);
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}