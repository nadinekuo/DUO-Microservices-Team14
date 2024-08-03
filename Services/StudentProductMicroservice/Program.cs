using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using StudentProductMicroservice.Repository;
using StudentProductMicroservice.Models;
using StudentProductMicroservice.Services;
using StudentProductMicroservice.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StudentProductContext>(opt => 
   { 
       opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
   });


// Repository registrations
builder.Services.AddScoped<IGrantRepository, GrantRepository>(); 
builder.Services.AddScoped<ILoanRepository, LoanRepository>(); 

// Unit of Work registration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// HTTPClient registration
builder.Services.AddHttpClient<GrantService>();
builder.Services.AddHttpClient<LoanService>();

// Service registrations
builder.Services.AddScoped<IGrantService, GrantService>(); 
builder.Services.AddScoped<ILoanService, LoanService>();

// Factory registrations
builder.Services.AddScoped<IGrantFactory, GrantFactory>(); 
builder.Services.AddScoped<ILoanFactory, LoanFactory>();

// Setup OpenAPI documentation
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Student Product Microservice APIs", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();

    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Product Microservice APIs");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
