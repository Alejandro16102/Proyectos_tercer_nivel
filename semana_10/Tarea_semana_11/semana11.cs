using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

class Translator
{
    // Diccionario bidireccional: inglés -> español y español -> inglés
    static Dictionary<string, string> enToEs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    static Dictionary<string, string> esEn = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        InitializeDictionary();

        bool running = true;
        while (running)
        {
            ShowMenu();
            string option = Console.ReadLine()?.Trim();

            switch (option)
            {
                case "1":
                    TranslatePhrase();
                    break;
                case "2":
                    AddWord();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("\n¡Hasta luego! / Goodbye!\n");
                    break;
                default:
                    Console.WriteLine("\n⚠  Opción no válida. Intente de nuevo.\n");
                    break;
            }
        }
    }

    static void InitializeDictionary()
    {
        var words = new List<(string en, string es)>
        {
            ("time",       "tiempo"),
            ("person",     "persona"),
            ("year",       "año"),
            ("way",        "camino"),
            ("day",        "día"),
            ("thing",      "cosa"),
            ("man",        "hombre"),
            ("world",      "mundo"),
            ("life",       "vida"),
            ("hand",       "mano"),
            ("part",       "parte"),
            ("child",      "niño"),
            ("eye",        "ojo"),
            ("woman",      "mujer"),
            ("place",      "lugar"),
            ("work",       "trabajo"),
            ("week",       "semana"),
            ("case",       "caso"),
            ("point",      "punto"),
            ("government", "gobierno"),
            ("company",    "empresa")
        };

        foreach (var (en, es) in words)
        {
            enToEs[en] = es;
            esEn[es]   = en;
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("==================== MENÚ ====================");
        Console.WriteLine("1. Traducir una frase");
        Console.WriteLine("2. Agregar palabras al diccionario");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");
    }

    static void TranslatePhrase()
    {
        Console.WriteLine("\n--- TRADUCCIÓN ---");
        Console.WriteLine("Idiomas disponibles:");
        Console.WriteLine("  [1] Español → Inglés");
        Console.WriteLine("  [2] Inglés  → Español");
        Console.Write("Seleccione dirección: ");
        string dir = Console.ReadLine()?.Trim();

        if (dir != "1" && dir != "2")
        {
            Console.WriteLine("⚠  Dirección no válida.\n");
            return;
        }

        Console.Write("\nIngrese la frase: ");
        string phrase = Console.ReadLine() ?? "";

        string result = Translate(phrase, dir == "1" ? esEn : enToEs);

        Console.WriteLine($"\nFrase original : {phrase}");
        Console.WriteLine($"Traducción     : {result}");
        Console.WriteLine();
    }

    // Traduce una frase usando el diccionario indicado.
    // Conserva puntuación, mayúsculas/minúsculas originales.
    static string Translate(string phrase, Dictionary<string, string> dict)
    {
        // Divide conservando delimitadores (puntuación, espacios)
        string[] tokens = Regex.Split(phrase, @"(\W+)");
        var sb = new StringBuilder();

        foreach (string token in tokens)
        {
            if (string.IsNullOrEmpty(token))
                continue;

            // ¿Es una "palabra" traducible?
            if (Regex.IsMatch(token, @"^\w+$"))
            {
                if (dict.TryGetValue(token, out string translation))
                {
                    // Preservar capitalización
                    translation = MatchCase(token, translation);
                    sb.Append(translation);
                }
                else
                {
                    sb.Append(token); // sin traducción → se deja igual
                }
            }
            else
            {
                sb.Append(token); // puntuación / espacios
            }
        }

        return sb.ToString();
    }

    // Ajusta el case de la traducción para que coincida con el original.
    static string MatchCase(string original, string translation)
    {
        if (string.IsNullOrEmpty(translation)) return translation;

        if (char.IsUpper(original[0]) && original.Length > 1 &&
            original.ToUpper() == original)               // TODO-CAPS
            return translation.ToUpper();

        if (char.IsUpper(original[0]))                    // Primera en mayúscula
            return char.ToUpper(translation[0]) + translation.Substring(1);

        return translation;                               // todo minúscula
    }

    static void AddWord()
    {
        Console.WriteLine("\n--- AGREGAR PALABRA ---");
        Console.Write("Palabra en inglés  : ");
        string en = Console.ReadLine()?.Trim().ToLower() ?? "";

        Console.Write("Palabra en español : ");
        string es = Console.ReadLine()?.Trim().ToLower() ?? "";

        if (string.IsNullOrWhiteSpace(en) || string.IsNullOrWhiteSpace(es))
        {
            Console.WriteLine("⚠  No se pueden agregar palabras vacías.\n");
            return;
        }

        bool updatedEn = enToEs.ContainsKey(en);
        bool updatedEs = esEn.ContainsKey(es);

        enToEs[en] = es;
        esEn[es]   = en;

        if (updatedEn || updatedEs)
            Console.WriteLine($"✔  Palabra actualizada: \"{en}\" ↔ \"{es}\"\n");
        else
            Console.WriteLine($"✔  Palabra agregada: \"{en}\" ↔ \"{es}\"\n");
    }
}