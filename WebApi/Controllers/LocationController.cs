
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class LocationController
{
    private LocationService _locationService;
    public LocationController(LocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<Response<List<Location>>> GetLocations()
    {
        return await _locationService.GetLocations();
    }

    [HttpPost]
    public async Task<Response<Location>> AddLocation(Location location)
    {
        return await _locationService.AddLocation(location);
    }

    [HttpPut]
    public async Task<int> UpdateLocation(Location location)
    {
        return await _locationService.UpdateLocation(location);
    }
    
    [HttpDelete]
    public async Task<int> DeleteLocation(int id)
    {
        return await _locationService.DeleteLocation(id);
    }
}

