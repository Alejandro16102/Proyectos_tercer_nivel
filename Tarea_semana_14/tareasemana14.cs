using System;
using System.Collections.Generic;
using System.Text;

namespace BSTApp
{
    // ─────────────────────────────────────────────
    //  NODO del árbol
    // ─────────────────────────────────────────────
    class Nodo
    {
        public int Valor { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    // ─────────────────────────────────────────────
    //  ÁRBOL BINARIO DE BÚSQUEDA
    // ─────────────────────────────────────────────
    class ArbolBST
    {
        private Nodo raiz;

        public ArbolBST() => raiz = null;

        // ── Insertar ────────────────────────────
        public void Insertar(int valor)
        {
            raiz = InsertarRec(raiz, valor);
        }

        private Nodo InsertarRec(Nodo nodo, int valor)
        {
            if (nodo == null) return new Nodo(valor);

            if (valor < nodo.Valor)
                nodo.Izquierdo = InsertarRec(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = InsertarRec(nodo.Derecho, valor);
            else
                Console.WriteLine($"  ⚠  El valor {valor} ya existe en el árbol.");

            return nodo;
        }

        // ── Buscar ──────────────────────────────
        public bool Buscar(int valor) => BuscarRec(raiz, valor);

        private bool BuscarRec(Nodo nodo, int valor)
        {
            if (nodo == null) return false;
            if (valor == nodo.Valor) return true;
            return valor < nodo.Valor
                ? BuscarRec(nodo.Izquierdo, valor)
                : BuscarRec(nodo.Derecho, valor);
        }

        // ── Eliminar ────────────────────────────
        public void Eliminar(int valor)
        {
            if (!Buscar(valor))
            {
                Console.WriteLine($"  ✗  El valor {valor} no existe en el árbol.");
                return;
            }
            raiz = EliminarRec(raiz, valor);
            Console.WriteLine($"  ✓  Valor {valor} eliminado correctamente.");
        }

        private Nodo EliminarRec(Nodo nodo, int valor)
        {
            if (nodo == null) return null;

            if (valor < nodo.Valor)
                nodo.Izquierdo = EliminarRec(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = EliminarRec(nodo.Derecho, valor);
            else
            {
                // Caso 1 y 2: sin hijos o con un solo hijo
                if (nodo.Izquierdo == null) return nodo.Derecho;
                if (nodo.Derecho == null)   return nodo.Izquierdo;

                // Caso 3: dos hijos → sucesor inorden (mínimo del subárbol derecho)
                int sucesor = ObtenerMinimo(nodo.Derecho);
                nodo.Valor  = sucesor;
                nodo.Derecho = EliminarRec(nodo.Derecho, sucesor);
            }
            return nodo;
        }

        // ── Recorridos ──────────────────────────
        public void Preorden()
        {
            var resultado = new List<int>();
            PreordenRec(raiz, resultado);
            ImprimirLista("Preorden  (Raíz → Izq → Der)", resultado);
        }

        private void PreordenRec(Nodo nodo, List<int> lista)
        {
            if (nodo == null) return;
            lista.Add(nodo.Valor);
            PreordenRec(nodo.Izquierdo, lista);
            PreordenRec(nodo.Derecho, lista);
        }

        public void Inorden()
        {
            var resultado = new List<int>();
            InordenRec(raiz, resultado);
            ImprimirLista("Inorden   (Izq → Raíz → Der)", resultado);
        }

        private void InordenRec(Nodo nodo, List<int> lista)
        {
            if (nodo == null) return;
            InordenRec(nodo.Izquierdo, lista);
            lista.Add(nodo.Valor);
            InordenRec(nodo.Derecho, lista);
        }

        public void Postorden()
        {
            var resultado = new List<int>();
            PostordenRec(raiz, resultado);
            ImprimirLista("Postorden (Izq → Der → Raíz)", resultado);
        }

        private void PostordenRec(Nodo nodo, List<int> lista)
        {
            if (nodo == null) return;
            PostordenRec(nodo.Izquierdo, lista);
            PostordenRec(nodo.Derecho, lista);
            lista.Add(nodo.Valor);
        }

        private void ImprimirLista(string titulo, List<int> lista)
        {
            Console.Write($"  {titulo}: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(lista.Count > 0 ? string.Join(" → ", lista) : "(vacío)");
            Console.ResetColor();
        }

        // ── Mínimo y Máximo ─────────────────────
        public void MostrarMinimo()
        {
            if (raiz == null) { Console.WriteLine("  ✗  El árbol está vacío."); return; }
            Console.Write("  Valor mínimo: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(ObtenerMinimo(raiz));
            Console.ResetColor();
        }

        private int ObtenerMinimo(Nodo nodo)
        {
            while (nodo.Izquierdo != null) nodo = nodo.Izquierdo;
            return nodo.Valor;
        }

        public void MostrarMaximo()
        {
            if (raiz == null) { Console.WriteLine("  ✗  El árbol está vacío."); return; }
            Nodo nodo = raiz;
            while (nodo.Derecho != null) nodo = nodo.Derecho;
            Console.Write("  Valor máximo: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(nodo.Valor);
            Console.ResetColor();
        }

        // ── Altura ──────────────────────────────
        public void MostrarAltura()
        {
            int h = Altura(raiz);
            Console.Write($"  Altura del árbol: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(h == -1 ? "El árbol está vacío (altura = -1)" : h.ToString());
            Console.ResetColor();
        }

        private int Altura(Nodo nodo)
        {
            if (nodo == null) return -1;
            return 1 + Math.Max(Altura(nodo.Izquierdo), Altura(nodo.Derecho));
        }

        // ── Limpiar ─────────────────────────────
        public void Limpiar()
        {
            raiz = null;
            Console.WriteLine("  ✓  Árbol limpiado completamente.");
        }

        // ── Visualización en consola ─────────────
        public void Visualizar()
        {
            if (raiz == null)
            {
                Console.WriteLine("  (El árbol está vacío)");
                return;
            }
            Console.WriteLine();
            ImprimirArbol(raiz, "", false);
            Console.WriteLine();
        }

        private void ImprimirArbol(Nodo nodo, string prefijo, bool esIzquierdo)
        {
            if (nodo == null) return;

            Console.Write(prefijo);
            Console.Write(esIzquierdo ? "├── " : "└── ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(nodo.Valor);
            Console.ResetColor();

            string nuevoPrefijo = prefijo + (esIzquierdo ? "│   " : "    ");
            bool tieneAmbos = nodo.Izquierdo != null && nodo.Derecho != null;

            if (nodo.Izquierdo != null)
                ImprimirArbol(nodo.Izquierdo, nuevoPrefijo, nodo.Derecho != null);
            if (nodo.Derecho != null)
                ImprimirArbol(nodo.Derecho, nuevoPrefijo, false);
        }

        public bool EstaVacio() => raiz == null;
    }

    // ─────────────────────────────────────────────
    //  MENÚ PRINCIPAL
    // ─────────────────────────────────────────────
    class Program
    {
        static ArbolBST arbol = new ArbolBST();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool salir = false;

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1": OpcionInsertar();     break;
                    case "2": OpcionBuscar();       break;
                    case "3": OpcionEliminar();     break;
                    case "4": OpcionRecorridos();   break;
                    case "5": OpcionEstadisticas(); break;
                    case "6": OpcionVisualizar();   break;
                    case "7": arbol.Limpiar();      break;
                    case "0":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("  ¡Hasta luego!");
                        Console.ResetColor();
                        salir = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  ✗  Opción no válida. Intente de nuevo.");
                        Console.ResetColor();
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine();
                    Console.Write("  Presione ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }

        // ── Menú visual ─────────────────────────
        static void MostrarMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║    ÁRBOL BINARIO DE BÚSQUEDA (BST)       ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.ResetColor();
            Console.WriteLine("║  1. Insertar valor                       ║");
            Console.WriteLine("║  2. Buscar valor                         ║");
            Console.WriteLine("║  3. Eliminar valor                       ║");
            Console.WriteLine("║  4. Mostrar recorridos                   ║");
            Console.WriteLine("║  5. Mínimo / Máximo / Altura             ║");
            Console.WriteLine("║  6. Visualizar árbol                     ║");
            Console.WriteLine("║  7. Limpiar árbol                        ║");
            Console.WriteLine("║  0. Salir                                ║");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("  Seleccione una opción: ");
        }

        // ── Operaciones ─────────────────────────
        static void OpcionInsertar()
        {
            Console.Write("  Ingrese el valor a insertar: ");
            if (int.TryParse(Console.ReadLine(), out int v))
            {
                arbol.Insertar(v);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  ✓  Valor {v} insertado.");
                Console.ResetColor();
            }
            else Console.WriteLine("  ✗  Entrada no válida.");
        }

        static void OpcionBuscar()
        {
            Console.Write("  Ingrese el valor a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int v))
            {
                bool encontrado = arbol.Buscar(v);
                Console.ForegroundColor = encontrado ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(encontrado
                    ? $"  ✓  El valor {v} SÍ existe en el árbol."
                    : $"  ✗  El valor {v} NO existe en el árbol.");
                Console.ResetColor();
            }
            else Console.WriteLine("  ✗  Entrada no válida.");
        }

        static void OpcionEliminar()
        {
            Console.Write("  Ingrese el valor a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int v))
                arbol.Eliminar(v);
            else
                Console.WriteLine("  ✗  Entrada no válida.");
        }

        static void OpcionRecorridos()
        {
            if (arbol.EstaVacio()) { Console.WriteLine("  ✗  El árbol está vacío."); return; }
            Console.WriteLine("  ── Recorridos del árbol ──────────────────");
            arbol.Preorden();
            arbol.Inorden();
            arbol.Postorden();
        }

        static void OpcionEstadisticas()
        {
            Console.WriteLine("  ── Estadísticas ──────────────────────────");
            arbol.MostrarMinimo();
            arbol.MostrarMaximo();
            arbol.MostrarAltura();
        }

        static void OpcionVisualizar()
        {
            Console.WriteLine("  ── Estructura del árbol ──────────────────");
            arbol.Visualizar();
        }
    }
}