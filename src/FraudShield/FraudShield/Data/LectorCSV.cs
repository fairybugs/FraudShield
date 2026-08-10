using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using FraudShield.Models;

namespace FraudShield.Data
{
    public class LectorCSV
    {
        public List<Transaccion> LeerArchivo(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($" El archivo {rutaArchivo} no existe.");
                return new List<Transaccion>();
            }

            try
            {
                using var reader = new StreamReader(rutaArchivo);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null, // Ignora campos faltantes
                    BadDataFound = null       // Ignora datos corruptos
                });

                var transacciones = csv.GetRecords<Transaccion>();
                return new List<Transaccion>(transacciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error al leer el archivo: {ex.Message}");
                return new List<Transaccion>();
            }
        }
    }
}