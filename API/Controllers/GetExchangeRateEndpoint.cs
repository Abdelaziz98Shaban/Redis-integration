using Ardalis.ApiEndpoints;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace API.Controllers;


[Route("GetExchangeRate")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = "Admin")]
public class GetExchangeRateEndpoint : EndpointBaseAsync.WithRequest<string>.WithResult<IActionResult>
{
    private readonly IDistributedCache _distributedCache;

    public GetExchangeRateEndpoint(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    [HttpGet]
    public async override Task<IActionResult> HandleAsync([FromQuery] string currency, CancellationToken cancellationToken = default)
    {
        try
        {
            var cachedRate = await _distributedCache.GetAsync(currency, cancellationToken);

            if (cachedRate != null)
            {

                var deserializedRate = JsonSerializer.Deserialize<CurrencyExchangeRateDto>(cachedRate);
                return Ok(deserializedRate);
            }

            return Ok(null);

        }
        catch (Exception ex)
        {

            return StatusCode(500, $"An error occurred while getting the currency exchange rate: {ex.Message}");
        }

    }

}




