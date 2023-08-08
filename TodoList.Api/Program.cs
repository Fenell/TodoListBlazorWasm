using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoList.Api.Data;
using TodoList.Api.Entities;
using TodoList.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add framework service
builder.Services.AddDbContext<TodoListDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
});

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<TodoListDbContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
	option.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secretkey"]))
	};
});


builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IUserReposititory, UserRepository>();

// Add Cors
builder.Services.AddCors(a => a.AddPolicy("myPolicy", opt =>
{
	opt.AllowAnyHeader();
	opt.AllowAnyMethod();
	opt.AllowAnyOrigin();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("myPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
