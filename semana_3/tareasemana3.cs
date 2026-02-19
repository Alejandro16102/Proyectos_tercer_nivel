using System;

namespace RegistroEstudiantes
{
    // Clase que representa a un estudiante
    class Estudiante
    {
        // Propiedades del estudiante
        public string ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        
        // Array para almacenar tres números de teléfono
        public string[] Telefonos { get; set; }

        // Constructor de la clase Estudiante
        public Estudiante()
        {
            // Inicializa el array de teléfonos con capacidad para 3 números
            Telefonos = new string[3];
        }

        // Método para mostrar la información del estudiante
        public void MostrarInformacion()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         INFORMACIÓN DEL ESTUDIANTE REGISTRADO          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.WriteLine($"ID:              {ID}");
            Console.WriteLine($"Nombres:         {Nombres}");
            Console.WriteLine($"Apellidos:       {Apellidos}");
            Console.WriteLine($"Dirección:       {Direccion}");
            Console.WriteLine("\nTeléfonos:");
            
            // Recorre el array de teléfonos y los muestra
            for (int i = 0; i < Telefonos.Length; i++)
            {
                if (!string.IsNullOrEmpty(Telefonos[i]))
                {
                    Console.WriteLine($"  Teléfono {i + 1}:  {Telefonos[i]}");
                }
            }
            Console.WriteLine("════════════════════════════════════════════════════════\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Configuración de la consola para caracteres especiales
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║       SISTEMA DE REGISTRO DE ESTUDIANTES              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            // Crear una nueva instancia de Estudiante
            Estudiante estudiante = new Estudiante();

            try
            {
                // Solicitar y capturar el ID del estudiante
                Console.Write("Ingrese el ID del estudiante: ");
                estudiante.ID = Console.ReadLine();

                // Validar que el ID no esté vacío
                if (string.IsNullOrWhiteSpace(estudiante.ID))
                {
                    Console.WriteLine("Error: El ID no puede estar vacío.");
                    return;
                }

                // Solicitar y capturar los nombres
                Console.Write("Ingrese los nombres: ");
                estudiante.Nombres = Console.ReadLine();

                // Solicitar y capturar los apellidos
                Console.Write("Ingrese los apellidos: ");
                estudiante.Apellidos = Console.ReadLine();

                // Solicitar y capturar la dirección
                Console.Write("Ingrese la dirección: ");
                estudiante.Direccion = Console.ReadLine();

                // Capturar los tres números de teléfono usando un bucle
                Console.WriteLine("\n--- Ingrese los números de teléfono ---");
                for (int i = 0; i < estudiante.Telefonos.Length; i++)
                {
                    Console.Write($"Teléfono {i + 1}: ");
                    estudiante.Telefonos[i] = Console.ReadLine();
                    
                    // Validación básica de que no esté vacío
                    if (string.IsNullOrWhiteSpace(estudiante.Telefonos[i]))
                    {
                        Console.WriteLine("  Advertencia: Teléfono vacío, se guardará como 'No registrado'");
                        estudiante.Telefonos[i] = "No registrado";
                    }
                }

                // Mostrar la información capturada
                estudiante.MostrarInformacion();

                // Opción para registrar otro estudiante
                Console.Write("¿Desea registrar otro estudiante? (S/N): ");
                string respuesta = Console.ReadLine()?.ToUpper();
                
                if (respuesta == "S")
                {
                    // Reiniciar el programa (en un caso real, usaríamos un bucle)
                    Console.WriteLine("\nReiniciando el sistema...\n");
                    Main(args);
                }
                else
                {
                    Console.WriteLine("\n¡Gracias por usar el Sistema de Registro!");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPresione cualquier tecla para salir...");
                Console.ReadKey();
            }
        }
    }
}