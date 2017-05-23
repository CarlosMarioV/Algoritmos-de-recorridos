using System;

/// <summary>
/// Fecha de inicio: Viernes 13 de Mayo 2016
/// Autores: Esteban Blanco y Carlos Villafuerte
/// Tecnologico de Costa Rica
/// Curso: Analisis de Algortimo
/// Finalizado en: Por definir.
/// </summary>

namespace RompecabezaN2016Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            controlador control = new controlador(3,3);
            control.CrearMatrizLogica(3);
            string opcion;
            while(true)
            {
                
                Console.WriteLine("\n############################# Menu Principal #############################\n");
                Console.WriteLine("---- 1: Selecionar Matriz");
                Console.WriteLine("---- 2: Imprimir matriz");
                Console.WriteLine("---- 3: Voraz");
                Console.WriteLine("---- 4: Backtracking");
                Console.WriteLine("---- 5: Genético");
                Console.Write("___*** : ");
                opcion = Console.ReadLine();
                if(opcion.Equals("0"))
                {
                    break;
                }
                else if(opcion.Equals("1"))
                {
                    control.SeleccionarMatriz();
                }
                else if (opcion.Equals("2"))
                {
                    control.ImprimirMatrizMovimiento();
                }
                else if (opcion.Equals("3"))
                {
                    control.LlamarVoraz();
                }
                else if (opcion.Equals("4"))
                {
                    control.LlamarBacktracking();
                }
                else if (opcion.Equals("5"))
                {
                    control.LlamarGenetico();
                }
            }
        }
    }
}
