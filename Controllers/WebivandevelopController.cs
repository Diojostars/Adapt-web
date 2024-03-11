using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebivandevelopController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        [HttpGet]
        public IEnumerable<Webivandevelop> Get([FromQuery] WebivandevelopRequest request)
        {
            var rng = new Random();
			var webs = new List<Webivandevelop>();
			for (int i = 0; i < request.numberOfDays; i++)
			{
				var temperatureC = rng.Next(request.minValue, request.maxValue);
				var temperatureF = 32 + (int)(temperatureC / 0.5556);
				webs.Add(new Webivandevelop
                {
					Date = DateTime.Now.AddDays(i),
					TemperatureC = temperatureC,
					TemperatureF = temperatureF,
					Summary = Summaries[rng.Next(Summaries.Length)]
				});
			}
			return webs;
        }

        [HttpPost]
        public string Post(Webivandevelop web)
        {

            return "Maybe";
        }
    }
}
