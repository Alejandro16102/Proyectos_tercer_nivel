using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Lista con los precios predefinidos
        List<int> precios = new List<int> { 50, 75, 46, 22, 80, 65, 8 };
        
        Console.WriteLine("=== ANÁLISIS DE PRECIOS ===\n");
        
        // Mostrar todos los precios
        Console.WriteLine("Precios almacenados:");
        Console.WriteLine(string.Join(", ", precios));
        
        // Obtener el precio menor y mayor
        int precioMinimo = precios.Min();
        int precioMaximo = precios.Max();
        
        // Mostrar resultados
        Console.WriteLine($"\nPrecio MENOR: {precioMinimo}€");
        Console.WriteLine($"Precio MAYOR: {precioMaximo}€");
        
        Console.ReadKey();
    }
}