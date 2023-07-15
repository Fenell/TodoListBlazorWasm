using Microsoft.AspNetCore.Components;
using TodoList.ViewModel;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
	public partial class TodoList
	{
		[Inject] private IJobApiClient JobApiClient { get; set; }
		private List<JobDto>? jobDtos;

		protected override async Task OnInitializedAsync()
		{
			jobDtos = await JobApiClient.GetAllJobs();
		}
	}
}
