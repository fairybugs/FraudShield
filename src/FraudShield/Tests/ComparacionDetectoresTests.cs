using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Models;
using FraudShield.Services;
using Xunit;

namespace Tests;

public class ComparacionDetectoresTests
{
    [Fact]
    public void DebenTenerLoMismo()
    {
        var detectorSecuencial = new DetectorSecuencial();
        var detectorParalelo = new DetectorParalelo(Environment.ProcessorCount);

        var transacciones = new List<Transaccion>
        {
            new()
            {
                ClienteId = "C0001",
                Monto = 1000,
                MontoPromedioCliente = 1000,
                Pais = "República Dominicana",
                PaisHabitual = "República Dominicana",
                FechaHora = new DateTime(2026, 8, 5, 10, 0, 0),
                Comercio = "Jumbo"
            },

            new()
            {
                ClienteId = "C0002",
                Monto = 5000,
                MontoPromedioCliente = 5000,
                Pais = "República Dominicana",
                PaisHabitual = "República Dominicana",
                FechaHora = new DateTime(2026, 8, 5, 11, 0, 0),
                Comercio = "Bravo"
            }
        };

        var resultadosSecuencial =
            detectorSecuencial.Detectar(transacciones);

        var resultadosParalelo =
            detectorParalelo.Detectar(transacciones);

        Assert.Equal(
            resultadosSecuencial.Count,
            resultadosParalelo.Count);
    }
}