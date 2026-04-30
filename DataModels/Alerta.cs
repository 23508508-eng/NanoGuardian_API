using System;
using System.Text.Json.Serialization;

namespace NanoGuardian.Api.Models
{
    public class Alerta
    {
        [JsonPropertyName("paciente")]
        public string Paciente { get; set; } = "Desconocido";

        [JsonPropertyName("fuerzaImpactoG")]
        public int FuerzaImpactoG { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; } = "Normal";

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}