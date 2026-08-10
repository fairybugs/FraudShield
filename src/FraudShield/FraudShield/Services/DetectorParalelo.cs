using System.Collections.Concurrent;
using FraudShield.Analizadores;
using FraudShield.Interfaces;
using FraudShield.Models;

namespace FraudShield.Services;

/// <summary>
/// Ejecuta los analizadores de fraude de manera paralela.
/// </summary>
public class DetectorParalelo : IDetectorFraude
{
    private readonly int _maximoHilos;

    private readonly AnalizadorMonto _analizadorMonto = new();
    private readonly AnalizadorPais _analizadorPais = new();
    private readonly AnalizadorHorario _analizadorHorario = new();
    private readonly AnalizadorComercio _analizadorComercio = new();
    private readonly AnalizadorFrecuencia _analizadorFrecuencia = new();

    public DetectorParalelo(int maximoHilos)
    {
        _maximoHilos = maximoHilos;
    }

    /// <summary>
    /// Analiza una colección de transacciones de forma paralela.
    /// </summary>
    public List<ResultadoAnalisis> Detectar(
        IEnumerable<Transaccion> transacciones)
    {
        ConcurrentBag<ResultadoAnalisis> resultados = new();

        // Convertimos la colección a lista una sola vez.
        List<Transaccion> historial = transacciones.ToList();

        // Agrupamos las transacciones por cliente y las ordenamos por fecha.
        Dictionary<string, List<Transaccion>> historialPorCliente =
            historial
                .GroupBy(t => t.ClienteId)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(t => t.FechaHora).ToList());

        ParallelOptions opciones = new()
        {
            MaxDegreeOfParallelism = _maximoHilos
        };

        Parallel.ForEach(historial, opciones, transaccion =>
        {
            // Obtenemos únicamente el historial del cliente actual.
            List<Transaccion> historialCliente =
                historialPorCliente[transaccion.ClienteId];

            resultados.Add(
                _analizadorMonto.Analizar(transaccion));

            resultados.Add(
                _analizadorPais.Analizar(transaccion));

            resultados.Add(
                _analizadorHorario.Analizar(transaccion));

            resultados.Add(
                _analizadorComercio.Analizar(transaccion));

            resultados.Add(
                _analizadorFrecuencia.Analizar(
                    transaccion,
                    historialCliente));
        });

        return resultados.ToList();
    }
}