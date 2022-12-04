
using Dapper;
using Npgsql;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class EmployeeService
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _hosting;
    public EmployeeService(DapperContext context, IWebHostEnvironment hosting)
    {
        _context = context;
        _hosting = hosting;
    }
    public  async Task<Response<List<Employee>>> GetEmployees()
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"select * from employees";
            var result = await conn.QueryAsync<Employee>(sql);
            return new Response<List<Employee>>(result.ToList());
        }
    }
    public async Task<Response<GetEmployee>> AddEmployee(Employee employee)
    {
        try
        {
            using (var conn = _context.CreateConnection())
            {
                var path = Path.Combine(_hosting.WebRootPath, "employeeimages", employee.File.FileName);
            
                using (var stream = File.Create(path))
                {
                    await employee.File.CopyToAsync(stream);
                }
                var insertedId = await conn.ExecuteScalarAsync<int>($"insert into employees(firstname as FirstName, lastname as LastName, email as Email, phonenumber as PhoneNumber, departmentId as DepartmentId, managerId as ManagerId, commission as Commission, salary as Salary, jobId as JobId, hiredate as HireDate) Values('{employee.FirstName}','{employee.LastName}','{employee.Email}','{employee.PhoneNumber}', '{employee.DepartmentId}', '{employee.ManagerId}', '{employee.Commission}', '{employee.Salary}', '{employee.JobId}', '{employee.HireDate}')");
                employee.Id = insertedId;
                var response = new GetEmployee()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    DepartmentId = employee.DepartmentId,
                    ManagerId = employee.ManagerId,
                    Commission = employee.Commission,
                    Salary = employee.Salary,
                    JobId = employee.JobId,
                    HireDate = employee.HireDate,
                    FileName = employee.File.FileName
                };
                return new Response<GetEmployee>(response);
                // var sql = $"insert into employees(firstname as FirstName, lastname as LastName, email as Email, phonenumber as PhoneNumber, departmentId as DepartmentId, managerId as ManagerId, commission as Commission, salary as Salary, jobId as JobId, hiredate as HireDate) Values('{employee.FirstName}','{employee.LastName}','{employee.Email}','{employee.PhoneNumber}', '{employee.DepartmentId}', '{employee.ManagerId}', '{employee.Commission}', '{employee.Salary}', '{employee.JobId}', '{employee.HireDate}')";
                // var result = await conn.ExecuteAsync(sql);
                // employee.Id = result;
                // return new Response<Employee>(employee);
            }
        }
        catch(Exception ex)
        {
            return new Response<GetEmployee>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<int> UpdateEmployee(Employee employee)
    {
        using ( var conn = _context.CreateConnection())
        {
            var sql = $"update employees set firstname = '{employee.FirstName}', lastname = '{employee.LastName}', email = '{employee.Email}', phonenumber = '{employee.PhoneNumber}', departmentId = {employee.DepartmentId}, managerId = {employee.ManagerId}, commission = {employee.Commission}, salary = {employee.Salary}, jobId = {employee.JobId}, hiredate = {employee.HireDate},";  //pay attention to the types(string, int, decimal, datetime);
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
    public async Task<int> DeleteEmployee(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"delete from employees where id = {id}";
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
}
