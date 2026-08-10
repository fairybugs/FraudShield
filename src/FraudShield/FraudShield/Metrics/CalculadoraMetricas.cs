using System;
using System.Collections.Generic;
using System.Text;

namespace FraudShield.Metrics;

/// <summary>
/// Calcula las métricas de rendimiento del sistema.
/// </summary>
public static class CalculadoraMetricas
{
    public static double CalcularSpeedup(
        long tiempoSecuencial,
        long tiempoParalelo)
    {
        if (tiempoParalelo == 0)
        {
            return 0;
        }

        return (double)tiempoSecuencial / tiempoParalelo;
    }

    public static double CalcularEficiencia(
        double speedup,
        int procesadores)
    {
        if (procesadores == 0)
        {
            return 0;
        }

        return (speedup / procesadores) * 100;
    }

    public static int ObtenerProcesadores()
    {
        return Environment.ProcessorCount;
    }
}