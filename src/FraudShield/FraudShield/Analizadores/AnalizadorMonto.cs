using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Interfaces;
using FraudShield.Models;

namespace FraudShield.Analizadores;

public class AnalizadorMonto : IAnalizadorFraude
{
    
    // Resultado del análisis de riesgo por monto.
    public ResultadoAnalisis Analizar(Transaccion transaccion)
    {
        if (transaccion.MontoPromedioCliente <= 0)
        {
            return new ResultadoAnalisis
            {
                Analizador = "Monto",
                Riesgo = 0,
                EsSospechosa = false,
                Motivo = "No existe un monto promedio para evaluar."
            };
        }

        decimal relacion =
            transaccion.Monto / transaccion.MontoPromedioCliente;

        ResultadoAnalisis resultado = new()
        {
            Analizador = "Monto",
            Riesgo = 0,
            EsSospechosa = false,
            Motivo = "El monto se encuentra dentro del comportamiento habitual."
        };

        if (relacion > 5)
        {
            resultado.Riesgo = 30;
            resultado.EsSospechosa = true;
            resultado.Motivo =
                "El monto supera más de cinco veces el promedio del cliente.";
        }
        else if (relacion > 3)
        {
            resultado.Riesgo = 20;
            resultado.EsSospechosa = true;
            resultado.Motivo =
                "El monto supera más de tres veces el promedio del cliente.";
        }
        else if (relacion > 2)
        {
            resultado.Riesgo = 10;
            resultado.EsSospechosa = false;
            resultado.Motivo =
                "El monto supera dos veces el promedio del cliente.";
        }

        return resultado;
    }
}