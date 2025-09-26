using LMS.MiddleWare;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management System",
        Version = "v1",
        Description = "This Library Management System API is designed to automate and simplify common library operations.\n\n" +
        " It provide endpoint to perform following operations: \n\n" +
        "Add Update Delete and get all Students.\n\n" +
        "Add Update Delete and get all Books.\n\n" +
        "Keep Record of issuing books & also returning books."
    });

    // 🔑 Add API Key definition
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        Description = "Enter your API key"
    });

    // 🔑 Require API key for all endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                    
                }
            },
            Array.Empty<string>()
        }
    });

    // XML comments (optional)
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    }
});

var app = builder.Build();

// ✅ Middleware order
app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>(); // Make sure class name matches exactly
app.UseAuthorization();

// Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management System V1");

        // 🔹 Pre-fill API key in Swagger automatically (optional)
        c.DefaultModelsExpandDepth(-1); // collapse schemas
    });
}

app.MapControllers();
app.Run();
