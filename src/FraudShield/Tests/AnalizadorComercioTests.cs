using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Analizadores;
using FraudShield.Models;
using Xunit;

namespace Tests;

public class AnalizadorComercioTests
{
    [Fact]
    public void DebeDetectarComercioRiesgoso()
    {
        var analizador = new AnalizadorComercio();

        var transaccion = new Transaccion
        {
            Comercio = "Crypto Exchange"
        };

        var resultado = analizador.Analizar(transaccion);

        Assert.True(resultado.EsSospechosa);
        Assert.Equal(15, resultado.Riesgo);
    }

    [Fact]
    public void NoDebeDetectarComercioNormal()
    {
        var analizador = new AnalizadorComercio();

        var transaccion = new Transaccion
        {
            Comercio = "Jumbo"
        };

        var resultado = analizador.Analizar(transaccion);

        Assert.False(resultado.EsSospechosa);
        Assert.Equal(0, resultado.Riesgo);
    }
}
