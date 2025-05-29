//using CredWiseAdmin.Core.Entities;
//using CredWiseAdmin.Repository.Implementation;
//using CredWiseAdmin.Repository.Interfaces;
//using CredWiseAdmin.Services.Implementation;
//using CredWiseAdmin.Services.Interfaces;
//using CredWiseAdmin.Services.Mappings;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using CredWiseAdmin.Data.Repositories.Implementations;
//using CredWiseAdmin.Data.Repositories.Interfaces;
//using CredWiseAdmin.Middleware;
//using System.Reflection;
//using CredWiseAdmin.Repository;
//using CredWiseAdmin.Services.Services;
//using CredWiseAdmin.Repository.Repositories;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container
//builder.Services.AddControllers()
//    .AddNewtonsoftJson(options =>
//    {
//        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//    });

//// Configure DbContext
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Register repositories
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<ILoanProductRepository, LoanProductRepository>();
////builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
//builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
//builder.Services.AddScoped<ILoanBankStatementRepository, LoanBankStatementRepository>();
//builder.Services.AddScoped<ILoanRepaymentRepository, LoanRepaymentRepository>();
//builder.Services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
//builder.Services.AddScoped<IFDRepository, FDRepository>();
//builder.Services.AddScoped<ILoanEnquiryRepository, LoanEnquiryRepository>();


//// Register services
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<ILoanProductService, LoanProductService>();
////builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
//builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
//builder.Services.AddScoped<ILoanRepaymentService, LoanRepaymentService>();
//builder.Services.AddScoped<IFDService, FDService>();
//builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddScoped<IFileStorageService>(provider =>
//    new FileStorageService(builder.Configuration["FileStorage:BasePath"]));
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ILoanEnquiryService, LoanEnquiryService>();


//// Configure AutoMapper
//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//builder.Services.AddHttpContextAccessor();
//// Configure JWT authentication
//var jwtSettings = builder.Configuration.GetSection("Jwt");
//var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero
//    };
//});

//// =============================================
//// SWAGGER CONFIGURATION (UPDATED SECTION)
//// =============================================
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo 
//    { 
//        Title = "CredWise Admin API", 
//        Version = "v1",
//        Description = "API for CredWise administration including email services"
//    });

//    // Add JWT Bearer authentication to Swagger
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });

//    // Include XML comments if available
//    try
//    {
//        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//        if (File.Exists(xmlPath))
//        {
//            c.IncludeXmlComments(xmlPath);
//        }
//    }
//    catch { /* Ignored */ }
//});
//// =============================================

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("*",
//        policy => policy.AllowAnyOrigin()
//                        .AllowAnyHeader()
//                        .AllowAnyMethod());
//});

//var app = builder.Build();

//// =============================================
//// SWAGGER UI CONFIGURATION (UPDATED SECTION)
//// =============================================
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CredWise API v1");
//    c.RoutePrefix = "swagger"; // Changed from string.Empty to explicit "swagger"
//    c.DisplayRequestDuration(); // New: Show request duration
//    c.EnableTryItOutByDefault(); // New: Enable "Try it out" by default
//});
//// =============================================

//// Apply database migrations
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<AppDbContext>();
//        context.Database.Migrate();
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while migrating the database.");
//    }
//}
//app.UseMiddleware<ExceptionMiddleware>();

//app.UseHttpsRedirection();

//// Enable CORS
//app.UseCors("*");
//app.UseCors(x => x
//    .AllowAnyOrigin()
//    .AllowAnyMethod()
//    .AllowAnyHeader());

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Implementation;
using CredWiseAdmin.Repository.Interfaces;
using CredWiseAdmin.Services.Implementation;
using CredWiseAdmin.Services.Interfaces;
using CredWiseAdmin.Services.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using AutoMapper;
using CredWiseAdmin.Repository.Repositories;
using CredWiseAdmin.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Configure DbContext with retry policy
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null));
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

// Register repositories and services
builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();

// Configure AutoMapper with validation
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
    cfg.ConstructServicesUsing(type => ActivatorUtilities.CreateInstance(provider, type));
}).CreateMapper());

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
if (string.IsNullOrEmpty(jwtSettings["Secret"]))
{
    throw new InvalidOperationException("JWT Secret is not configured");
}

var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception}");
                return Task.CompletedTask;
            }
        };
    });

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CredWise Admin API",
        Version = "v1",
        Description = "API for CredWise administration"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CredWise API v1");
    c.RoutePrefix = "swagger";
    c.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
    {
        ["activated"] = false
    };
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Add detailed error pages in development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Global error handler middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            status = false,
            message = "An error occurred while processing your request.",
            error = app.Environment.IsDevelopment() ? ex.Message : null,
            stackTrace = app.Environment.IsDevelopment() ? ex.StackTrace : null
        }));
    }
});

// Apply migrations
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
    Console.WriteLine("Database migrations applied successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error applying migrations: {ex.Message}");
}

app.Run();