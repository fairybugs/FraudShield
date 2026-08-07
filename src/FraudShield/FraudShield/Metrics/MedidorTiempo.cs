using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using FraudShield.Interfaces;
using FraudShield.Models;

namespace FraudShield.Metrics;

public class MedidorTiempo
{
    public ResultadoMedicion Medir(
        IDetectorFraude detector,
        IEnumerable<Transaccion> transacciones)
    {
        Stopwatch cronometro = Stopwatch.StartNew();

        List<ResultadoAnalisis> resultados =
            detector.Detectar(transacciones);

        cronometro.Stop();

        return new ResultadoMedicion
        {
            Tiempo = cronometro.ElapsedMilliseconds,
            Resultados = resultados
        };
    }
}