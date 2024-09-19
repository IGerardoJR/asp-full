using Aplicacion.ManejadorErrores;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class ManejadorErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErrorMiddleware> _logger;
        public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

    public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await ManejadorExepcionAsincrono(context, e, _logger);
            }
        }

        private async Task ManejadorExepcionAsincrono(HttpContext context, Exception e, ILogger<ManejadorErrorMiddleware> logger)
        {
            object errores = null;
            switch (e)
            {
                case ManejadorExepcion me:
                    logger.LogError(e, "Manejador error");
                    errores = me.Errores;
                    context.Response.StatusCode = (int)me.Codigo;
                    break;
                case Exception ex:
                    logger.LogError(e, "Error de servidor");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    break;
                default:
                    break;
            }
            context.Response.ContentType = "application/json";
            if(errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }

        }
    }
}
