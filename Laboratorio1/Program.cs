using System;

namespace Laboratorio1 {
    class Program {
        static void Main(string[] args) {
            bool exit = false;
            while (!exit) {
                try {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("-------------------------LABORATORIO NO. 1-------------------------");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Ingrese una operación: ");
                    string regexp = Console.ReadLine();
                    Parser parser = new Parser();
                    Console.WriteLine("Resultado: " + parser.Parse(regexp));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Expresión aceptada :D");
                }
                catch (Exception ex) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Expresión no aceptada por " + ex.Message + " :(");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("¿Desea continuar ingresando operaciones? N - Salir / Enter - Continuar");
                if (Console.ReadKey().Key == ConsoleKey.N) {
                    exit = true;
                }
                else {
                    Console.Clear();
                }
            }
            
        }
    }
}
