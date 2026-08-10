using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Analizadores;
using FraudShield.Models;
using Xunit;

namespace Tests
{
    public class AnalizadorHorarioTests
    {
        [Fact]
        public void DebeDetectarHorarioPeligroso()
        {
            var analizador = new AnalizadorHorario();

            var transaccion = new Transaccion
            {
                FechaHora = new DateTime(2026, 8, 5, 2, 30, 0)
            };

            var resultado = analizador.Analizar(transaccion);

            Assert.True(resultado.EsSospechosa);
        }

        [Fact]
        public void NoDebeDetectarHorarioNormal()
        {
            var analizador = new AnalizadorHorario();

            var transaccion = new Transaccion
            {
                FechaHora = new DateTime(2026, 8, 5, 15, 0, 0)
            };

            var resultado = analizador.Analizar(transaccion);

            Assert.False(resultado.EsSospechosa);
        }

    }


}
