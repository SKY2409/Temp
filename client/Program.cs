using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoApp.Client;
using ToDoApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// 1. Root Blazor component
builder.RootComponents.Add<App>("#app");

// 2. Register HttpClient for your API
builder.Services.AddScoped(sp => new HttpClient
{
    // CHANGE PORT if your API runs on a different port
    BaseAddress = new Uri("http://localhost:5022/")
});

// 3. Register a typed service for ToDo operations
builder.Services.AddScoped<ToDoServices>();

await builder.Build().RunAsync();
