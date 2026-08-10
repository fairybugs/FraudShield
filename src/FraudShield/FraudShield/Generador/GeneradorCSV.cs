using CsvHelper;
using FraudShield.Data;
using FraudShield.Models;
using System.Globalization;

namespace FraudShield.Generador;

/// <summary>
/// Genera archivos CSV con transacciones bancarias simuladas.
/// </summary>
public class GeneradorCSV
{
    #region Campos

    private readonly Random _random = new();

    private const int CantidadClientes = 5000;

    #endregion

    #region Métodos públicos

    /// <summary>
    /// Genera un archivo CSV con transacciones normales y fraudulentas.
    /// </summary>

    public void GenerarArchivo(
        int total,
        int fraudulentas,
        string rutaArchivo)
    {
        if (fraudulentas > total)
            throw new ArgumentException(
                "La cantidad de fraudes no puede ser mayor que el total de transacciones.");

        List<Cliente> clientes = CrearCatalogoClientes();

        List<Transaccion> transacciones = new(total);

        int normales = total - fraudulentas;

        GenerarTransaccionesNormales(
            transacciones,
            clientes,
            normales);

        GenerarTransaccionesFraudulentas(
            transacciones,
            clientes,
            fraudulentas);

        transacciones = transacciones
            .OrderBy(x => _random.Next())
            .ToList();

        // Reasignar los IDs para que vuelvan a ser consecutivos.
        for (int i = 0; i < transacciones.Count; i++)
        {
            transacciones[i].Id = i + 1;
        }

        GuardarArchivo(
            rutaArchivo,
            transacciones);

        Console.WriteLine();
        Console.WriteLine("====================================");
        Console.WriteLine("Archivo generado correctamente.");
        Console.WriteLine($"Ruta: {rutaArchivo}");
        Console.WriteLine($"Total de transacciones: {total:N0}");
        Console.WriteLine($"Normales: {normales:N0}");
        Console.WriteLine($"Fraudulentas: {fraudulentas:N0}");
        Console.WriteLine("====================================");
    }

    #endregion

    #region Catálogo de clientes

    /// <summary>
    /// Crea un conjunto de clientes con comportamientos habituales.
    /// </summary>
    private List<Cliente> CrearCatalogoClientes()
    {
        List<Cliente> clientes = new(CantidadClientes);

        for (int i = 1; i <= CantidadClientes; i++)
        {
            string pais = GenerarPaisHabitual();

            string ciudad =
                CatalogoDatos.ObtenerCiudad(_random, pais);

            clientes.Add(new Cliente
            {
                ClienteId = $"C{i:D5}",

                PaisHabitual = pais,

                CiudadHabitual = ciudad,

                MontoPromedio =
                    _random.Next(1500, 12000),

                HoraInicioHabitual = 7,

                HoraFinHabitual = 21
            });
        }

        return clientes;
    }

    #endregion

    #region Guardar archivo

    /// <summary>
    /// Guarda todas las transacciones en un archivo CSV.
    /// </summary>
    private void GuardarArchivo(
        string rutaArchivo,
        List<Transaccion> transacciones)
    {
        using StreamWriter writer = new(rutaArchivo);

        using CsvWriter csv =
            new(writer, CultureInfo.InvariantCulture);

        csv.WriteRecords(transacciones);
    }

    #endregion
    #region Generación de transacciones

    /// <summary>
    /// Genera las transacciones normales.
    /// </summary>
    private void GenerarTransaccionesNormales(
        List<Transaccion> transacciones,
        List<Cliente> clientes,
        int cantidad)
    {
        long id = 1;

        for (int i = 0; i < cantidad; i++)
        {
            Cliente cliente = clientes[_random.Next(clientes.Count)];

            transacciones.Add(
                CrearTransaccion(cliente, id++, false));
        }
    }

    /// <summary>
    /// Genera las transacciones fraudulentas.
    /// </summary>
    private void GenerarTransaccionesFraudulentas(
    List<Transaccion> transacciones,
    List<Cliente> clientes,
    int cantidad)
    {
        long id = transacciones.Count + 1;

        int restantes = cantidad;

        while (restantes > 0)
        {
            Cliente cliente = clientes[_random.Next(clientes.Count)];

            bool generarRafaga =
                restantes >= 5 &&
                _random.Next(100) < 20;

            if (generarRafaga)
            {
                GenerarRafagaFraudulenta(
                    transacciones,
                    cliente,
                    ref id);

                restantes -= 5;
            }
            else
            {
                Transaccion transaccion =
                    CrearTransaccion(cliente, id++, true);

                AplicarFraude(transaccion, cliente);

                transacciones.Add(transaccion);

                restantes--;
            }
        }
    }

    #endregion

