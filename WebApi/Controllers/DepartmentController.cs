
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class DepartmentController
{
    private DepartmentService _departmentService;
    public DepartmentController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<Response<List<Department>>> GetDepartments()
    {
        return await _departmentService.GetDepartments();
    }

    [HttpPost]
    public async Task<Response<Department>> AddDepartment(Department department)
    {
        return await _departmentService.AddDepartment(department);
    }

    [HttpPut]
    public async Task<int> UpdateDepartment(Department department)
    {
        return await _departmentService.UpdateDepartment(department);
    }
    
    [HttpDelete]
    public async Task<int> DeleteDepartment(int id)
    {
        return await _departmentService.DeleteDepartment(id);
    }
}

