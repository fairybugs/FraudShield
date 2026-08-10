using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Models;
using FraudShield.Services;
using Xunit;

namespace Tests;

public class DetectorParaleloTests
{
    [Fact]
    public void DebeGenerarCincoResultadosPorUnaTransaccion()
    {
        var detector = new DetectorParalelo(Environment.ProcessorCount);

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
            }
        };

        var resultados = detector.Detectar(transacciones);

        Assert.Equal(5, resultados.Count);
    }

    [Fact]
    public void DebeGenerarDiezResultadosParaDosTransacciones()
    {
        var detector = new DetectorParalelo(Environment.ProcessorCount);

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

        var resultados = detector.Detectar(transacciones);

        Assert.Equal(10, resultados.Count);
    }

    [Fact]
    public void NoDebeGenerarResultadosSiNoHayTransacciones()
    {
        var detector = new DetectorParalelo(Environment.ProcessorCount);

        var transacciones = new List<Transaccion>();

        var resultados = detector.Detectar(transacciones);

        Assert.Empty(resultados);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    public void DebeGradosdeParalelismo(int hilos)
    {
        var detector = new DetectorParalelo(hilos);

        var transacciones = new List<Transaccion>
    {
        new()
        {
            ClienteId = "C0001",
            Monto = 1000,
            MontoPromedioCliente = 1000,
            Pais = "República Dominicana",
            PaisHabitual = "República Dominicana",
            FechaHora = DateTime.Now,
            Comercio = "Jumbo"
        }
    };

        var resultados = detector.Detectar(transacciones);

        Assert.Equal(5, resultados.Count);
    }





}