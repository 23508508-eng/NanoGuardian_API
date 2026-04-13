namespace NanoGuardian.Api.Models
{
    public class Alerta
    {
        public required string Paciente { get; set; }
        public int FuerzaImpactoG { get; set; }
        public string Estado { get; set; } = "Emergencia";
    }
}