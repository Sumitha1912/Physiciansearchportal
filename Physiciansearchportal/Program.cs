using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Physiciansearchportal.Data;
using Physiciansearchportal.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PhysiciansearchDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhysiciansearchConnectionString")));

builder.Services.AddScoped<IPhysicianRepository, PhysicianRepository>();
builder.Services.AddScoped<IPhotoRepository, LocalStoragePhotoRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(app.Environment.ContentRootPath, "Resources")),
    RequestPath = "/Resources"
}) ;


app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
