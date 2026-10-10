using Chat.Client;
using Chat.Client.Repositories;
using Chat.Client.Repositories.Contract;
using D20Tek.Blazor.BrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7109/") });

builder.Services.AddBrowserStorage();
builder.Services.AddScoped<IUserIntegration, UserIntegration>();

await builder.Build().RunAsync();
