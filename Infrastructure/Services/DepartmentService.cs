
using Dapper;
using Npgsql;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;
// Department
public class DepartmentService
{
    private readonly DapperContext _context;
    public DepartmentService(DapperContext context)
    {
        _context = context;
    }
    public  async Task<Response<List<Department>>> GetDepartments()
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"select * from departments";
            var result = await conn.QueryAsync<Department>(sql);
            return new Response<List<Department>>(result.ToList());
        }
    }
    public async Task<Response<Department>> AddDepartment(Department department)
    {
        try
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"insert into departments(departmentName as DepartmentName, managerid as ManagerId, locationid as LocationId) Values('{department.DepartmentName}',{department.ManagerId},{department.LocationId})";
                var result = await conn.ExecuteAsync(sql);
                department.Id = result;
                return new Response<Department>(department);
            }
        }
        catch(Exception ex)
        {
            return new Response<Department>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<int> UpdateDepartment(Department department)
    {
        using ( var conn = _context.CreateConnection())
        {
            var sql = $"update departments set departmentName = '{department.DepartmentName}', managerid = {department.ManagerId}, locationid = {department.LocationId},";  //pay attention to the types(string, int, decimal, datetime);
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
    public async Task<int> DeleteDepartment(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"delete from departments where id = {id}";
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
}
