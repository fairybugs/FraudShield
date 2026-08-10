using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Data;
using FraudShield.Interfaces;
using FraudShield.Models;

namespace FraudShield.Analizadores;

/// <summary>
/// Analiza si la transacción fue realizada en un comercio catalogado como alto riesgo.
/// </summary>

public class AnalizadorComercio : IAnalizadorFraude
{
    public ResultadoAnalisis Analizar(Transaccion transaccion)
    {
        ResultadoAnalisis resultado = new()
        {
            Analizador = "Comercio",
            Riesgo = 0,
            EsSospechosa = false,
            Motivo = "El comercio no presenta riesgos conocidos."
        };

        if (CatalogoDatos.ComerciosRiesgosos.Contains(transaccion.Comercio))
        {
            resultado.Riesgo = 15;
            resultado.EsSospechosa = true;
            resultado.Motivo =
                "La transacción fue realizada en un comercio catalogado como de alto riesgo.";
        }

        return resultado;
    }
}