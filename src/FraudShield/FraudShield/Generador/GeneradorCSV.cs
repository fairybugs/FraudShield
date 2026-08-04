using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using FraudShield.Models;

namespace FraudShield.Generador
{
    public class GeneradorCSV
    {
        private readonly Random _random = new();

        // Catálogos de valores realistas
        private readonly string[] comercios =
        {
            "Supermercado Nacional",
            "La Sirena",
            "Banco Popular",
            "Farmacia Carol",
            "Estación de Gasolina",
            "Restaurante Adrian Tropical",
            "Tiendas Corripio",
            "Ferretería Americana"
        };

        private readonly string[] categorias =
        {
            "Alimentos",
            "Electronica",
            "Farmacia",
            "Combustible",
            "Restaurante",
            "Ferreteria"
        };

        private readonly string[] metodosPago =
        {
            "Tarjeta de credito",
            "Tarjeta de debito",
            "Transferencia bancaria",
            "Pago movil",
            "Cheque"
        };

        private readonly string[] paises =
        {
            "Republica Dominicana",
            "Estados Unidos",
            "España",
            "Mexico",
            "Colombia",
            "Panama"
        };

        public void GenerarArchivo(int total, int fraudulentas, string rutaArchivo)
        {
            int normales = total - fraudulentas;
            var transacciones = new List<Transaccion>(total);

            // Generar normales
            for (int i = 0; i < normales; i++)
                transacciones.Add(GenerarTransaccion(false, i + 1));

            // Generar fraudulentas
            for (int i = 0; i < fraudulentas; i++)
                transacciones.Add(GenerarTransaccion(true, normales + i + 1));

            // Guardar en CSV
            using var writer = new StreamWriter(rutaArchivo);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(transacciones);

            Console.WriteLine($" Archivo generado: {rutaArchivo}");
        }

        private Transaccion GenerarTransaccion(bool esFraude, int id)
        {
            string comercio = comercios[_random.Next(comercios.Length)];
            string categoria = categorias[_random.Next(categorias.Length)];
            string metodo = metodosPago[_random.Next(metodosPago.Length)];
            string paisNormal = "República Dominicana";
            string pais = esFraude ? paises[_random.Next(paises.Length)] : paisNormal;

            return new Transaccion
            {
                Id = id,
                ClienteId = $"C{id:D6}",
                Monto = esFraude ? _random.Next(50000, 200000) : _random.Next(1000, 5000),
                Pais = pais,
                PaisHabitual = paisNormal,
                FechaHora = esFraude
                    ? DateTime.Now.AddHours(_random.Next(0, 5)) // fraude: horarios inusuales
                    : DateTime.Now.AddHours(_random.Next(8, 20)), // normal: horario laboral
                Comercio = comercio,
                Categoria = categoria,
                MetodoPago = metodo,
                EsFraudeReal = esFraude
            };
        }
    }
}