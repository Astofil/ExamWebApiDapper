// using Microsoft.AspNetCore.Http;

namespace Domain.Dtos;

public class GetEmployee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int DepartmentId { get; set; }
    public int ManagerId { get; set; }
    public decimal Commission { get; set; }
    public int Salary { get; set; }
    public int JobId { get; set; }
    public DateTime HireDate { get; set; }
    public string? FileName { get; set; }
}

