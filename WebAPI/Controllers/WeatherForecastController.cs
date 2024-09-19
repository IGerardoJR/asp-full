using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // http://localhost:5000/WeatherForecast
    public class WeatherForecastController : ControllerBase
    {
        //private readonly ILogger _logger;
        private readonly CursosOnlineContext context;
        public WeatherForecastController(CursosOnlineContext _context)
        {
            this.context = _context;
        }

        //[HttpGet]
        //public IEnumerable<string> Get(){
        //    string[] nombres = {"Isaias","Rolando","Maria","Rebeca"};
        //    return nombres;
        //}

        [HttpGet]
        public IEnumerable<Curso> Get()
        {
            return context.Curso.ToList();
        }
    }
}
