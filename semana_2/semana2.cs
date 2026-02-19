using System;

namespace FigurasGeometricas
{
    // Clase que representa un Círculo
    public class Circulo
    {
        // Campo privado para encapsular el radio
        private double radio;

        // Constructor que inicializa el radio del círculo
        public Circulo(double radio)
        {
            // Validación para asegurar que el radio sea positivo
            if (radio <= 0)
            {
                throw new ArgumentException("El radio debe ser mayor que cero.");
            }
            this.radio = radio;
        }

        // Propiedad pública para acceder y modificar el radio
        public double Radio
        {
            get { return radio; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El radio debe ser mayor que cero.");
                }
                radio = value;
            }
        }

        // Método para calcular el área del círculo (π * r²)
        public double CalcularArea()
        {
            return Math.PI * Math.Pow(radio, 2);
        }

        // Método para calcular el perímetro del círculo (2 * π * r)
        public double CalcularPerimetro()
        {
            return 2 * Math.PI * radio;
        }

        // Método para mostrar información del círculo
        public void MostrarInformacion()
        {
            Console.WriteLine($"--- Círculo ---");
            Console.WriteLine($"Radio: {radio:F2}");
            Console.WriteLine($"Área: {CalcularArea():F2}");
            Console.WriteLine($"Perímetro: {CalcularPerimetro():F2}");
        }
    }

    // Clase que representa un Rectángulo
    public class Rectangulo
    {
        // Campos privados para encapsular el largo y el ancho
        private double largo;
        private double ancho;

        // Constructor que inicializa el largo y el ancho del rectángulo
        public Rectangulo(double largo, double ancho)
        {
            // Validación para asegurar que las dimensiones sean positivas
            if (largo <= 0 || ancho <= 0)
            {
                throw new ArgumentException("El largo y el ancho deben ser mayores que cero.");
            }
            this.largo = largo;
            this.ancho = ancho;
        }

        // Propiedad pública para acceder y modificar el largo
        public double Largo
        {
            get { return largo; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El largo debe ser mayor que cero.");
                }
                largo = value;
            }
        }

        // Propiedad pública para acceder y modificar el ancho
        public double Ancho
        {
            get { return ancho; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El ancho debe ser mayor que cero.");
                }
                ancho = value;
            }
        }

        // Método para calcular el área del rectángulo (largo * ancho)
        public double CalcularArea()
        {
            return largo * ancho;
        }

        // Método para calcular el perímetro del rectángulo (2 * (largo + ancho))
        public double CalcularPerimetro()
        {
            return 2 * (largo + ancho);
        }

        // Método para mostrar información del rectángulo
        public void MostrarInformacion()
        {
            Console.WriteLine($"--- Rectángulo ---");
            Console.WriteLine($"Largo: {largo:F2}");
            Console.WriteLine($"Ancho: {ancho:F2}");
            Console.WriteLine($"Área: {CalcularArea():F2}");
            Console.WriteLine($"Perímetro: {CalcularPerimetro():F2}");
        }
    }

    // Clase principal para demostrar el uso de las figuras geométricas
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== CALCULADORA DE FIGURAS GEOMÉTRICAS ===\n");

            // Crear una instancia de Círculo
            Circulo miCirculo = new Circulo(5.0);
            miCirculo.MostrarInformacion();

            Console.WriteLine(); // Línea en blanco para separación

            // Crear una instancia de Rectángulo
            Rectangulo miRectangulo = new Rectangulo(8.0, 4.0);
            miRectangulo.MostrarInformacion();

            Console.WriteLine("\n--- Modificando valores ---\n");

            // Modificar el radio del círculo usando la propiedad
            miCirculo.Radio = 7.5;
            miCirculo.MostrarInformacion();

            Console.WriteLine();

            // Modificar las dimensiones del rectángulo
            miRectangulo.Largo = 10.0;
            miRectangulo.Ancho = 6.0;
            miRectangulo.MostrarInformacion();

            // Mantener la consola abierta
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}