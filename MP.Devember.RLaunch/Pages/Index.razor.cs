using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MP.Blazor.Library.Services.Interfaces;
using MP.Devember.RLaunch.Models;
using MP.SimpleTokens.Token.Clients;
using MP.SimpleTokens.Token.Contracts;
using Refit;

namespace MP.Devember.RLaunch.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private IBasicDetailsClient? TokenClient { get; set; }

        [Inject]
        public IOptions<FeaturedToken>? TokenConfiguration { get; set; }

        [Inject]
        public ITranslationService? TranslationService { get; set; }

        [Inject]
        private ILogger<Index>? Logger { get; set; }

        private ITranslationService TS { get => TranslationService == null ? throw new NullReferenceException() : TranslationService; }

        public PublicTokenSummary TokenSummary { get; set; } = new PublicTokenSummary();

        public bool Loading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            var tokenId = TokenConfiguration!.Value.Id;

            try
            {
                TokenSummary = await TokenClient!.GetSummary(tokenId!);
            }
            catch (ApiException ex)
            {
                Logger!.LogError(ex, "Error encountered fetching data from API. Displaying error image.");

                TokenSummary = new PublicTokenSummary
                {
                    Title = "Hidden level: error state",
                    Image = "/images/rip.png",
                    Description = "Unfortunately the NFT is currently unavailable... Oh no. Feel free to DM me on the Level One forums (User: negazippy) to investigate."
                };
            }

            await base.OnInitializedAsync();

            Loading = false;
        }
    }
}
