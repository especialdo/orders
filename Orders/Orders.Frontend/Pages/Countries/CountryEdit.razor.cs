using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Shared.Entities;

namespace Orders.Frontend.Pages.Countries
{
    public partial class CountryEdit
    {
        private Country country;

        private CountryForm? countryForm;
        [Inject] private IRepository repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [EditorRequired, Parameter]public int Id { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            var responseHttp = await repository.GetAsync<Country>($"/api/countries/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/countries");
                }
                else
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                }
            }
            else {
                country = responseHttp.Response!;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await repository.PutAsync("/api/countries", country);
            if (responseHttp.Error) {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message);
                return;
            }
            Return();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            }); ;

            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con exito");
        }

        private void Return()
        {
            countryForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/countries");
        }

    }

    
}