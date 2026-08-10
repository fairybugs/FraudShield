using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Interfaces;
using FraudShield.Models;

namespace FraudShield.Analizadores;

/// <summary>
/// Analiza si la transacción fue realizada desde
/// un país diferente al habitual del cliente.
/// </summary>

public class AnalizadorPais : IAnalizadorFraude
{
    //Resultado del análisis de riesgo por país.
    
    public ResultadoAnalisis Analizar(Transaccion transaccion)
    {
        ResultadoAnalisis resultado = new()
        {
            Analizador = "País",
            Riesgo = 0,
            EsSospechosa = false,
            Motivo = "La transacción se realizó desde el país habitual."
        };

        if (transaccion.Pais != transaccion.PaisHabitual)
        {
            resultado.Riesgo = 20;
            resultado.EsSospechosa = true;
            resultado.Motivo =
                "La transacción fue realizada desde un país diferente al habitual del cliente.";
        }

        return resultado;
    }
}