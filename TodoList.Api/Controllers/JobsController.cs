using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Entities;
using TodoList.Api.Repositories;
using TodoList.ViewModel;

namespace TodoList.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class JobsController : ControllerBase
	{
		private readonly IJobRepository _jobRepository;

		public JobsController(IJobRepository jobRepository)
		{
			_jobRepository = jobRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var jobs = await _jobRepository.GetListAsync();

			return Ok(jobs);
		}

		[HttpGet("{jobId}")]
		public async Task<IActionResult> GetById(Guid jobId)
		{
			var job = await _jobRepository.GetByIdAsync(jobId);

			if (job == null)
				return NotFound($"Cannot find job with id: {jobId}");

			return Ok(job);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] JobCreateRequest request)
		{
			var jobs = await _jobRepository.CreateAsync(request);

			return CreatedAtAction(nameof(GetById), new{jobId=jobs.Id}, jobs);
		}

		[HttpPut("{jobId}")]
		public async Task<IActionResult> Update(Guid jobId, JobUpdateRequest request)
		{
			//k cần thiết trong API controller

			//if (!ModelState.IsValid)
			//	return BadRequest(ModelState);
			var jobFromDb = await _jobRepository.GetByIdAsync(jobId);

			if (jobFromDb == null)
				return NotFound($"Cannot find job with id: {jobId}");
			if (jobId != request.Id)
			{
				return BadRequest("Id not match");
			}

			var result = await _jobRepository.UpdateAsync(request);

			return Ok(result);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid jobId)
		{
			var jobFromDb = await _jobRepository.GetByIdAsync(jobId);

			if (jobFromDb == null)
				return NotFound($"Cannot find job with id: {jobId}");

			var result = await _jobRepository.DeleteAsync(jobId);

			return Ok(result);
		}
	}
}
