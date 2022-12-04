
using Dapper;
using Npgsql;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class LocationService
{
    private readonly DapperContext _context;
    public LocationService(DapperContext context)
    {
        _context = context;
    }
    public  async Task<Response<List<Location>>> GetLocations()
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"select * from locations";
            var result = await conn.QueryAsync<Location>(sql);
            return new Response<List<Location>>(result.ToList());
        }
    }
    public async Task<Response<Location>> AddLocation(Location location)
    {
        try
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"insert into locations(streetaddress as StreetAddress, postalCode as PostalCode, city as City, stateprovince as StateProvince, countryId as CountryId) Values('{location.StreetAddress}',{location.PostalCode}, '{location.City}','{location.StateProvince}', {location.CountryId},";
                var result = await conn.ExecuteAsync(sql);
                location.Id = result;
                return new Response<Location>(location);
            }
        }
        catch(Exception ex)
        {
            return new Response<Location>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<int> UpdateLocation(Location location)
    {
        using ( var conn = _context.CreateConnection())
        {
            var sql = $"update locations set streetaddress = '{location.StreetAddress}', postalCode = {location.PostalCode}, city = '{location.City}', stateprovince = '{location.StateProvince}', countryId = {location.CountryId},";  //pay attention to the types(string, int, decimal, datetime);
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
    public async Task<int> DeleteLocation(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var sql = $"delete from locations where id = {id}";
            var result = await conn.ExecuteAsync(sql);
            return result;
        }
    }
}
