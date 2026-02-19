using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Crear lista con números del 1 al 10
        List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        Console.WriteLine("=== NÚMEROS EN ORDEN INVERSO ===\n");
        
        // Mostrar orden original
        Console.WriteLine("Orden original:");
        Console.WriteLine(string.Join(", ", numeros));
        
        // Invertir el orden
        numeros.Reverse();
        
        // Mostrar orden inverso
        Console.WriteLine("\nOrden inverso:");
        Console.WriteLine(string.Join(", ", numeros));
        
        Console.ReadKey();
    }
}