using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Analizadores;
using FraudShield.Models;
using Xunit;

namespace Tests;

public class AnalizadorFrecuenciaTests
{
    [Fact]
    public void DebeDetectarCincoCompras()
    {
        var analizador = new AnalizadorFrecuencia();

        DateTime fecha = new DateTime(2026, 8, 5, 10, 0, 0);

        var historial = new List<Transaccion>
        {
            new() { ClienteId = "C0001", FechaHora = fecha.AddMinutes(-4) },
            new() { ClienteId = "C0001", FechaHora = fecha.AddMinutes(-3) },
            new() { ClienteId = "C0001", FechaHora = fecha.AddMinutes(-2) },
            new() { ClienteId = "C0001", FechaHora = fecha.AddMinutes(-1) },
            new() { ClienteId = "C0001", FechaHora = fecha }
        };

        var resultado = analizador.Analizar(historial[4], historial);

        Assert.True(resultado.EsSospechosa);
        Assert.Equal(20, resultado.Riesgo);
    }

    [Fact]
    public void DebeDetectarCuatroCompras()
    {
        var analizador = new AnalizadorFrecuencia();

        DateTime fecha = new DateTime(2026, 8, 5, 10, 0, 0);

        var historial = new List<Transaccion>
        {
            new() { ClienteId = "C0001", FechaHora = fecha.AddMinutes(-3) },
            new() { ClienteId = "C0001", FechaHora = fecha.AddMinutes(-2) },
            new() { ClienteId = "C0001", FechaHora = fecha.AddMinutes(-1) },
            new() { ClienteId = "C0001", FechaHora = fecha }
        };

        var resultado = analizador.Analizar(historial[3], historial);

        Assert.True(resultado.EsSospechosa);
        Assert.Equal(15, resultado.Riesgo);
    }

    [Fact]
    public void NoDebeDetectarUnaCompra()
    {
        var analizador = new AnalizadorFrecuencia();

        DateTime fecha = new DateTime(2026, 8, 5, 10, 0, 0);

        var historial = new List<Transaccion>
        {
            new() { ClienteId = "C0001", FechaHora = fecha }
        };

        var resultado = analizador.Analizar(historial[0], historial);

        Assert.False(resultado.EsSospechosa);
        Assert.Equal(0, resultado.Riesgo);
    }
}