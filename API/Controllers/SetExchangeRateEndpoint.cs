using API.Helpers;
using Ardalis.ApiEndpoints;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
namespace API.Controllers;


[Route("SetExchangeRate")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = "Admin")]
public class SetExchangeRateEndpoint : EndpointBaseAsync.WithRequest<CurrencyExchangeRateDto>.WithResult<IActionResult>
{
    private readonly IDistributedCache _cache;
    private readonly IConfiguration _config;

    public SetExchangeRateEndpoint(IDistributedCache cache, IConfiguration config)
    {
        _cache = cache;
        _config = config;
    }

    [HttpPost]
    [CheckModelState]
    public async override Task<IActionResult> HandleAsync([FromBody] CurrencyExchangeRateDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var expirationTime = TimeSpan.FromMinutes(_config.GetValue<int>("Redis:ExpirationTimeMinutes"));

            var serializedRate = JsonSerializer.Serialize(request);

            await _cache.SetStringAsync(request.Currency, serializedRate, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            });

            return Ok(serializedRate);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"An error occurred while saving the currency exchange rate: {ex.Message}");
        }

    }

}




