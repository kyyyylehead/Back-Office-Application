using Microsoft.AspNetCore.Components;

namespace BlzLogin.Components
{
    public class RedirectToLogin : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                NavigationManager.NavigateTo("/login");
            }
            catch
            { }
        }
    }
}
