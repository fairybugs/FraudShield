using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Models;

namespace FraudShield.Services;

/// <summary>
/// Evalúa el riesgo total de una transacción y determina
/// si debe aprobarse o cancelarse.
/// </summary>
public class EvaluadorFraude
{
    public int CalcularRiesgoTotal(
        IEnumerable<ResultadoAnalisis> resultados)
    {
        return resultados.Sum(r => r.Riesgo);
    }

    public bool DebeCancelar(int riesgoTotal)
    {
        return riesgoTotal >= 50;
    }

    public bool DebeAprobar(int riesgoTotal)
    {
        return !DebeCancelar(riesgoTotal);
    }
}