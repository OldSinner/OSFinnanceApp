using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Models
{
    public class EditModal : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Message { get; set; }

        public string name { get; set; }
        protected bool Confirm { get; set; }

        public void Show()
        {
            Confirm = true;
            StateHasChanged();
        }
        [Parameter]
        public EventCallback<string> ConfirmChanged { get; set; }

        protected async Task OnConfirmChange(string value)
        {
            Confirm = false;
            await ConfirmChanged.InvokeAsync(value);
        }
    }
}