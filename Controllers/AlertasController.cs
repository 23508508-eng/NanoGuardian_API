using Microsoft.AspNetCore.Mvc;
using NanoGuardian.Api.Models;

namespace NanoGuardian.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertasController : ControllerBase
    {
        [HttpPost]
        public IActionResult RecibirAlerta([FromBody] Alerta nuevaAlerta)
        {
            if (string.IsNullOrEmpty(nuevaAlerta.Paciente))
            {
                return BadRequest("El nombre del paciente es obligatorio.");
            }
            
            Console.WriteLine($"\n🚨 ALERTA RECIBIDA: Paciente {nuevaAlerta.Paciente} cayó con {nuevaAlerta.FuerzaImpactoG}G\n");
            
            return Ok(new { mensaje = "Alerta procesada con éxito", codigo = 200 });
        }
    }
}