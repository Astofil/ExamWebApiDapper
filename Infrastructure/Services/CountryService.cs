
using Dapper;
using Npgsql;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class CountryService
{
    private readonly DapperContext _context;
    public CountryService(DapperContext context)
    {
        _context = context;
    }
    public  async Task<Response<List<Country>>> GetCountries()
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"select * from countries";
            var result = await conn.QueryAsync<Country>(sql);
            return new Response<List<Country>>(result.ToList());
        }
    }
    public async Task<Response<Country>> AddCountry(Country country)
    {
        try
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"insert into countries(countryName as CountryName,regionId as RegionId) Values('{country.CountryName}',{country.RegionId})";
                var result = await conn.ExecuteAsync(sql);
                country.Id = result;
                return new Response<Country>(country);
            }
        }
        catch(Exception ex)
        {
            return new Response<Country>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<int> UpdateCountry(Country country)
    {
        using ( var conn = _context.CreateConnection())
        {
            var sql = $"update countries set countryName = '{country.CountryName}', regionId = {country.RegionId}";
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
    public async Task<int> DeleteCountry(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"delete from countries where id = {id}";
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
}
