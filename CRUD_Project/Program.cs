
using CRUD_Project.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:4200") // Angular app URL
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("*") // Specify the URL of the allowed origin
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });

    options.AddPolicy("AllowAnyOrigin",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable the CORS policy
app.UseCors("AllowSpecificOrigin");

// Enable the CORS policy
/*app.UseCors("AllowAngularApp");*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
