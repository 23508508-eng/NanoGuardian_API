using Microsoft.AspNetCore.Mvc;
using NanoGuardian.Api.Models;

namespace NanoGuardian.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertasController : ControllerBase
    {
        // 1. Creamos una "memoria temporal" para guardar la última alerta que llegue
        private static Alerta _ultimaAlerta = null;

        // 2. Método POST: El sensor usa este para ENVIAR la alerta
        [HttpPost]
        public IActionResult RecibirAlerta([FromBody] Alerta nuevaAlerta)
        {
            if (string.IsNullOrEmpty(nuevaAlerta.Paciente))
            {
                return BadRequest("El nombre del paciente es obligatorio.");
            }
            
            // Guardamos la alerta en nuestra memoria temporal
            _ultimaAlerta = nuevaAlerta;
            
            Console.WriteLine($"\n🚨 ALERTA RECIBIDA: Paciente {nuevaAlerta.Paciente} cayó con {nuevaAlerta.FuerzaImpactoG}G\n");
            
            return Ok(new { mensaje = "Alerta procesada con éxito", codigo = 200 });
        }

        // 3. Método GET: Tu app móvil usa este para LEER la última alerta
        [HttpGet]
        public IActionResult ObtenerUltimaAlerta()
        {
            // Si nadie ha enviado alertas aún, mandamos un mensaje de espera
            if (_ultimaAlerta == null)
            {
                // Usamos una estructura anónima que coincida con lo que espera tu app
                return Ok(new { Paciente = "Ninguno", Estado = "Monitoreando...", FuerzaImpactoG = 0.0 });
            }

            // Si hay una alerta guardada, se la enviamos a la app
            return Ok(_ultimaAlerta);
        }
    }
}