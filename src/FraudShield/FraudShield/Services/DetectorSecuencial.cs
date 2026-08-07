using FraudShield.Analizadores;
using FraudShield.Interfaces;
using FraudShield.Models;

namespace FraudShield.Services;

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

        // Convertimos a lista una sola vez para reutilizarla
        List<Transaccion> historial = transacciones.ToList();

        foreach (Transaccion transaccion in historial)
        {
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
                    historial);

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
