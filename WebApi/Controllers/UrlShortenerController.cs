using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlMappingService _service;
        public UrlShortenerController(IUrlMappingService service)
        {
            _service = service;
        }
        [HttpPost("/makeItShort")]
        public async Task<IActionResult> MakeUrlShort(string url)
        {
            var response = await _service.MakeItShort(url);
            return Ok(response);
        }
    }

}
