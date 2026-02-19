using System;
using System.Collections.Generic;
using System.Linq;

namespace VacunacionCovid19
{
    // Clase que representa a un ciudadano
    class Ciudadano
    {
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Ciudad { get; set; }

        public Ciudadano(int cedula, string nombre, int edad, string ciudad)
        {
            Cedula = cedula;
            Nombre = nombre;
            Edad = edad;
            Ciudad = ciudad;
        }

        public override string ToString()
        {
            return $"  Cédula: {Cedula,10} | Nombre: {Nombre,-30} | Edad: {Edad,3} | Ciudad: {Ciudad}";
        }

        // Sobrescribir Equals y GetHashCode para que HashSet los compare por Cedula
        public override bool Equals(object obj)
        {
            if (obj is Ciudadano otro)
                return Cedula == otro.Cedula;
            return false;
        }

        public override int GetHashCode() => Cedula.GetHashCode();
    }

    class Program
    {
        // Datos de ejemplo para generación aleatoria
        static readonly string[] Nombres = {
            "Santiago", "Valentina", "Sebastián", "Camila", "Mateo", "Sofía", "Nicolás", "Isabella",
            "Andrés", "Gabriela", "Daniel", "Laura", "Felipe", "Mariana", "Alejandro", "Daniela",
            "Juan", "Natalia", "Carlos", "Paola", "Diego", "Andrea", "Luis", "Carolina",
            "Miguel", "Alejandra", "Jorge", "Juliana", "Ricardo", "Melissa", "Roberto", "Viviana",
            "Cristian", "Mónica", "Eduardo", "Patricia", "Fernando", "Elena", "Gonzalo", "Diana"
        };

        static readonly string[] Apellidos = {
            "García", "Rodríguez", "López", "Martínez", "González", "Pérez", "Sánchez", "Ramírez",
            "Torres", "Flores", "Rivera", "Gómez", "Díaz", "Morales", "Muñoz", "Jiménez",
            "Vargas", "Castro", "Ortega", "Reyes", "Mendoza", "Ramos", "Herrera", "Cruz",
            "Guerrero", "Salinas", "Medina", "Suárez", "Vásquez", "Rojas", "Paredes", "Cabrera",
            "Mora", "Espinoza", "Arias", "Andrade", "Villacís", "Naranjo", "Benítez", "Alvarado"
        };

        static readonly string[] Ciudades = {
            "Quito", "Guayaquil", "Cuenca", "Ambato", "Manta", "Portoviejo", "Loja",
            "Riobamba", "Ibarra", "Esmeraldas", "Santo Domingo", "Machala", "Latacunga"
        };

        static Random rnd = new Random(42); // Semilla fija para reproducibilidad

        // Genera un ciudadano con datos ficticios únicos
        static Ciudadano GenerarCiudadano(int cedula)
        {
            string nombre = Nombres[rnd.Next(Nombres.Length)] + " " + Apellidos[rnd.Next(Apellidos.Length)];
            int edad = rnd.Next(18, 81);
            string ciudad = Ciudades[rnd.Next(Ciudades.Length)];
            return new Ciudadano(cedula, nombre, edad, ciudad);
        }

