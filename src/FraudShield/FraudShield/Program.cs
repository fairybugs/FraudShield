using System;
using FraudShield.Generador;

namespace FraudShield
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            var generador = new GeneradorCSV();

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("SISTEMA DETECTOR DE FRAUDE BANCARIO");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Generar archivo CSV");
                Console.WriteLine("2. Leer archivo CSV (pendiente)");
                Console.WriteLine("3. Ejecutar versión secuencial (pendiente)");
                Console.WriteLine("4. Ejecutar versión paralela (pendiente)");
                Console.WriteLine("5. Comparar resultados (pendiente)");
                Console.WriteLine("6. Mostrar estadísticas (pendiente)");
                Console.WriteLine("7. Salir");
                Console.WriteLine("==========================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine() ?? string.Empty;

                switch (opcion)
                {
                    case "1":
                        Console.Write("¿Cuántas transacciones desea generar? ");
                        int total = int.Parse(Console.ReadLine() ?? "0");

                        Console.Write("¿Cuántas transacciones fraudulentas desea generar? ");
                        int fraudulentas = int.Parse(Console.ReadLine() ?? "0");

                        string rutaArchivo = $"transacciones_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                        generador.GenerarArchivo(total, fraudulentas, rutaArchivo);
                        Console.WriteLine("Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine(" Aquí irá la lógica de LectorCSV.cs");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.WriteLine(" Aquí se ejecutará la versión secuencial");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.WriteLine(" Aquí se ejecutará la versión paralela");
                        Console.ReadKey();
                        break;

                    case "5":
                        Console.WriteLine(" Aquí se compararán los resultados");
                        Console.ReadKey();
                        break;

                    case "6":
                        Console.WriteLine(" Aquí se mostrarán las estadísticas");
                        Console.ReadKey();
                        break;

                    case "7":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine(" Opción inválida. Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}