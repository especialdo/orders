using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Shared.Entities;

namespace Orders.Frontend.Pages.Categories
{
    public partial class CategoriesIndex
    {
        [Inject] private IRepository repository { get; set; } = null!;
        public List<Category>? Categories { get; set; }
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        protected async override Task OnInitializedAsync()
        {
            await LoadAsync();

        }

        private async Task LoadAsync()
        {
            await base.OnInitializedAsync();
            var responseHttp = await repository.GetAsync<List<Category>>("api/Categories");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Categories = responseHttp.Response;
        }

        private async Task DeleteAsync(Category category)
        {
            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmacion",
                Text = $"¿Estás seguro de querer borrar el pais: {category.Name}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm) { return; }

            var responseHttp = await repository.DeleteAsync<Country>($"api/categories/{category.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/categories");
                }
                else
                {
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadAsync();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            }); ;

            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con exito");

        }
    }
}