        // Imprime los ciudadanos de un conjunto con encabezado
        static void ImprimirConjunto(string titulo, HashSet<Ciudadano> conjunto)
        {
            string linea = new string('═', 85);
            Console.WriteLine($"\n╔{linea}╗");
            Console.WriteLine($"║  {titulo,-83}║");
            Console.WriteLine($"║  Total: {conjunto.Count} ciudadanos{new string(' ', 73 - conjunto.Count.ToString().Length)}║");
            Console.WriteLine($"╠{linea}╣");

            if (conjunto.Count == 0)
            {
                Console.WriteLine($"║  (Sin registros){new string(' ', 68)}║");
            }
            else
            {
                int contador = 1;
                foreach (var c in conjunto.OrderBy(x => x.Cedula))
                {
                    string linea_c = $"  {contador++,3}. {c}";
                    // Truncar si es demasiado larga para la caja
                    if (linea_c.Length > 84) linea_c = linea_c.Substring(0, 84);
                    Console.WriteLine($"║{linea_c.PadRight(84)}║");
                }
            }

            Console.WriteLine($"╚{linea}╝");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        MINISTERIO DE SALUD — CAMPAÑA DE VACUNACIÓN COVID-19                         ║");
            Console.WriteLine("║        Sistema de Gestión de Conjuntos — Datos Ficticios                            ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════╝");

            // ─── PASO 1: Generar el universo de 500 ciudadanos ───────────────────────────
            Console.WriteLine("\n[1/4] Generando universo de ciudadanos...");

            // Las cédulas van de 1000000001 a 1000000500
            HashSet<Ciudadano> U = new HashSet<Ciudadano>(); // Universo
            for (int i = 1; i <= 500; i++)
            {
                U.Add(GenerarCiudadano(1000000000 + i));
            }
            Console.WriteLine($"    ✔ Universo U generado: {U.Count} ciudadanos.");

            // ─── PASO 2: Seleccionar 75 vacunados con Pfizer (subconjunto del universo) ──
            Console.WriteLine("[2/4] Seleccionando vacunados con Pfizer...");

            // Se toman los primeros 75 ciudadanos del universo (orden por cédula)
            HashSet<Ciudadano> P = new HashSet<Ciudadano>(
                U.OrderBy(c => c.Cedula).Take(75)
            );
            Console.WriteLine($"    ✔ Conjunto P (Pfizer) generado: {P.Count} ciudadanos.");

            // ─── PASO 3: Seleccionar 75 vacunados con AstraZeneca ────────────────────────
            // Con solapamiento intencional: 25 ciudadanos reciben ambas dosis
            Console.WriteLine("[3/4] Seleccionando vacunados con AstraZeneca...");

            // Los últimos 50 de Pfizer (cédulas 26–75) + 50 nuevos ciudadanos (cédulas 76–125)
            // Así: 50 tienen ambas dosis, 25 solo Pfizer, 50 solo AstraZeneca
            HashSet<Ciudadano> A = new HashSet<Ciudadano>(
                U.OrderBy(c => c.Cedula).Skip(25).Take(75)
            );
            Console.WriteLine($"    ✔ Conjunto A (AstraZeneca) generado: {A.Count} ciudadanos.");

            // ─── PASO 4: Operaciones de Teoría de Conjuntos ──────────────────────────────
            Console.WriteLine("[4/4] Aplicando operaciones de teoría de conjuntos...\n");

            // Todos los vacunados = P ∪ A  (Unión)
            HashSet<Ciudadano> vacunados = new HashSet<Ciudadano>(P);
            vacunados.UnionWith(A);

            // 1. No vacunados = U \ (P ∪ A)  (Diferencia: Universo menos vacunados)
            HashSet<Ciudadano> noVacunados = new HashSet<Ciudadano>(U);
            noVacunados.ExceptWith(vacunados);

            // 2. Ambas dosis = P ∩ A  (Intersección)
            HashSet<Ciudadano> ambasDosis = new HashSet<Ciudadano>(P);
            ambasDosis.IntersectWith(A);

            // 3. Solo Pfizer = P \ A  (Diferencia: Pfizer menos AstraZeneca)
            HashSet<Ciudadano> soloPfizer = new HashSet<Ciudadano>(P);
            soloPfizer.ExceptWith(A);

            // 4. Solo AstraZeneca = A \ P  (Diferencia: AstraZeneca menos Pfizer)
            HashSet<Ciudadano> soloAstra = new HashSet<Ciudadano>(A);
            soloAstra.ExceptWith(P);

            // ─── Verificación de integridad ──────────────────────────────────────────────
            Console.WriteLine("  Verificación de integridad (|U| debe ser 500):");
            int suma = noVacunados.Count + ambasDosis.Count + soloPfizer.Count + soloAstra.Count;
            Console.WriteLine($"    No vacunados ({noVacunados.Count}) + Ambas dosis ({ambasDosis.Count}) " +
                              $"+ Solo Pfizer ({soloPfizer.Count}) + Solo AstraZeneca ({soloAstra.Count}) = {suma}");
            Console.WriteLine(suma == 500 ? "    ✔ Verificación OK: partición exacta del universo.\n"
                                          : "    ✗ Error en la partición.\n");

            // ─── Imprimir resultados ─────────────────────────────────────────────────────
            ImprimirConjunto(
                "LISTADO 1 — Ciudadanos NO VACUNADOS  [ U \\ (P ∪ A) ]",
                noVacunados);

            ImprimirConjunto(
                "LISTADO 2 — Ciudadanos con AMBAS DOSIS  [ P ∩ A ]",
                ambasDosis);

            ImprimirConjunto(
                "LISTADO 3 — Ciudadanos SOLO PFIZER  [ P \\ A ]",
                soloPfizer);

            ImprimirConjunto(
                "LISTADO 4 — Ciudadanos SOLO ASTRAZENECA  [ A \\ P ]",
                soloAstra);

            // ─── Resumen estadístico ─────────────────────────────────────────────────────
            Console.WriteLine("\n╔═══════════════════════════════════════════╗");
            Console.WriteLine("║           RESUMEN ESTADÍSTICO             ║");
            Console.WriteLine("╠═══════════════════════════════════════════╣");
            Console.WriteLine($"║  Total ciudadanos (|U|)        : {U.Count,5}     ║");
            Console.WriteLine($"║  Vacunados con Pfizer (|P|)    : {P.Count,5}     ║");
            Console.WriteLine($"║  Vacunados con AstraZeneca (|A|): {A.Count,4}     ║");
            Console.WriteLine($"║  Total vacunados (|P ∪ A|)     : {vacunados.Count,5}     ║");
            Console.WriteLine("╠═══════════════════════════════════════════╣");
            Console.WriteLine($"║  1. No vacunados               : {noVacunados.Count,5}     ║");
            Console.WriteLine($"║  2. Con ambas dosis (P ∩ A)    : {ambasDosis.Count,5}     ║");
            Console.WriteLine($"║  3. Solo Pfizer (P \\ A)        : {soloPfizer.Count,5}     ║");
            Console.WriteLine($"║  4. Solo AstraZeneca (A \\ P)   : {soloAstra.Count,5}     ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}