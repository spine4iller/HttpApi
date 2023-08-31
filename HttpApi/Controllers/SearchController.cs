using Api.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HttpApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SearchController : Controller
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> Search([FromBody] SearchRequest request)
    {
        var response = await _searchService.SearchAsync(request, default);

        return StatusCode(StatusCodes.Status200OK, response);
    }
    
    [HttpGet("ping")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Ping()
    {
        bool isAvailable = await _searchService.IsAvailableAsync(default);

        return isAvailable ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
    }
}

