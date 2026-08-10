using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Interfaces;
using FraudShield.Models;

namespace FraudShield.Analizadores;

/// <summary>
/// Analiza la frecuencia de compras realizadas por un mismo cliente.
/// </summary>

public class AnalizadorFrecuencia : IAnalizadorFraude
{
    public ResultadoAnalisis Analizar(Transaccion transaccion)
    {
        return new ResultadoAnalisis
        {
            Analizador = "Frecuencia",
            Riesgo = 0,
            EsSospechosa = false,
            Motivo = "No existe historial suficiente para evaluar la frecuencia."
        };
    }


    // Analiza la frecuencia utilizando el historial de transacciones del cliente.

    public ResultadoAnalisis Analizar(
        Transaccion transaccion,
        IEnumerable<Transaccion> historial)
    {
        ResultadoAnalisis resultado = new()
        {
            Analizador = "Frecuencia",
            Riesgo = 0,
            EsSospechosa = false,
            Motivo = "La frecuencia de compras es normal."
        };

        DateTime inicio =
            transaccion.FechaHora.AddMinutes(-5);

        int cantidad =
            historial.Count(t =>
                t.ClienteId == transaccion.ClienteId &&
                t.FechaHora >= inicio &&
                t.FechaHora <= transaccion.FechaHora);

        if (cantidad >= 5)
        {
            resultado.Riesgo = 20;
            resultado.EsSospechosa = true;
            resultado.Motivo =
                "Se detectaron cinco o más compras en menos de cinco minutos.";
        }
        else if (cantidad == 4)
        {
            resultado.Riesgo = 15;
            resultado.EsSospechosa = true;
            resultado.Motivo =
                "Se detectaron cuatro compras en menos de cinco minutos.";
        }
        else if (cantidad == 3)
        {
            resultado.Riesgo = 10;
            resultado.EsSospechosa = false;
            resultado.Motivo =
                "Se detectaron tres compras en menos de cinco minutos.";
        }

        return resultado;
    }
}