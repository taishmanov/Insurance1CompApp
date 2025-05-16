using Microsoft.AspNetCore.Mvc;
using Insurance1CompApp.Models;
using Insurance1CompAppServices;
using Insurance1CompAppServices.Exceptions;
using Microsoft.Extensions.Logging;

namespace Insurance1CompAppApi.Controllers
{
    [ApiController]
    [Route("server/api/gwp")]
    public class CountryGwpController : ControllerBase
    {
        private readonly CountryGwpService _service;
        private readonly ILogger<CountryGwpController> _logger;

        public CountryGwpController(CountryGwpService service, ILogger<CountryGwpController> logger)
        {
            _service = service;
            _logger = logger;
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

            try
            {
                var result = await _service.GetAverageGwpAsync(request.Country, request.Lob);
                return Ok(result);
            }
            catch (CalculationException ex)
            {
                _logger.LogError(ex, "Calculation error in PostAvg: {Message}", ex.Message);
                return BadRequest("There was a problem calculating the average GWP. Please check your input and try again.");
            }
            catch (DataRetrievalException ex)
            {
                _logger.LogError(ex, "Data retrieval error in PostAvg: {Message}", ex.Message);
                return StatusCode(500, "There was a problem retrieving the data. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in PostAvg: {Message}", ex.Message);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
