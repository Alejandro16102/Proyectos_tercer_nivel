using System;

namespace SistemaParqueoUEA
{
    // Clase para representar un vehículo
    class Vehiculo
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public double Precio { get; set; }
        public Vehiculo Siguiente { get; set; }

        public Vehiculo(string placa, string marca, string modelo, int anio, double precio)
        {
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            Precio = precio;
            Siguiente = null;
        }
    }

    // Clase para gestionar la lista enlazada de vehículos
    class ListaVehiculos
    {
        private Vehiculo cabeza;

        public ListaVehiculos()
        {
            cabeza = null;
        }

        // a. Agregar vehículo
        public void AgregarVehiculo()
        {
            Console.WriteLine("\n=== AGREGAR VEHICULO ===");
            
            Console.Write("Placa: ");
            string placa = Console.ReadLine();

            // Verificar si la placa ya existe
            if (BuscarVehiculoPorPlaca(placa) != null)
            {
                Console.WriteLine("\nError: Ya existe un vehículo con esa placa.");
                return;
            }

            Console.Write("Marca: ");
            string marca = Console.ReadLine();

            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Año: ");
            int anio;
            while (!int.TryParse(Console.ReadLine(), out anio))
            {
                Console.Write("Año inválido. Intente nuevamente: ");
            }

            Console.Write("Precio: $");
            double precio;
            while (!double.TryParse(Console.ReadLine(), out precio))
            {
                Console.Write("Precio inválido. Intente nuevamente: $");
            }

            Vehiculo nuevo = new Vehiculo(placa, marca, modelo, anio, precio);

            // Insertar al final de la lista
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Vehiculo temp = cabeza;
                while (temp.Siguiente != null)
                {
                    temp = temp.Siguiente;
                }
                temp.Siguiente = nuevo;
            }

            Console.WriteLine("\n¡Vehículo agregado exitosamente!");
        }

        // b. Buscar vehículo por placa
        private Vehiculo BuscarVehiculoPorPlaca(string placa)
        {
            Vehiculo temp = cabeza;
            while (temp != null)
            {
                if (temp.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
                {
                    return temp;
                }
                temp = temp.Siguiente;
            }
            return null;
        }

        public void BuscarYMostrarVehiculo()
        {
            Console.WriteLine("\n=== BUSCAR VEHICULO ===");
            Console.Write("Ingrese la placa: ");
            string placa = Console.ReadLine();

            Vehiculo encontrado = BuscarVehiculoPorPlaca(placa);

            if (encontrado != null)
            {
                Console.WriteLine("\n--- Vehículo Encontrado ---");
                MostrarVehiculo(encontrado);
            }
            else
            {
                Console.WriteLine("\nVehículo no encontrado.");
            }
        }

        // c. Ver vehículos por año
        public void VerVehiculosPorAnio()
        {
            Console.WriteLine("\n=== VEHICULOS POR AÑO ===");
            Console.Write("Ingrese el año: ");
            int anio;
            while (!int.TryParse(Console.ReadLine(), out anio))
            {
                Console.Write("Año inválido. Intente nuevamente: ");
            }

            bool encontrado = false;
            Vehiculo temp = cabeza;

            Console.WriteLine("\n" + new string('-', 80));
            Console.WriteLine("{0,-12} {1,-15} {2,-15} {3,-8} {4,-12}", 
                "PLACA", "MARCA", "MODELO", "AÑO", "PRECIO");
            Console.WriteLine(new string('-', 80));

            while (temp != null)
            {
                if (temp.Anio == anio)
                {
                    Console.WriteLine("{0,-12} {1,-15} {2,-15} {3,-8} ${4:F2}",
                        temp.Placa, temp.Marca, temp.Modelo, temp.Anio, temp.Precio);
                    encontrado = true;
                }
                temp = temp.Siguiente;
            }

            Console.WriteLine(new string('-', 80));

            if (!encontrado)
            {
                Console.WriteLine($"No hay vehículos registrados del año {anio}");
            }
        }

        // d. Ver todos los vehículos
        public void VerTodosLosVehiculos()
        {
            Console.WriteLine("\n=== TODOS LOS VEHICULOS REGISTRADOS ===");

            if (cabeza == null)
            {
                Console.WriteLine("\nNo hay vehículos registrados.");
                return;
            }

            Vehiculo temp = cabeza;
            int contador = 0;

            Console.WriteLine("\n" + new string('-', 80));
            Console.WriteLine("{0,-12} {1,-15} {2,-15} {3,-8} {4,-12}", 
                "PLACA", "MARCA", "MODELO", "AÑO", "PRECIO");
            Console.WriteLine(new string('-', 80));

            while (temp != null)
            {
                Console.WriteLine("{0,-12} {1,-15} {2,-15} {3,-8} ${4:F2}",
                    temp.Placa, temp.Marca, temp.Modelo, temp.Anio, temp.Precio);
                temp = temp.Siguiente;
                contador++;
            }

            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"Total de vehículos: {contador}");
        }

        // e. Eliminar vehículo
        public void EliminarVehiculo()
        {
            Console.WriteLine("\n=== ELIMINAR VEHICULO ===");
            Console.Write("Ingrese la placa del vehículo a eliminar: ");
            string placa = Console.ReadLine();

            if (cabeza == null)
            {
                Console.WriteLine("\nNo hay vehículos registrados.");
                return;
            }

            // Si el vehículo a eliminar es el primero
            if (cabeza.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
            {
                cabeza = cabeza.Siguiente;
                Console.WriteLine("\n¡Vehículo eliminado exitosamente!");
                return;
            }

            // Buscar el vehículo en el resto de la lista
            Vehiculo anterior = cabeza;
            Vehiculo actual = cabeza.Siguiente;

            while (actual != null)
            {
                if (actual.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
                {
                    anterior.Siguiente = actual.Siguiente;
                    Console.WriteLine("\n¡Vehículo eliminado exitosamente!");
                    return;
                }
                anterior = actual;
                actual = actual.Siguiente;
            }

            Console.WriteLine("\nVehículo no encontrado.");
        }

        // Método auxiliar para mostrar un vehículo
        private void MostrarVehiculo(Vehiculo v)
        {
            Console.WriteLine($"Placa:  {v.Placa}");
            Console.WriteLine($"Marca:  {v.Marca}");
            Console.WriteLine($"Modelo: {v.Modelo}");
            Console.WriteLine($"Año:    {v.Anio}");
            Console.WriteLine($"Precio: ${v.Precio:F2}");
        }
    }

    // Clase principal del programa
    class Program
    {
        static void MostrarMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║    SISTEMA DE REGISTRO DE VEHICULOS - UEA              ║");
            Console.WriteLine("║    Área de Ingeniería de Sistemas                      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.WriteLine("\n1. Agregar vehículo");
            Console.WriteLine("2. Buscar vehículo por placa");
            Console.WriteLine("3. Ver vehículos por año");
            Console.WriteLine("4. Ver todos los vehículos");
            Console.WriteLine("5. Eliminar vehículo");
            Console.WriteLine("6. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        static void Main(string[] args)
        {
            ListaVehiculos parking = new ListaVehiculos();
            int opcion;

            do
            {
                MostrarMenu();
                
                while (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.Write("Opción inválida. Intente nuevamente: ");
                }

                switch (opcion)
                {
                    case 1:
                        parking.AgregarVehiculo();
                        break;
                    case 2:
                        parking.BuscarYMostrarVehiculo();
                        break;
                    case 3:
                        parking.VerVehiculosPorAnio();
                        break;
                    case 4:
                        parking.VerTodosLosVehiculos();
                        break;
                    case 5:
                        parking.EliminarVehiculo();
                        break;
                    case 6:
                        Console.WriteLine("\n¡Gracias por usar el sistema!");
                        break;
                    default:
                        Console.WriteLine("\nOpción inválida. Intente nuevamente.");
                        break;
                }

                if (opcion != 6)
                {
                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (opcion != 6);
        }
    }
}