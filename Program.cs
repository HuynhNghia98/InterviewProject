using GroupManagement.DataAccess.Data;
using GroupManagement.DataAccess.DbInitializer;
using GroupManagement.DataAccess.Repository;
using GroupManagement.DataAccess.Repository.IRepository;
using GroupManagement.Models;
using GroupManagement.Services.Auth;
using GroupManagement.Services.Auth.Interfaces;
using GroupManagement.Services.User;
using GroupManagement.Services.User.Interfaces;
using GroupManagement.Services.Weather;
using GroupManagement.Services.Weather.Interfaces;
using GroupManagement.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using WareHouseManagement.Services.Auth;

namespace GroupManagement
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			//dbcontext
			builder.Services.AddDbContext<ApplicationDbContext>
				(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			//identity dbcontext
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

			//config password
			builder.Services.Configure<IdentityOptions>(ops =>
			{
				ops.Password.RequireDigit = false;
				ops.Password.RequiredLength = 1;
				ops.Password.RequireUppercase = false;
				ops.Password.RequireLowercase = false;
				ops.Password.RequireNonAlphanumeric = false;
			});

			//config authentication
			var key = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");
			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
					ValidateIssuer = false,
					ValidateAudience = false,
				};
			});

			//truyền dữ liệu từ appsetting vào trong lớp MailSettings theo đúng tên các thuộc tính.
			builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

			//builder.Services.AddCors();
			builder.Services.AddCors();

			//register services
			builder.Services.AddScoped<IDbInitializer, DbInitializer>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IUserServices, UserServices>();
			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
			builder.Services.AddScoped<IMailService, MailService>();
			builder.Services.AddScoped<IWeatherService, WeatherService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			SeedData();

			app.Run();

			void SeedData()
			{
				using (var scope = app.Services.CreateScope())
				{
					var dbInititalizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
					dbInititalizer.Initializer();
				}
			}
		}
	}
}