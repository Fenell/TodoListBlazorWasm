using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using TodoList.ViewModel;
using TodoList.ViewModel.Jobs;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
    public partial class JobCreate
    {
        [Inject] private IJobApiClient JobApiClient { get; set; }
        [Inject] private IUserApiClient UserApiClient { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }

        private List<UserDto>? userDtos;
        private JobCreateRequest jobCreate = new JobCreateRequest();

        protected override async Task OnInitializedAsync()
        {
            userDtos = await UserApiClient.GetUserList();
        }

        private async Task SubmitJob(EditContext context)
        {
            var result = await JobApiClient.CreateJob(jobCreate);

            if (result.IsSuccess)
            {
                ToastService.ShowSuccess($"{jobCreate.Name} đã tạo thành thành công");
                NavigationManager.NavigateTo("/job-list");
                Snackbar.Add("Thêm thành công", Severity.Success, options => { options.ShowTransitionDuration = 300; });
            }
            else
            {
                ToastService.ShowError("Có lỗi xảy ra");
            }
        }
    }
}
