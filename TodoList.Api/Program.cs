using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Cors
builder.Services.AddCors(a => a.AddPolicy("myPolicy", opt =>
{
	opt.AllowAnyHeader();
	opt.AllowAnyMethod();
	opt.AllowAnyOrigin();
}));

// Add framework service
builder.Services.AddDbContext<TodoListDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
});

builder.Services.AddScoped<IJobRepository, JobRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("myPolicy");
app.MapControllers();

app.Run();
