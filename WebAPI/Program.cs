using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;
using WebAPI.ResponseModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddScoped<GenericResponseMethod>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("TokenController", new OpenApiInfo
    {
        Title = "ControllerToken",
        Version = "v1",
    });
    c.SwaggerDoc("UserController", new OpenApiInfo
    {
        Title = "UserExpenseTracker",
        Version = "v1"
    });
    c.SwaggerDoc("UserExpenseCategoryController", new OpenApiInfo
    {
        Title = "UserExpenseCategory",
        Version = "v1"
    });
    c.SwaggerDoc("UserExpenseController", new OpenApiInfo
    {
        Title = "UserExpense",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "User Authentication",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/TokenController/swagger.json", "TokenController");
        c.SwaggerEndpoint("/swagger/UserController/swagger.json", "UserController");
        c.SwaggerEndpoint("/swagger/UserExpenseCategoryController/swagger.json", "UserExpenseCategoryController");
        c.SwaggerEndpoint("/swagger/UserExpenseController/swagger.json", "UserExpenseController");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
