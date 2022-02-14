using System.Diagnostics;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resultados;
using Microsoft.AspNetCore.Cors;
using Data;
using Models;

namespace CodeFirstPracticaFinal.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    public class DeporteController : ControllerBase
    {
        //declaro una variable llamada db, de tipo context, que nunca podrá ser modificada
        //usamos el ctor vacío
        private readonly Context db = new Context();

        private readonly ILogger<SocioController> _logger;

        public DeporteController(ILogger<SocioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Deporte/GetDeportes")]
        public ActionResult<ResultadoAPI> GetAllDeportes()
        {
            var resultado = new ResultadoAPI();
            try
            {
                resultado.Ok = true;
                resultado.CodigoError = 200;
                resultado.Return = db.Deportes.ToList();
                return resultado;

            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 500;
                resultado.Error = "Internal Server Error - " + ex.Message;
                return resultado;
            }

        }
    }
}