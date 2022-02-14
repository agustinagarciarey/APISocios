using System.Diagnostics;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resultados;
using Microsoft.AspNetCore.Cors;
using Data;
using Comando.Usuarios;
using Models;

namespace CodeFirstPracticaFinal.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    public class UsuarioController : ControllerBase
    {
        //declaro una variable llamada db, de tipo context, que nunca podrá ser modificada
        //usamos el ctor vacío
        private readonly Context db = new Context();

        private readonly ILogger<SocioController> _logger;

        public UsuarioController(ILogger<SocioController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Usuario/Login")]
        public ActionResult<ResultadoAPI> Login([FromBody] ComandoLogin comando)
        {
            var resultado = new ResultadoAPI();
            if (comando.Email.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese email";
                return resultado;

            }
            if (comando.Password.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese password";
                return resultado;

            }
            try
            {
                var user = db.Usuarios.Where(u => u.Email == comando.Email && u.Password == comando.Password).FirstOrDefault();
                if (user == null)
                {
                    resultado.Ok = false;
                    resultado.Error = "Bad Request - Mail o contraseña incorrectas";
                    resultado.CodigoError = 400;
                    return resultado;
                }
                else {
                    resultado.Ok = true;
                    resultado.CodigoError = 200;
                    return resultado;
                }

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