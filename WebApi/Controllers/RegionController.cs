
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class RegionController
{
    private RegionService _regionService;
    public RegionController(RegionService regionService)
    {
        _regionService = regionService;
    }

    [HttpGet]
    public async Task<Response<List<Region>>> GetRegions()
    {
        return await _regionService.GetRegions();
    }

    [HttpPost]
    public async Task<Response<Region>> AddRegion(Region region)
    {
        return await _regionService.AddRegion(region);
    }

    [HttpPut]
    public async Task<int> UpdateRegion(Region region)
    {
        return await _regionService.UpdateRegion(region);
    }
    
    [HttpDelete]
    public async Task<int> DeleteRegion(int id)
    {
        return await _regionService.DeleteRegion(id);
    }
}

