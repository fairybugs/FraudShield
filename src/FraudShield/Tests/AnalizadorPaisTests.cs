using FraudShield.Analizadores;
using FraudShield.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class AnalizadorPaisTests
    {
        [Fact]
        public void DebeDetectarPaisDiferente()
        {
            var analizador = new AnalizadorPais();

            var transaccion = new Transaccion
            {
                Pais = "Estados Unidos",
                PaisHabitual = "República Dominicana"
            };

            var resultado = analizador.Analizar(transaccion);

            Assert.True(resultado.EsSospechosa);
            Assert.Equal(20, resultado.Riesgo);

        }

        [Fact]
        public void NoDebeDetectarPaisHabitual()
        {
            var analizador = new AnalizadorPais();

            var transaccion = new Transaccion
            {
                Pais = "República Dominicana",
                PaisHabitual = "República Dominicana"
            };

            var resultado = analizador.Analizar(transaccion);

            Assert.False(resultado.EsSospechosa);
            Assert.Equal(0, resultado.Riesgo);
        }
    }
}
