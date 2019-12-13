using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using static CrowDo.UserDTO;

namespace CrowDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private DTOExcel _dtoExcel;
        public WeatherForecastController (DTOExcel dtoExcel,ILogger<WeatherForecastController> logger)
        {
            _dtoExcel = dtoExcel; 
            _logger = logger;
        }


      

        //[HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public List<int> GetUsers()
        {

            List<int> lint = new List<int>();
            //data transfer only first time
            //_dtoExcel.test();
           return lint;
        }

    }
}
