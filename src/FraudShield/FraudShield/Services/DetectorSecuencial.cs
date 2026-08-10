using FraudShield.Analizadores;
using FraudShield.Interfaces;
using FraudShield.Models;

namespace FraudShield.Services;

/// <summary>
/// Analiza una colección de transacciones de forma secuencial.
/// </summary

public class DetectorSecuencial : IDetectorFraude
{
    private readonly AnalizadorMonto _analizadorMonto = new();
    private readonly AnalizadorPais _analizadorPais = new();
    private readonly AnalizadorHorario _analizadorHorario = new();
    private readonly AnalizadorComercio _analizadorComercio = new();
    private readonly AnalizadorFrecuencia _analizadorFrecuencia = new();

    /// <summary>
    /// Analiza una colección de transacciones de forma secuencial.
    /// </summary>
    public List<ResultadoAnalisis> Detectar(
        IEnumerable<Transaccion> transacciones)
    {
        List<ResultadoAnalisis> resultados = new();

        // Convertimos la colección a lista una sola vez.
        List<Transaccion> historial = transacciones.ToList();

        // Agrupamos las transacciones por cliente y las ordenamos por fecha.
        Dictionary<string, List<Transaccion>> historialPorCliente =
            historial
                .GroupBy(t => t.ClienteId)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(t => t.FechaHora).ToList());

        foreach (Transaccion transaccion in historial)
        {
            // Obtenemos únicamente el historial del cliente actual.
            List<Transaccion> historialCliente =
                historialPorCliente[transaccion.ClienteId];

            ResultadoAnalisis resultadoMonto =
                _analizadorMonto.Analizar(transaccion);

            ResultadoAnalisis resultadoPais =
                _analizadorPais.Analizar(transaccion);

            ResultadoAnalisis resultadoHorario =
                _analizadorHorario.Analizar(transaccion);

            ResultadoAnalisis resultadoComercio =
                _analizadorComercio.Analizar(transaccion);

            ResultadoAnalisis resultadoFrecuencia =
                _analizadorFrecuencia.Analizar(
                    transaccion,
                    historialCliente);

            resultados.AddRange(new[]
            {
                resultadoMonto,
                resultadoPais,
                resultadoHorario,
                resultadoComercio,
                resultadoFrecuencia
            });
        }

        return resultados;
    }
}