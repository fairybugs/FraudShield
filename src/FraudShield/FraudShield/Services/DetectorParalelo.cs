using System;
using System.Collections.Generic;
using System.Text;
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
    private readonly AnalizadorMonto _analizadorMonto = new();
    private readonly AnalizadorPais _analizadorPais = new();
    private readonly AnalizadorHorario _analizadorHorario = new();
    private readonly AnalizadorComercio _analizadorComercio = new();
    private readonly AnalizadorFrecuencia _analizadorFrecuencia = new();

    public List<ResultadoAnalisis> Detectar(
        IEnumerable<Transaccion> transacciones)
    {
        ConcurrentBag<ResultadoAnalisis> resultados = new();

        Parallel.ForEach(transacciones, transaccion =>
        {
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
                    transacciones));
        });

        return resultados.ToList();
    }
}