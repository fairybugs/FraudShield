using FraudShield.Models;

Transaccion transaccion = new Transaccion
{
    Id = 1,
    ClienteId = "CLI000001",
    Monto = 3500.50m,
    Pais = "Republica Dominicana",
    PaisHabitual = "Republica Dominicana",
    FechaHora = DateTime.Now,
    Comercio = "Supermercado",
    Categoria = "Alimentos",
    MetodoPago = "TarjetaDebito",
    EsFraudeReal = false
};

Console.WriteLine("Transacción creada correctamente.");
Console.WriteLine($"ID: {transaccion.Id}");
Console.WriteLine($"Cliente: {transaccion.ClienteId}");
Console.WriteLine($"Monto: RD$ {transaccion.Monto}");
Console.WriteLine($"País: {transaccion.Pais}");