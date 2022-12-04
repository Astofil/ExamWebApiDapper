
using Dapper;
using Npgsql;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class JobService
{
    private readonly DapperContext _context;
    public JobService(DapperContext context)
    {
        _context = context;
    }
    public  async Task<Response<List<Job>>> GetJobs()
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"select * from jobs";
            var result = await conn.QueryAsync<Job>(sql);
            return new Response<List<Job>>(result.ToList());
        }
    }
    public async Task<Response<Job>> AddJob(Job job)
    {
        try
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"insert into jobs(jobtitle as JobTitle, minsalary as MinSalary, maxsalary as Maxsalary) Values('{job.JobTitle}',{job.MinSalary},{job.MaxSalary},')";
                var result = await conn.ExecuteAsync(sql);
                job.Id = result;
                return new Response<Job>(job);
            }
        }
        catch(Exception ex)
        {
            return new Response<Job>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<int> UpdateJob(Job job)
    {
        using ( var conn = _context.CreateConnection())
        {
            var sql = $"update jobs set jobtitle = '{job.JobTitle}', minsalary = {job.MinSalary}, maxsalary = {job.MaxSalary}";  //pay attention to the types(string, int, decimal, datetime);
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
    public async Task<int> DeleteJob(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"delete from jobs where id = {id}";
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
}
