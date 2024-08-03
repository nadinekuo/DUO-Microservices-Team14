using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TransactionMicroservice.Repository;
using TransactionMicroservice.Models;
using TransactionMicroservice.Services;
using Stubbery;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TransactionContext>(opt =>
{
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Repository registrations
builder.Services.AddScoped<IPayoutRepository, PayoutRepository>();
builder.Services.AddScoped<IDebtRepaymentRepository, DebtRepaymentRepository>();

// Unit of Work registration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Service registrations
builder.Services.AddScoped<IPayoutService, PayoutService>();
builder.Services.AddScoped<IDebtRepaymentService, DebtRepaymentService>();

builder.Services.AddHttpClient();

// Set up and start the Stubbery API stub server
var apiStub = new ApiStub();
apiStub.Post("/stub", (req, args) =>
{
    var result = new TransactionResult
    {
        Success = true,
        Message = "Payment processed successfully.",
        TransactionId = Guid.NewGuid().ToString()
    };

    // Manually serialize the result to JSON and return it as a string.
    return Task.FromResult(result);
    // Ensure the response header for content type is set to application/json if required by your framework.
});

apiStub.Start();
// Store the stub server's host and port in the configuration to use it in your services/controllers
builder.Configuration["StubServer:BaseUrl"] = $"{apiStub.Address}/stub";
// end

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transaction Microservice APIs", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Transactions Microservice APIs");
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
