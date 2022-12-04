
using Dapper;
using Npgsql;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class JobHistoryService
{
    private readonly DapperContext _context;
    public JobHistoryService(DapperContext context)
    {
        _context = context;
    }
    public  async Task<Response<List<JobHistory>>> GetJobHistorys()
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"select * from jobhistory";
            var result = await conn.QueryAsync<JobHistory>(sql);
            return new Response<List<JobHistory>>(result.ToList());
        }
    }
    public async Task<Response<JobHistory>> AddJobHistory(JobHistory jobhistory)
    {
        try
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"insert into jobhistory(startdate as StartDate, enddate as EndDate, jobId as JobId, departmentId as DepartmentId) Values({jobhistory.StartDate},{jobhistory.EndDate},{jobhistory.JobId},{jobhistory.DepartmentId}')";
                var result = await conn.ExecuteAsync(sql);
                jobhistory.Id = result;
                return new Response<JobHistory>(jobhistory);
            }
        }
        catch(Exception ex)
        {
            return new Response<JobHistory>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<int> UpdateJobHistory(JobHistory jobhistory)
    {
        using ( var conn = _context.CreateConnection())
        {
            var sql = $"update jobhistory set startdate = {jobhistory.StartDate}, enddate = {jobhistory.EndDate}, jobId = {jobhistory.JobId}, departmentId = {jobhistory.DepartmentId}";  //pay attention to the types(string, int, decimal, datetime);
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
    public async Task<int> DeleteJobHistory(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"delete from JobHistory where id = {id}";
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
}
