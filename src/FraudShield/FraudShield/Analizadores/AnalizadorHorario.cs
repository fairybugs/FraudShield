using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Interfaces;
using FraudShield.Models;

namespace FraudShield.Analizadores;

/// <summary>
/// Analiza si una transacción fue realizada
/// en un horario considerado poco habitual.
/// </summary>

public class AnalizadorHorario : IAnalizadorFraude
{
    // Evalúa la hora de la transacción.
    
    public ResultadoAnalisis Analizar(Transaccion transaccion)
    {
        ResultadoAnalisis resultado = new()
        {
            Analizador = "Horario",
            Riesgo = 0,
            EsSospechosa = false,
            Motivo = "La transacción fue realizada en un horario habitual."
        };

        int hora = transaccion.FechaHora.Hour;

        if (hora < 7 || hora > 21)
        {
            resultado.Riesgo = 15;
            resultado.EsSospechosa = true;
            resultado.Motivo =
                "La transacción fue realizada en un horario de alto riesgo.";
        }

        return resultado;
    }
}