
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class JobHistoryeController
{
    private JobHistoryService _jobhistoryService;
    public JobHistoryeController(JobHistoryService jobhistoryService)
    {
        _jobhistoryService = jobhistoryService;
    }

    [HttpGet]
    public async Task<Response<List<JobHistory>>> GetJobHistorys()
    {
        return await _jobhistoryService.GetJobHistorys();
    }

    [HttpPost]
    public async Task<Response<JobHistory>> AddJobHistory(JobHistory jobhistory)
    {
        return await _jobhistoryService.AddJobHistory(jobhistory);
    }

    [HttpPut]
    public async Task<int> UpdateJobHistory(JobHistory jobhistory)
    {
        return await _jobhistoryService.UpdateJobHistory(jobhistory);
    }
    
    [HttpDelete]
    public async Task<int> DeleteJobHistory(int id)
    {
        return await _jobhistoryService.DeleteJobHistory(id);
    }
}

