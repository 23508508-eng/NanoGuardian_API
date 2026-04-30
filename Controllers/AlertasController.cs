using Microsoft.AspNetCore.Mvc;
using NanoGuardian.Api.Models; // Referencia a tu carpeta local

namespace NanoGuardian.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class AlertasController : ControllerBase
    {
        private static readonly List<Alerta> _alertasGuardadas = new List<Alerta>();

        [HttpGet]
        public IActionResult Get() => Ok(_alertasGuardadas);

        [HttpPost]
        public IActionResult Post([FromBody] Alerta nuevaAlerta)
        {
            if (nuevaAlerta == null) return BadRequest("Datos nulos");
            nuevaAlerta.Fecha = DateTime.UtcNow;
            _alertasGuardadas.Add(nuevaAlerta);
            return Ok(new { mensaje = "Recibido", total = _alertasGuardadas.Count });
        }
    }
}