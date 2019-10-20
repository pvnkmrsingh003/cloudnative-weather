using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace cloudnative_weather.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IOptions<Settings> _options;
		private readonly IHttpClientFactory _httpClientFactory;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<Settings> options, 
			IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_options = options;
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public async Task<WeatherResponse> Get()
		{
			var client = _httpClientFactory.CreateClient("Weather");
			var request = $"{_options.Value.uri}?zip={_options.Value.location}&appid={_options.Value.token}";
			
			var response = await client.GetStringAsync(request);
			return JsonSerializer.Deserialize<WeatherResponse>(response); 
		}
	}
}
