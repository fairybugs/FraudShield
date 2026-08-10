using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Models;

namespace FraudShield.Interfaces;

/// <summary>
/// Define el contrato para los detectores de fraude.
/// </summary>
public interface IDetectorFraude
{
    List<ResultadoAnalisis> Detectar(
        IEnumerable<Transaccion> transacciones);
}