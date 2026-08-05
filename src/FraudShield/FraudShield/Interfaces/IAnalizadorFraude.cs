using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Models;

namespace FraudShield.Interfaces
{
    public interface IAnalizadorFraude
    {
        /// <summary>
        /// esto analiza una transacción y devuelve el resultado de la evaluación
        /// </summary>
   
        ResultadoAnalisis Analizar(Transaccion transaccion);
    }
}
