using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Models
{
    public class Modal : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Message { get; set; }
        protected bool Confirm { get; set; }

        public void Show()
        {
            Confirm = true;
            StateHasChanged();
        }
        [Parameter]
        public EventCallback<bool> ConfirmChanged { get; set; }

        protected async Task OnConfirmChange(bool value)
        {
            Confirm = false;
            await ConfirmChanged.InvokeAsync(value);
        }

    }
}