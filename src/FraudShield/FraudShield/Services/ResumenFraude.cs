using System;
using System.Collections.Generic;
using System.Text;
using FraudShield.Models;
using System.Linq;  

namespace FraudShield.Services;

public class ResumenFraude
{
    private readonly EvaluadorFraude _evaluador = new();
    public void Mostrar(List<ResultadoAnalisis> resultados)
    {
        int aprobadas = 0;
        int canceladas = 0;

        for (int i = 0; i < resultados.Count; i += 5)
        {
            List<ResultadoAnalisis> resultadosTransaccion =
                resultados.Skip(i).Take(5).ToList();

            int riesgoTotal =
                _evaluador.CalcularRiesgoTotal(resultadosTransaccion);

            if (_evaluador.DebeCancelar(riesgoTotal))
            {
                canceladas++;
            }
            else
            {
                aprobadas++;
            }
        }

        int total = aprobadas + canceladas;

        double porcentajeFraude = total == 0
            ? 0
            : (double)canceladas / total * 100;

        Console.WriteLine();
        Console.WriteLine("========== RESUMEN DE FRAUDE ==========");
        Console.WriteLine($"Transacciones analizadas : {total}");
        Console.WriteLine($"Aprobadas                : {aprobadas}");
        Console.WriteLine($"Canceladas               : {canceladas}");
        Console.WriteLine($"Porcentaje de fraude     : {porcentajeFraude:F2}%");
        Console.WriteLine();
        Console.WriteLine("========== TOP DE ALERTAS ==========");

        var topAlertas = resultados
             .Where(r => r.EsSospechosa)
             .GroupBy(r => r.Analizador)
             .OrderByDescending(g => g.Count());

        foreach (var alerta in topAlertas)
        {
            string nombre = alerta.Key switch
            {
                "Monto" => "Monto elevado",
                "Comercio" => "Comercio riesgoso",
                "Horario" => "Horario sospechoso",
                "País" => "País inusual",
                "Frecuencia" => "Alta frecuencia",
                _ => alerta.Key
            };

            Console.WriteLine($"{nombre,-25} : {alerta.Count()}");
        }
    }
}