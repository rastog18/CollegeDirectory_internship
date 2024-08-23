//Importing necessary namespaces

using System.Text;
using CRUDmvc.DataAccessLayer;
using CRUDmvc.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

//Start the pipeline for HTTP Reauest
var builder = WebApplication.CreateBuilder(args);

// Allow the pipeline to have acceess to cinfiguration settings - appSettings.json
var config = builder.Configuration;

// Configure JWT Authentication to ensure the identity of users.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

// Configure JWT Authorization to control access based on user roles and claims.
builder.Services.AddAuthorization();

// Register MVC Controllers with the dependency injection container. In MVC we use builder.Services.AddControllersWithViews();
//this enables us to make requests to Controllers, and allows Contraollers to respond to Views
builder.Services.AddControllersWithViews();

// Stuff to add for passing the token as header
builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache(); // You can use other distributed caches as well.
// Why do we use this: 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//It protects you from 500 error: Internal Server Error
//Frequent Changes: During development, you frequently change Razor views. AddRazorRuntimeCompilation() helps by reflecting changes immediately without needing a full rebuild.
builder.Services.AddControllers().AddRazorRuntimeCompilation();

// Dependency Injection for CRUD operations data layer.
//Registers the ICrudOperationDL interface with its implementation CrudOperationDL for dependency injection.
//This is why when you call ICrudOperationDL.method() the right method is called
//Why Dependecy Injection: easier unit testing and makes us to swap DataAccessLayer easily
builder.Services.AddScoped<ICrudOperationDL, CrudOperationDL>();
builder.Services.AddScoped<ILoginDL, LoginDL>();
//builder.Services.AddTransient<CustomMiddleware>();

// Configure Swagger to include JWT authentication scheme.
//In simple terms, AddEndpointsApiExplorer tells your application to keep track
//of all the places (endpoints) where it can receive API requests. This makes it easier to
//create a detailed map of your API for developers to understand and use.

//Swagger Code:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Add this line to enable Swagger generation

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware for HTTPS redirection
app.UseHttpsRedirection();

// Middleware for session handling
app.UseSession(); // Ensure this line is added for session handling

// Middleware for routing: This middleware is essential for routing requests to the correct destination.
app.UseRouting();
app.UseMiddleware<CustomMiddleware>();

// Middleware for setting authorization header
//MiddleWare has access to both incoming and outgoing requests.

app.UseMiddleware<AuthorizationHeaderMiddleware>(); // Register your middleware here
//app.UseMiddleware<CustomMiddleware>();

// Middleware for authenticating users.
app.UseAuthentication();

// Middleware for authorizing users based on their roles and claims.
app.UseAuthorization();

// Define MVC route patterns
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map controllers to routes.
//Maps attribute-routed controllers to their respective endpoints. Ensures that the routing framework recognizes and processes the controllers correctly meaing correct reuqest is passed to correct
//controoler
app.MapControllers();

app.Run();
//TODO: Add different Users limiting their access.