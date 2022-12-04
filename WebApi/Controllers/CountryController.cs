
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class CountryController
{
    private CountryService _countryService;
    public CountryController(CountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<Response<List<Country>>> GetCountries()
    {
        return await _countryService.GetCountries();
    }

    [HttpPost]
    public async Task<Response<Country>> AddCountry(Country country)
    {
        return await _countryService.AddCountry(country);
    }

    [HttpPut]
    public async Task<int> UpdateCountry(Country country)
    {
        return await _countryService.UpdateCountry(country);
    }
    
    [HttpDelete]
    public async Task<int> DeleteCountry(int id)
    {
        return await _countryService.DeleteCountry(id);
    }
}

