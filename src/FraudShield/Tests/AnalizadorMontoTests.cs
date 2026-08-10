using FraudShield.Analizadores;
using FraudShield.Models;
using Xunit;

namespace Tests;

public class AnalizadorMontoTests
{
    [Fact]
    public void DebeDetectarMontoMuyAlto()
    {
        var analizador = new AnalizadorMonto();

        var transaccion = new Transaccion
        {
            Monto = 60000,
            MontoPromedioCliente = 10000
        };

        var resultado = analizador.Analizar(transaccion);

        Assert.True(resultado.EsSospechosa);
        Assert.Equal(30, resultado.Riesgo);
    }

    [Fact]
    public void NoDebeDetectarMontoNormal()
    {
        var analizador = new AnalizadorMonto();

        var transaccion = new Transaccion
        {
            Monto = 9500,
            MontoPromedioCliente = 10000
        };

        var resultado = analizador.Analizar(transaccion);

        Assert.False(resultado.EsSospechosa);
        Assert.Equal(0, resultado.Riesgo);
    }
}