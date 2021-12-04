using Microsoft.AspNetCore.Components;

namespace MP.Devember.RLaunch.Shared
{
    public partial class SurveyPrompt : ComponentBase
    {
        // Demonstrates how a parent component can supply parameters
        [Parameter]
        public string? Title { get; set; }
    }
}
