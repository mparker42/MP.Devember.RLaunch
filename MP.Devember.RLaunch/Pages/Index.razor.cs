using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MP.Devember.RLaunch.Models;
using MP.SimpleTokens.Token.Clients;
using MP.SimpleTokens.Token.Contracts;

namespace MP.Devember.RLaunch.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private IBasicDetailsClient TokenClient { get; set; }

        [Inject]
        public IOptions<FeaturedToken> TokenConfiguration { get; set; }

        public PublicTokenSummary TokenSummary { get; set; } = new PublicTokenSummary();

        protected override async Task OnInitializedAsync()
        {
            var tokenId = TokenConfiguration.Value.Id;

            TokenSummary = await TokenClient.GetSummary(tokenId);
        }
    }
}
