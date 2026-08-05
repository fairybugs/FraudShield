using System;
using System.Collections.Generic;
using System.Text;

namespace FraudShield.Models
{
    public class Cliente
    {
        public string ClienteId { get; set; } = string.Empty;

        public string PaisHabitual { get; set; } = string.Empty;

        public decimal MontoPromedio { get; set; }

        public int HoraInicioHabitual { get; set; }

        public int HoraFinHabitual { get; set; }
    }
}
