using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Lista de asignaturas del curso
        List<string> asignaturas = new List<string> 
        { 
            "Matemáticas", 
            "Física", 
            "Química", 
            "Historia", 
            "Lengua" 
        };
        
        // Lista para almacenar las asignaturas a repetir
        List<string> asignaturasARepetir = new List<string>();
        
        Console.WriteLine("=== REGISTRO DE CALIFICACIONES ===\n");
        Console.WriteLine("Introduce la nota de cada asignatura (0-10):\n");
        
        // Preguntar la nota de cada asignatura
        foreach (string asignatura in asignaturas)
        {
            bool notaValida = false;
            double nota = 0;
            
            while (!notaValida)
            {
                Console.Write($"{asignatura}: ");
                string entrada = Console.ReadLine();
                
                // Validar que la entrada sea un número
                if (double.TryParse(entrada, out nota))
                {
                    // Validar que esté en el rango correcto
                    if (nota >= 0 && nota <= 10)
                    {
                        notaValida = true;
                        
                        // Si la nota es menor a 7, añadir a la lista de repetir
                        if (nota < 7)
                        {
                            asignaturasARepetir.Add(asignatura);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: La nota debe estar entre 0 y 10.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Debes introducir un número válido.\n");
                }
            }
        }
        
        // Mostrar resultados
        Console.WriteLine("\n=== RESULTADOS ===");
        
        if (asignaturasARepetir.Count == 0)
        {
            Console.WriteLine("\n¡Felicidades! Has aprobado todas las asignaturas.");
        }
        else
        {
            Console.WriteLine("\nAsignaturas que debes repetir:");
            foreach (string asignatura in asignaturasARepetir)
            {
                Console.WriteLine($"- {asignatura}");
            }
            
            Console.WriteLine($"\nTotal de asignaturas a repetir: {asignaturasARepetir.Count}");
        }
        
        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}