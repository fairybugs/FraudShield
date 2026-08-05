using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FraudShield.Data;

/// <summary>
/// Contiene todos los catálogos utilizados para generar
/// transacciones con datos similares a un entorno real.
/// </summary>
public static class CatalogoDatos
{
    #region Países y ciudades

    public static readonly Dictionary<string, string[]> CiudadesPorPais = new()
    {
        {
            "República Dominicana",
            new[]
            {
                "Santo Domingo",
                "Santiago",
                "La Romana",
                "San Pedro de Macorís",
                "Puerto Plata",
                "Punta Cana"
            }
        },

        {
            "Estados Unidos",
            new[]
            {
                "Miami",
                "New York",
                "Orlando",
                "Dallas",
                "Boston",
                "Los Angeles"
            }
        },

        {
            "España",
            new[]
            {
                "Madrid",
                "Barcelona",
                "Valencia",
                "Sevilla"
            }
        },

        {
            "México",
            new[]
            {
                "Ciudad de México",
                "Monterrey",
                "Guadalajara"
            }
        },

        {
            "Colombia",
            new[]
            {
                "Bogotá",
                "Medellín",
                "Cali"
            }
        },

        {
            "Panamá",
            new[]
            {
                "Ciudad de Panamá",
                "David"
            }
        }
    };

    #endregion

    #region Monedas

    public static readonly Dictionary<string, string> Monedas = new()
    {
        { "República Dominicana", "DOP" },
        { "Estados Unidos", "USD" },
        { "España", "EUR" },
        { "México", "MXN" },
        { "Colombia", "COP" },
        { "Panamá", "PAB" }
    };

    #endregion

    #region Comercios

    public static readonly string[] ComerciosNormales =
    {
        "Jumbo",
        "La Sirena",
        "Bravo",
        "PriceSmart",
        "Farmacia Carol",
        "Banco Popular",
        "Banco BHD",
        "Adrian Tropical",
        "KFC",
        "McDonald's",
        "Subway",
        "IKEA",
        "Plaza Lama",
        "Sirena Market"
    };

    public static readonly string[] ComerciosRiesgosos =
    {
        "Crypto Exchange",
        "Online Casino",
        "Betting Platform",
        "Gift Cards Store",
        "Luxury Watches",
        "Gaming Portal"
    };

    #endregion


    #region Métodos de pago

    public static readonly string[] MetodosPago =
    {
        "Tarjeta de Débito",
        "Tarjeta de Crédito",
        "Transferencia",
        "Apple Pay",
        "Google Pay",
        "Pago Móvil"
    };

    #endregion

    #region Categorías por comercio

    public static readonly Dictionary<string, string> CategoriaPorComercio = new()
{
    { "Jumbo", "Supermercado" },
    { "La Sirena", "Supermercado" },
    { "Bravo", "Supermercado" },
    { "PriceSmart", "Supermercado" },

    { "Farmacia Carol", "Farmacia" },

    { "Banco Popular", "Finanzas" },
    { "Banco BHD", "Finanzas" },

    { "Adrian Tropical", "Restaurante" },
    { "KFC", "Restaurante" },
    { "McDonald's", "Restaurante" },
    { "Subway", "Restaurante" },

    { "IKEA", "Hogar" },
    { "Plaza Lama", "Electrónica" },

    { "Sirena Market", "Supermercado" },

    { "Crypto Exchange", "Criptomonedas" },
    { "Online Casino", "Apuestas" },
    { "Betting Platform", "Apuestas" },
    { "Gift Cards Store", "Gift Cards" },
    { "Luxury Watches", "Lujo" },
    { "Gaming Portal", "Videojuegos" }
};

    #endregion

    #region Métodos auxiliares

    public static string ObtenerPais(Random random)
    {
        var paises = CiudadesPorPais.Keys.ToArray();
        return paises[random.Next(paises.Length)];
    }


    public static string ObtenerCiudad(Random random, string pais)
    {
        return CiudadesPorPais[pais][random.Next(CiudadesPorPais[pais].Length)];
    }

    public static string ObtenerMoneda(string pais)
    {
        return Monedas[pais];
    }

    public static string ObtenerComercioNormal(Random random)
    {
        return ComerciosNormales[random.Next(ComerciosNormales.Length)];
    }

    public static string ObtenerComercioRiesgoso(Random random)
    {
        return ComerciosRiesgosos[random.Next(ComerciosRiesgosos.Length)];
    }

    public static string ObtenerCategoria(string comercio)
    {
        return CategoriaPorComercio.TryGetValue(comercio, out string? categoria)
            ? categoria
            : "Otros";
    }

    public static string ObtenerMetodoPago(Random random)
    {
        return MetodosPago[random.Next(MetodosPago.Length)];
    }

    #endregion
}