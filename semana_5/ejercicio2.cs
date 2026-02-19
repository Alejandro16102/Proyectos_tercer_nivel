using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== CÁLCULO DE MEDIA Y DESVIACIÓN TÍPICA ===\n");
        
        // Solicitar la muestra de números
        Console.WriteLine("Introduce una muestra de números separados por comas:");
        Console.Write("Ejemplo: 10, 20, 30, 40, 50\n> ");
        string entrada = Console.ReadLine();
        
        try
        {
            // Separar los números y convertirlos a una lista
            List<double> numeros = entrada
                .Split(',')
                .Select(n => double.Parse(n.Trim()))
                .ToList();
            
            // Validar que haya al menos un número
            if (numeros.Count == 0)
            {
                Console.WriteLine("\nError: Debes introducir al menos un número.");
                Console.ReadKey();
                return;
            }
            
            // Calcular la media
            double media = numeros.Average();
            
            // Calcular la desviación típica
            double sumaCuadrados = numeros.Sum(n => Math.Pow(n - media, 2));
            double desviacionTipica = Math.Sqrt(sumaCuadrados / numeros.Count);
            
            // Mostrar resultados
            Console.WriteLine("\n=== RESULTADOS ===");
            Console.WriteLine($"Números ingresados: {string.Join(", ", numeros)}");
            Console.WriteLine($"Cantidad de números: {numeros.Count}");
            Console.WriteLine($"\nMedia: {media:F2}");
            Console.WriteLine($"Desviación Típica: {desviacionTipica:F2}");
        }
        catch (FormatException)
        {
            Console.WriteLine("\nError: Formato incorrecto. Asegúrate de introducir solo números separados por comas.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }
        
        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
