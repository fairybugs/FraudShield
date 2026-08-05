using System;
using System.Collections.Generic;
using System.Text;

namespace FraudShield.Models
{
    public class ResultadoAnalisis
    {
        public string Analizador { get; set; } = string.Empty;

        public int Riesgo { get; set; }

        public string Motivo { get; set; } = string.Empty;
    }
}
