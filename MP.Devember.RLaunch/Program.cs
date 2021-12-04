using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MP.Blazor.Library;
using MP.Blazor.Library.Models;
using MP.Devember.RLaunch;
using MP.Devember.RLaunch.Models;
using MP.SimpleTokens.Token.Clients;
using Refit;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBaseLibrary(
    new SiteDescription
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
    }
);

builder.Services
    .AddRefitClient<IBasicDetailsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ApiClients:Token"] ?? ""));

builder.Services.Configure<FeaturedToken>(builder.Configuration.GetSection(nameof(FeaturedToken)));

await builder.Build().RunAsync();