    /// <summary>
    /// Construye una transacción basada en el comportamiento habitual del cliente.
    /// </summary>
    private Transaccion CrearTransaccion(
        Cliente cliente,
        long id,
        bool esFraude)
    {
        string pais = cliente.PaisHabitual;

        string ciudad = cliente.CiudadHabitual;

        string comercio =
            CatalogoDatos.ObtenerComercioNormal(_random);

        return new Transaccion
        {
            Id = id,

            ClienteId = cliente.ClienteId,

            Monto =
                GenerarMonto(cliente),

            MontoPromedioCliente =
                cliente.MontoPromedio,

            Pais = pais,

            Ciudad = ciudad,

            PaisHabitual = cliente.PaisHabitual,

            FechaHora =
                GenerarFecha(cliente),

            Comercio = comercio,

            Categoria =
                CatalogoDatos.ObtenerCategoria(comercio),

            MetodoPago =
                CatalogoDatos.ObtenerMetodoPago(_random),

            Moneda =
                CatalogoDatos.ObtenerMoneda(pais),

            EsFraudeReal = esFraude
        };
    }

    #region Métodos auxiliares

    private string GenerarPaisHabitual()
    {
        int probabilidad = _random.Next(100);

        if (probabilidad < 90)
            return "República Dominicana";

        return CatalogoDatos.ObtenerPais(_random);
    }

    private decimal GenerarMonto(Cliente cliente)
    {
        decimal variacion =
            (decimal)(_random.NextDouble() * 0.40 - 0.20);

        decimal monto =
            cliente.MontoPromedio +
            (cliente.MontoPromedio * variacion);

        return Math.Round(monto, 2);
    }

    private DateTime GenerarFecha(Cliente cliente)
    {
        DateTime fecha =
            DateTime.Now.Date.AddDays(-_random.Next(30));

        int hora =
            _random.Next(
                cliente.HoraInicioHabitual,
                cliente.HoraFinHabitual + 1);

        int minuto = _random.Next(60);

        int segundo = _random.Next(60);

        return fecha
            .AddHours(hora)
            .AddMinutes(minuto)
            .AddSeconds(segundo);
    }


    private void AplicarFraude(
    Transaccion transaccion,
    Cliente cliente)
    {
        int tipoFraude = _random.Next(5);

        switch (tipoFraude)
        {
            case 0:

                AplicarFraudeMonto(transaccion);
                AplicarFraudePais(transaccion, cliente);

                break;

            // Monto + Horario
            case 1:

                AplicarFraudeMonto(transaccion);
                AplicarFraudeHorario(transaccion);

                break;

            case 2:

                AplicarFraudePais(transaccion, cliente);
                AplicarFraudeComercio(transaccion);
                AplicarFraudeHorario(transaccion);

                break;

            case 3:

                AplicarFraudeMonto(transaccion);
                AplicarFraudePais(transaccion, cliente);
                AplicarFraudeHorario(transaccion);

                break;

            case 4:

                AplicarFraudeMonto(transaccion);
                AplicarFraudeComercio(transaccion);
                AplicarFraudeHorario(transaccion);

                break;
        }
    }

    private void AplicarFraudeMonto(
    Transaccion transaccion)
    {
        decimal factor = _random.Next(5, 11);

        transaccion.Monto = Math.Round(transaccion.Monto * factor, 2);
    }

    private void GenerarRafagaFraudulenta(
    List<Transaccion> transacciones,
    Cliente cliente,
    ref long id)
    {
        DateTime fechaBase = GenerarFecha(cliente);

        for (int i = 0; i < 5; i++)
        {
            Transaccion transaccion =
                CrearTransaccion(cliente, id++, true);

            transaccion.FechaHora =
                fechaBase.AddMinutes(i);

            AplicarFraudeMonto(transaccion);
            AplicarFraudeComercio(transaccion);

            transacciones.Add(transaccion);
        }
    }

    private void AplicarFraudePais(
    Transaccion transaccion,
    Cliente cliente)
    {
        string pais =
            CatalogoDatos.ObtenerPais(_random);

        while (pais == cliente.PaisHabitual)
        {
            pais = CatalogoDatos.ObtenerPais(_random);
        }

        transaccion.Pais = pais;

        transaccion.Ciudad =
            CatalogoDatos.ObtenerCiudad(_random, pais);

        transaccion.Moneda =
            CatalogoDatos.ObtenerMoneda(pais);
    }

    private void AplicarFraudeHorario(
    Transaccion transaccion)
    {
        int hora;

        if (_random.Next(2) == 0)
        {
            hora = _random.Next(0, 6);   // madrugada
        }
        else
        {
            hora = _random.Next(22, 24); // 22:00 o 23:00
        }

        transaccion.FechaHora =
            transaccion.FechaHora.Date
            .AddHours(hora)
            .AddMinutes(_random.Next(60))
            .AddSeconds(_random.Next(60));
    }

    private void AplicarFraudeComercio(
    Transaccion transaccion)
    {
        string comercio =
            CatalogoDatos.ObtenerComercioRiesgoso(_random);

        transaccion.Comercio = comercio;

        transaccion.Categoria =
            CatalogoDatos.ObtenerCategoria(comercio);
    }

    #endregion

}