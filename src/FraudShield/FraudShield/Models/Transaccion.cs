using CsvHelper.Configuration.Attributes;
namespace FraudShield.Models;

public class Transaccion
{
    public long Id { get; set; }

    public string ClienteId { get; set; } = string.Empty;

    public decimal Monto { get; set; }

    public string Pais { get; set; } = string.Empty;

    public string PaisHabitual { get; set; } = string.Empty;

    public DateTime FechaHora { get; set; }

    public string Comercio { get; set; } = string.Empty;

    public string Categoria { get; set; } = string.Empty;

    public string MetodoPago { get; set; } = string.Empty;

    public bool EsFraudeReal { get; set; }
}