using Microsoft.AspNetCore.Mvc;
using Insurance1CompApp.Models;
using Insurance1CompAppServices;

namespace Insurance1CompAppApi.Controllers
{
    [ApiController]
    [Route("server/api/gwp")]
    public class CountryGwpController : ControllerBase
    {
        private readonly CountryGwpService _service;

        public CountryGwpController(CountryGwpService service)
        {
            _service = service;
        }

        /// <summary>
        /// Return an average gross written premium (GWP) over 2008-2015 period for the selected lines of business.
        /// </summary>
        /// <param name="request">country and one or more lines of business (LOB)</param>
        /// <returns>average gross written premium (GWP) over 2008-2015 period</returns>
        [HttpPost("avg")]
        public async Task<ActionResult<GwpResponse>> PostAvg([FromBody] GwpRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Country) || request.Lob == null || request.Lob.Count == 0)
                return BadRequest("Invalid input");

            var result = await _service.GetAverageGwpAsync(request.Country, request.Lob);
            return Ok(result);
        }
    }
}
