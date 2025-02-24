using ToDoApp.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Read DB connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");

// 2. Register repository (as a singleton or scoped, your choice)
builder.Services.AddSingleton<IToDoRepository>(_ => new ToDoRepository(connectionString));

// 3. Add controllers
builder.Services.AddControllers();

// 4. (Optional) Configure CORS so the Blazor client (on another port) can call this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 5. Use the named CORS policy (if configured)
app.UseCors("AllowAll");

// 6. Map your controller endpoints
app.MapControllers();

app.Run();
