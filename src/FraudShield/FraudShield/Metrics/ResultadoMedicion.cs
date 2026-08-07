using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Models;

namespace FraudShield.Metrics;

public class ResultadoMedicion
{
    public long Tiempo { get; set; }
    public List<ResultadoAnalisis> Resultados { get; set; } = new();
}

