using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using TodoList.ViewModel;
using TodoList.ViewModel.Jobs;
using TodoList.ViewModel.SeedWork;
using TodoListBlazorWasm.Components;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
    public partial class JobList
	{
		[Inject] private IJobApiClient JobApiClient { get; set; }
		[Inject] private IUserApiClient UserApiClient { get; set; }
		[Inject] private IDialogService DialogService { get; set; }

		private List<JobDto>? jobDtos;
		private JobListSearch JobSearch = new JobListSearch();
		private DialogOptions dialogOptions = new DialogOptions() { FullWidth = true };
		private List<UserDto>? userDtos;
		private bool visible;
		private Guid JobId { get; set; }
		private string assignId { get; set; }
		private MetaData MetaData { get; set; }

		protected override async Task OnInitializedAsync()
		{
            await GetJob();
            var response = await UserApiClient.GetUserList();
			userDtos = response;
		}

		public async Task SearchJob(JobListSearch jobSearch)
		{
			JobSearch = jobSearch;
            await GetJob();
        }

		private async Task OnButtonClicked(Guid id)
		{
			bool? result = await DialogService.ShowMessageBox(
				"Warning",
				"Deleting can not be undone!",
				yesText: "Delete!", cancelText: "Cancel");
			if (result == true)
            {
                await GetJob();
            }
			StateHasChanged();
		}

		private void OpenDialog(Guid jobId)
		{
			visible = true;
			JobId = jobId;
		}


		private async Task UpdateAssignJob()
		{
			var assignJobUpdate = new AssignJobUpdate()
			{
				UserId = Guid.Parse(assignId)
			};
			var response =  await JobApiClient.UpdateAssignJob(JobId, assignJobUpdate);

			if (response.IsSuccess)
			{
                await GetJob();
                visible = false;
			}
		}

		private async Task GetJob()
        {
            var response = await JobApiClient.GetAllJobs(JobSearch);

            if (response.IsSuccess)
            {
                jobDtos = response.Data.Items;
				MetaData = response.Data.MetaData;
            }
        }

		private async Task PageChanged(int pageNumber)
		{
			JobSearch.PageNumber = pageNumber;
            await GetJob();
        }
    }


}
