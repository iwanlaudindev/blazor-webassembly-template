using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Water.Management.Application;
using Water.Management.Client;
using Microsoft.AspNetCore.Components.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// adding application services
var applicationSettings = builder.Configuration.GetSection("ApplicationSettings");
builder.Services.AddApplication(applicationSettings.Get<ApplicationSettings>() 
    ?? new ApplicationSettings());

await builder.Build().RunAsync();
