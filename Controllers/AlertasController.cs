using Microsoft.AspNetCore.Mvc;
using NanoGuardian.Api.Models;

namespace NanoGuardian.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertasController : ControllerBase
    {
        // Memoria temporal estática (persiste mientras el servidor esté encendido)
        private static Alerta? _ultimaAlerta = null;

        // POST: api/Alertas
        // El ESP32 envía los datos aquí
        [HttpPost]
        public IActionResult RecibirAlerta([FromBody] Alerta nuevaAlerta)
        {
            if (nuevaAlerta == null)
            {
                return BadRequest(new { mensaje = "Cuerpo de la petición vacío o inválido" });
            }

            // Guardamos la alerta
            _ultimaAlerta = nuevaAlerta;

            // Log para Render (ver en la pestaña 'Logs' de Render)
            Console.WriteLine($"[ALERT] Recibida de: {nuevaAlerta.Paciente}, G: {nuevaAlerta.FuerzaImpactoG}");

            return Ok(new { mensaje = "Alerta procesada", ok = true });
        }

        // GET: api/Alertas
        // La App móvil consulta aquí
        [HttpGet]
        public IActionResult ObtenerUltimaAlerta()
        {
            if (_ultimaAlerta == null)
            {
                return Ok(new { 
                    paciente = "Ninguno", 
                    estado = "Monitoreando", 
                    fuerzaImpactoG = 0 
                });
            }
            return Ok(_ultimaAlerta);
        }
    }
}