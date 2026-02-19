using System;

namespace SistemaEstudiantesRedesIII
{
    // Clase para representar un estudiante
    class Estudiante
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public double NotaDefinitiva { get; set; }
        public Estudiante Siguiente { get; set; }

        public Estudiante(string cedula, string nombre, string apellido, string correo, double notaDefinitiva)
        {
            Cedula = cedula;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            NotaDefinitiva = notaDefinitiva;
            Siguiente = null;
        }

        public bool EstaAprobado()
        {
            return NotaDefinitiva >= 6.0;
        }

        public string Estado()
        {
            return EstaAprobado() ? "APROBADO" : "REPROBADO";
        }
    }

    // Clase para gestionar la lista enlazada de estudiantes
    class ListaEstudiantes
    {
        private Estudiante cabeza;
        private const double NOTA_APROBATORIA = 6.0;

        public ListaEstudiantes()
        {
            cabeza = null;
        }

        // a. Agregar estudiante
        public void AgregarEstudiante()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════╗");
            Console.WriteLine("║       AGREGAR NUEVO ESTUDIANTE             ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            Console.Write("\nCédula: ");
            string cedula = Console.ReadLine();

            // Verificar si la cédula ya existe
            if (BuscarEstudiantePorCedula(cedula) != null)
            {
                Console.WriteLine("\n❌ Error: Ya existe un estudiante con esa cédula.");
                return;
            }

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Correo: ");
            string correo = Console.ReadLine();

            Console.Write("Nota Definitiva (1-10): ");
            double nota;
            while (!double.TryParse(Console.ReadLine(), out nota) || nota < 1 || nota > 10)
            {
                Console.Write("❌ Nota inválida. Debe estar entre 1 y 10: ");
            }

            Estudiante nuevo = new Estudiante(cedula, nombre, apellido, correo, nota);

            // APROBADOS: insertar al INICIO
            // REPROBADOS: insertar al FINAL
            if (nuevo.EstaAprobado())
            {
                InsertarAlInicio(nuevo);
                Console.WriteLine($"\n✅ ¡Estudiante APROBADO agregado al inicio de la lista!");
            }
            else
            {
                InsertarAlFinal(nuevo);
                Console.WriteLine($"\n✅ ¡Estudiante REPROBADO agregado al final de la lista!");
            }

            Console.WriteLine($"   Estado: {nuevo.Estado()} - Nota: {nota:F2}");
        }

        // Insertar al inicio (para aprobados)
        private void InsertarAlInicio(Estudiante nuevo)
        {
            nuevo.Siguiente = cabeza;
            cabeza = nuevo;
        }

        // Insertar al final (para reprobados)
        private void InsertarAlFinal(Estudiante nuevo)
        {
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Estudiante temp = cabeza;
                while (temp.Siguiente != null)
                {
                    temp = temp.Siguiente;
                }
                temp.Siguiente = nuevo;
            }
        }

        // b. Buscar estudiante por cédula
        private Estudiante BuscarEstudiantePorCedula(string cedula)
        {
            Estudiante temp = cabeza;
            while (temp != null)
            {
                if (temp.Cedula.Equals(cedula, StringComparison.OrdinalIgnoreCase))
                {
                    return temp;
                }
                temp = temp.Siguiente;
            }
            return null;
        }

        public void BuscarYMostrarEstudiante()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════╗");
            Console.WriteLine("║         BUSCAR ESTUDIANTE                  ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            Console.Write("\nIngrese la cédula: ");
            string cedula = Console.ReadLine();

            Estudiante encontrado = BuscarEstudiantePorCedula(cedula);

            if (encontrado != null)
            {
                Console.WriteLine("\n┌─────────────────────────────────────────┐");
                Console.WriteLine("│     ESTUDIANTE ENCONTRADO               │");
                Console.WriteLine("└─────────────────────────────────────────┘");
                MostrarEstudiante(encontrado);
            }
            else
            {
                Console.WriteLine("\n❌ Estudiante no encontrado.");
            }
        }

        // c. Eliminar estudiante
        public void EliminarEstudiante()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════╗");
            Console.WriteLine("║         ELIMINAR ESTUDIANTE                ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            Console.Write("\nIngrese la cédula del estudiante a eliminar: ");
            string cedula = Console.ReadLine();

            if (cabeza == null)
            {
                Console.WriteLine("\n❌ No hay estudiantes registrados.");
                return;
            }

            // Si el estudiante a eliminar es el primero
            if (cabeza.Cedula.Equals(cedula, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\nEliminando: {cabeza.Nombre} {cabeza.Apellido} - {cabeza.Estado()}");
                cabeza = cabeza.Siguiente;
                Console.WriteLine("✅ ¡Estudiante eliminado exitosamente!");
                return;
            }

            // Buscar el estudiante en el resto de la lista
            Estudiante anterior = cabeza;
            Estudiante actual = cabeza.Siguiente;

            while (actual != null)
            {
                if (actual.Cedula.Equals(cedula, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\nEliminando: {actual.Nombre} {actual.Apellido} - {actual.Estado()}");
                    anterior.Siguiente = actual.Siguiente;
                    Console.WriteLine("✅ ¡Estudiante eliminado exitosamente!");
                    return;
                }
                anterior = actual;
                actual = actual.Siguiente;
            }

            Console.WriteLine("\n❌ Estudiante no encontrado.");
        }

        // d. Total estudiantes aprobados
        public int ContarAprobados()
        {
            int contador = 0;
            Estudiante temp = cabeza;

            while (temp != null)
            {
                if (temp.EstaAprobado())
                {
                    contador++;
                }
                temp = temp.Siguiente;
            }

            return contador;
        }

        // e. Total estudiantes reprobados
        public int ContarReprobados()
        {
            int contador = 0;
            Estudiante temp = cabeza;

            while (temp != null)
            {
                if (!temp.EstaAprobado())
                {
                    contador++;
                }
                temp = temp.Siguiente;
            }

            return contador;
        }

        // Mostrar estadísticas
        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════╗");
            Console.WriteLine("║           ESTADÍSTICAS                     ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            int aprobados = ContarAprobados();
            int reprobados = ContarReprobados();
            int total = aprobados + reprobados;

            Console.WriteLine($"\n📊 Total de estudiantes: {total}");
            Console.WriteLine($"✅ Estudiantes aprobados: {aprobados}");
            Console.WriteLine($"❌ Estudiantes reprobados: {reprobados}");

            if (total > 0)
            {
                double porcentajeAprobados = (aprobados * 100.0) / total;
                double porcentajeReprobados = (reprobados * 100.0) / total;
                Console.WriteLine($"\n📈 Porcentaje de aprobación: {porcentajeAprobados:F2}%");
                Console.WriteLine($"📉 Porcentaje de reprobación: {porcentajeReprobados:F2}%");
            }
        }

        // Mostrar todos los estudiantes
        public void MostrarTodosLosEstudiantes()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    LISTA COMPLETA DE ESTUDIANTES - REDES III                      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════╝");

            if (cabeza == null)
            {
                Console.WriteLine("\n❌ No hay estudiantes registrados.");
                return;
            }

            Estudiante temp = cabeza;
            int posicion = 1;

            Console.WriteLine("\n" + new string('─', 90));
            Console.WriteLine("{0,-4} {1,-12} {2,-15} {3,-15} {4,-25} {5,-8} {6,-10}",
                "#", "CÉDULA", "NOMBRE", "APELLIDO", "CORREO", "NOTA", "ESTADO");
            Console.WriteLine(new string('─', 90));

            while (temp != null)
            {
                string estado = temp.EstaAprobado() ? "✅ APROBADO" : "❌ REPROBADO";
                Console.WriteLine("{0,-4} {1,-12} {2,-15} {3,-15} {4,-25} {5,-8:F2} {6,-10}",
                    posicion, temp.Cedula, temp.Nombre, temp.Apellido, temp.Correo, temp.NotaDefinitiva, estado);
                temp = temp.Siguiente;
                posicion++;
            }

            Console.WriteLine(new string('─', 90));
        }

        // Mostrar estudiantes aprobados
        public void MostrarAprobados()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════╗");
            Console.WriteLine("║       ESTUDIANTES APROBADOS                ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            Estudiante temp = cabeza;
            bool hayAprobados = false;

            Console.WriteLine("\n" + new string('─', 80));
            Console.WriteLine("{0,-12} {1,-20} {2,-30} {3,-8}",
                "CÉDULA", "NOMBRE COMPLETO", "CORREO", "NOTA");
            Console.WriteLine(new string('─', 80));

            while (temp != null)
            {
                if (temp.EstaAprobado())
                {
                    Console.WriteLine("{0,-12} {1,-20} {2,-30} {3,-8:F2}",
                        temp.Cedula, $"{temp.Nombre} {temp.Apellido}", temp.Correo, temp.NotaDefinitiva);
                    hayAprobados = true;
                }
                temp = temp.Siguiente;
            }

            Console.WriteLine(new string('─', 80));

            if (!hayAprobados)
            {
                Console.WriteLine("No hay estudiantes aprobados.");
            }
        }

        // Mostrar estudiantes reprobados
        public void MostrarReprobados()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════╗");
            Console.WriteLine("║       ESTUDIANTES REPROBADOS               ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            Estudiante temp = cabeza;
            bool hayReprobados = false;

            Console.WriteLine("\n" + new string('─', 80));
            Console.WriteLine("{0,-12} {1,-20} {2,-30} {3,-8}",
                "CÉDULA", "NOMBRE COMPLETO", "CORREO", "NOTA");
            Console.WriteLine(new string('─', 80));

            while (temp != null)
            {
                if (!temp.EstaAprobado())
                {
                    Console.WriteLine("{0,-12} {1,-20} {2,-30} {3,-8:F2}",
                        temp.Cedula, $"{temp.Nombre} {temp.Apellido}", temp.Correo, temp.NotaDefinitiva);
                    hayReprobados = true;
                }
                temp = temp.Siguiente;
            }

            Console.WriteLine(new string('─', 80));

            if (!hayReprobados)
            {
                Console.WriteLine("No hay estudiantes reprobados.");
            }
        }

        // Método auxiliar para mostrar un estudiante
        private void MostrarEstudiante(Estudiante e)
        {
            Console.WriteLine($"\n  Cédula:         {e.Cedula}");
            Console.WriteLine($"  Nombre:         {e.Nombre}");
            Console.WriteLine($"  Apellido:       {e.Apellido}");
            Console.WriteLine($"  Correo:         {e.Correo}");
            Console.WriteLine($"  Nota Definitiva: {e.NotaDefinitiva:F2}");
            Console.WriteLine($"  Estado:         {e.Estado()}");
        }
    }

    // Clase principal del programa
    class Program
    {
        static void MostrarMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  SISTEMA DE GESTIÓN DE ESTUDIANTES - REDES III        ║");
            Console.WriteLine("║  Universidad Estatal Amazónica                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.WriteLine("\n   GESTIÓN DE ESTUDIANTES");
            Console.WriteLine("  1. Agregar estudiante");
            Console.WriteLine("  2. Buscar estudiante por cédula");
            Console.WriteLine("  3. Eliminar estudiante");
            Console.WriteLine("\n   CONSULTAS Y REPORTES");
            Console.WriteLine("  4. Ver todos los estudiantes");
            Console.WriteLine("  5. Ver estudiantes aprobados");
            Console.WriteLine("  6. Ver estudiantes reprobados");
            Console.WriteLine("  7. Ver estadísticas generales");
            Console.WriteLine("\n   SALIR");
            Console.WriteLine("  8. Salir del sistema");
            Console.Write("\n  Seleccione una opción: ");
        }

        static void Main(string[] args)
        {
            ListaEstudiantes lista = new ListaEstudiantes();
            int opcion;

            do
            {
                MostrarMenu();

                while (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.Write("  ❌ Opción inválida. Intente nuevamente: ");
                }

                Console.Clear();

                switch (opcion)
                {
                    case 1:
                        lista.AgregarEstudiante();
                        break;
                    case 2:
                        lista.BuscarYMostrarEstudiante();
                        break;
                    case 3:
                        lista.EliminarEstudiante();
                        break;
                    case 4:
                        lista.MostrarTodosLosEstudiantes();
                        break;
                    case 5:
                        lista.MostrarAprobados();
                        break;
                    case 6:
                        lista.MostrarReprobados();
                        break;
                    case 7:
                        lista.MostrarEstadisticas();
                        break;
                    case 8:
                        Console.WriteLine("\n╔════════════════════════════════════════════╗");
                        Console.WriteLine("║  ¡Gracias por usar el sistema!             ║");
                        Console.WriteLine("║  Universidad Estatal Amazónica             ║");
                        Console.WriteLine("╚════════════════════════════════════════════╝\n");
                        break;
                    default:
                        Console.WriteLine("\n❌ Opción inválida. Intente nuevamente.");
                        break;
                }

                if (opcion != 8)
                {
                    Console.WriteLine("\n\nPresione Enter para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (opcion != 8);
        }
    }
}