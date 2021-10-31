using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MP.Blazor.Library;
using MP.Blazor.Library.Models;
using MP.Devember.RLaunch;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBaseLibrary(new SiteDescription
{
    Pages = new[]
    {
        new NavigatablePage
        {
            Location = "/",
            TranslationKey = "exampleHomePage"
        },
        new NavigatablePage
        {
            Location = "/counter",
            TranslationKey = "exampleCounterPage"
        },
        new NavigatablePage
        {
            TranslationKey = "exampleFetchDataPageGrouping",
            SubPages = new[]
            {
                new NavigatableSubPage
                {
                    Location = "/fetchdata",
                    TranslationKey = "exampleFetchDataPage"
                }
            }
        }
    },
    TitleTranslationKey = "exampleSiteTitle",
    TranslationsLocation = new Uri(new Uri(builder.HostEnvironment.BaseAddress), "/translations/__locale__.json"),
    SupportedLocales = new[] { "en-GB" }
});

await builder.Build().RunAsync();
