using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Lista para almacenar los números ganadores
        List<int> numerosGanadores = new List<int>();
        
        Console.WriteLine("=== LOTERÍA PRIMITIVA ===");
        Console.WriteLine("Introduce los 6 números ganadores (del 1 al 49):\n");
        
        // Solicitar los 6 números al usuario
        for (int i = 1; i <= 6; i++)
        {
            bool numeroValido = false;
            
            while (!numeroValido)
            {
                Console.Write($"Número {i}: ");
                string entrada = Console.ReadLine();
                
                // Validar que la entrada sea un número
                if (int.TryParse(entrada, out int numero))
                {
                    // Validar que esté en el rango correcto
                    if (numero >= 1 && numero <= 49)
                    {
                        // Validar que no esté repetido
                        if (!numerosGanadores.Contains(numero))
                        {
                            numerosGanadores.Add(numero);
                            numeroValido = true;
                        }
                        else
                        {
                            Console.WriteLine("Error: Ese número ya fue ingresado. Intenta con otro.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: El número debe estar entre 1 y 49.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Debes introducir un número válido.\n");
                }
            }
        }
        
        // Ordenar los números de menor a mayor
        numerosGanadores.Sort();
        
        // Mostrar los números ordenados
        Console.WriteLine("\n=== NÚMEROS GANADORES ORDENADOS ===");
        Console.WriteLine(string.Join(" - ", numerosGanadores));
        
        Console.WriteLine("\n¡Buena suerte!");
        Console.ReadKey();
    }
}