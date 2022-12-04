
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class JobController
{
    private JobService _jobService;
    public JobController(JobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<Response<List<Job>>> GetJobs()
    {
        return await _jobService.GetJobs();
    }

    [HttpPost]
    public async Task<Response<Job>> AddJob(Job job)
    {
        return await _jobService.AddJob(job);
    }

    [HttpPut]
    public async Task<int> UpdateJob(Job job)
    {
        return await _jobService.UpdateJob(job);
    }
    
    [HttpDelete]
    public async Task<int> DeleteJob(int id)
    {
        return await _jobService.DeleteJob(id);
    }
}

