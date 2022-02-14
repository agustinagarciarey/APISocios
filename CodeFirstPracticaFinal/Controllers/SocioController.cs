using System.Diagnostics;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resultados;
using Microsoft.AspNetCore.Cors;
using Data;
using Comando.Socios;
using Models;

namespace CodeFirstPracticaFinal.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    public class SocioController : ControllerBase
    {
        //declaro una variable llamada db, de tipo context, que nunca podrá ser modificada
        //usamos el ctor vacío
        private readonly Context db = new Context();

        private readonly ILogger<SocioController> _logger;

        public SocioController(ILogger<SocioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Socio/GetSocios")]
        public ActionResult<ResultadoAPI> GetAllSocios()
        {
            var resultado = new ResultadoAPI();
            try
            {
                resultado.Ok = true;
                var socios = db.Socios.ToList();
                foreach (var d in socios)
                {
                    var idDeporte = d.IdDeporte;
                    var deporte = db.Deportes.FirstOrDefault(d => d.Id == idDeporte);
                    d.Deporte = deporte;
                }
                resultado.Return = socios;
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

        [HttpGet]
        [Route("Socio/GetSocio{idSocio}")]
        public ActionResult<ResultadoAPI> GetSocio(int idSocio)
        {
            var resultado = new ResultadoAPI();
            try
            {
                var s = db.Socios.Where(s => s.Id == idSocio).FirstOrDefault();
                if (s != null)
                {
                    resultado.Ok = true;
                    resultado.Return = s;
                    db.Entry(s).Reference(d => d.Deporte).Load();
                }
                else
                {
                    resultado.Ok = false;
                    resultado.CodigoError = 404;
                    resultado.Error = "Socio no encontrado";
                }
                return resultado;

            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 500;
                resultado.Error = "Internal Server Error -" + ex.Message;
                return resultado;
            }
        }

        [HttpPost]
        [Route("Alumno/CreateSocio")]
        public ActionResult<ResultadoAPI> AltaPersona([FromBody] ComandoCreateSocio comando)
        {
            var resultado = new ResultadoAPI();
            try
            {
                if (comando.Nombre.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese nombre";
                    return resultado;

                }
                if (comando.Apellido.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese nombre";
                    return resultado;

                }
                if (comando.DNI.Equals(0))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese dni";
                    return resultado;
                }
                if (comando.Edad.Equals(0))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese edad";
                    return resultado;
                }
                if (comando.Direccion.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese dirección";
                    return resultado;

                }
                if (comando.Email.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese email";
                    return resultado;

                }
                if (comando.Deporte.Equals(0))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese deporte";
                    return resultado;

                }
                if (comando.FechaAlta == default(DateTime))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese deporte";
                    return resultado;

                }

                var exists = db.Socios.Where(s => s.Email == comando.Email).FirstOrDefault();
                if (exists != null)
                {
                    resultado.Ok = false;
                    resultado.Error = "Bad Request - El email ya ha sido ingresado";
                    resultado.CodigoError = 400;
                    return resultado;
                }
                else
                {
                    var s = new Socio();
                    s.Nombre = comando.Nombre;
                    s.Apellido = comando.Apellido;
                    s.IdDeporte = comando.Deporte;
                    s.Edad = comando.Edad;
                    s.DNI = comando.DNI;
                    s.Email = comando.Email;
                    s.Direccion = comando.Direccion;
                    s.Premium = comando.Premium;
                    s.FechaAlta = comando.FechaAlta;

                    db.Socios.Add(s);
                    db.SaveChanges(); //NO OLVIDAR ESTO
                    resultado.Ok = true;

                    resultado.Return = s.Id;
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 500;
                resultado.Error = "Internal Server Error -" + ex.Message;
                return resultado;
            }

        }

    }
}