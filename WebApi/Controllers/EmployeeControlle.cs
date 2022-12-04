
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class EmployeeController
{
    private EmployeeService _employeeService;
    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<Response<List<Employee>>> GetEmployees()
    {
        return await _employeeService.GetEmployees();
    }

    [HttpPost]
    public async Task<Response<GetEmployee>> AddEmployee(Employee employee)
    {
        return await _employeeService.AddEmployee(employee);
    }

    [HttpPut]
    public async Task<int> UpdateEmployee(Employee employee)
    {
        return await _employeeService.UpdateEmployee(employee);
    }
    
    [HttpDelete]
    public async Task<int> DeleteEmployee(int id)
    {
        return await _employeeService.DeleteEmployee(id);
    }
}

