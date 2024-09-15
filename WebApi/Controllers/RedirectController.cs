using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{*url}")]
    public class RedirectController : ControllerBase
    {
        private readonly IUrlMappingService _service;
        public RedirectController(IUrlMappingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToBaseUrl()
        {
            var requested = HttpContext.Request.Path;
            var baseUrl = await _service.FindBaseUrlByShortGuid(requested.ToString().Split('/')[1]);
            return Redirect(baseUrl.URL);
        }
    }
}
