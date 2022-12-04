
using Dapper;
using Npgsql;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class RegionService
{
    private readonly DapperContext _context;
    // private readonly IWebHostEnvironment _hosting;
    public RegionService(DapperContext context)
    {
        _context = context;
    }
    public  async Task<Response<List<Region>>> GetRegions()
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"select * from regions";
            var result = await conn.QueryAsync<Region>(sql);
            return new Response<List<Region>>(result.ToList());
        }
    }
    public async Task<Response<Region>> AddRegion(Region region)
    {
        try
        {
            using (var conn = _context.CreateConnection())
            {
                // var sql = $"insert into employees(firstname as FirstName, lastname as LastName, email as Email, phonenumber as PhoneNumber, departmentId as DepartmentId, managerId as ManagerId, commission as Commission, salary as Salary, jobId as JobId, hiredate as HireDate) Values('{employee.FirstName}','{employee.LastName}','{employee.Email}','{employee.PhoneNumber}', '{employee.DepartmentId}', '{employee.ManagerId}', '{employee.Commission}', '{employee.Salary}', '{employee.JobId}', '{employee.HireDate}')";
                var sql = $"insert into regions(regionName as RegionName) Values('{region.RegionName}')";
                var result = await conn.ExecuteAsync(sql);
                region.Id = result;
                return new Response<Region>(region);
            }
        }
        catch(Exception ex)
        {
            return new Response<Region>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<int> UpdateRegion(Region region)
    {
        using ( var conn = _context.CreateConnection())
        {
            // var sql = $"update employees set firstname = '{region.FirstName}', lastname = '{region.LastName}', email = '{region.Email}', phonenumber = '{employee.PhoneNumber}', departmentId = {employee.DepartmentId}, managerId = {employee.ManagerId}, commission = {employee.Commission}, salary = {employee.Salary}, jobId = {employee.JobId}, hiredate = {employee.HireDate},";  //pay attention to the types(string, int, decimal, datetime);
            var sql = $"update regions set regionName = '{region.RegionName}'";
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
    public async Task<int> DeleteRegion(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"delete from regions where id = {id}";
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
}
