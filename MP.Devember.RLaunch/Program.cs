using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MP.Blazor.Library;
using MP.Blazor.Library.Models;
using MP.Devember.RLaunch;
using MP.Devember.RLaunch.Models;
using MP.SimpleTokens.Token.Clients;
using MudBlazor;
using MudBlazor.Utilities;
using Refit;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var lightThemePalette = new Palette
{
    Primary = "#005900",
    Secondary = "#F7C20C",
    Tertiary = "#BB2E16",
    Info = "#0C80DF",
    Success = "#00C853",
    Warning = "#FF9800",
    Error = "#F44336",
    TextPrimary = "#0F0F0F",
    TextSecondary = "#424242",
    TextDisabled = "#9F9F9F",
    Background = "#FAFAFA"
};

lightThemePalette.AppbarBackground = lightThemePalette.Primary;



var darkThemePalette = new Palette
{
    Black = "#27272f",
    BackgroundGrey = "#27272f",
    Surface = "#373740",
    AppbarText = "rgba(255,255,255, 0.70)",
    Primary = "#005900",
    Secondary = "#F7C20C",
    Tertiary = "#BB2E16",
    Info = "#0C80DF",
    Success = "#00C853",
    Warning = "#FF9800",
    Error = "#F44336",
    TextPrimary = "#FAFAFA",
    TextSecondary = "#E6E6E6",
    TextDisabled = "#9F9F9F",
    Background = "#242424",
    DrawerBackground = "#333333",
    ActionDisabled = "rgba(255,255,255, 0.26)",
    ActionDisabledBackground = "rgba(255,255,255, 0.12)",
    ActionDefault = "rgba(255, 255, 255, 0.54)",
    Divider = "rgba(255,255,255, 0.12)",
    DividerLight = "rgba(255,255,255, 0.06)",
    TableLines = "rgba(255,255,255, 0.12)",
    LinesDefault = "rgba(255,255,255, 0.12)",
    LinesInputs = "rgba(255,255,255, 0.3)"
};

darkThemePalette.AppbarBackground = darkThemePalette.Primary;
darkThemePalette.DrawerText = darkThemePalette.TextPrimary;



builder.Services.AddBaseLibrary(
    new SiteDescription
    {
        Pages = null,
        TitleTranslationKey = "siteTitle",
        TranslationsLocation = new Uri(new Uri(builder.HostEnvironment.BaseAddress), "/translations/__locale__.json"),
        SupportedLocales = new[] { "en-GB" },
        Themes = new Dictionary<ThemeType, SiteTheme> {
            {
                ThemeType.Dark,
                new SiteTheme
                {
                    Palette = darkThemePalette
                }
            },
            {
                ThemeType.Light,
                new SiteTheme
                {
                    Palette = lightThemePalette
                }
            }
        }
    }
);

builder.Services
    .AddRefitClient<IBasicDetailsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ApiClients:Token"] ?? ""));

builder.Services.Configure<FeaturedToken>(builder.Configuration.GetSection(nameof(FeaturedToken)));

await builder.Build().RunAsync();
