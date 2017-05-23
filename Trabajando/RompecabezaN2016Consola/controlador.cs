using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RompecabezaN2016Consola
{
    class controlador
    {
        private int[,] tablero;
        int fila, columna;

        /// <summary>
        /// El contructor del controlador principal...
        /// </summary>
        /// <param name="fila">La cantidad de fila</param>
        /// <param name="columna">La cantidad de columnas</param>
        public controlador(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            tablero = new int[this.fila, this.columna];
        }

        public void SeleccionarMatriz()
        {
            Console.WriteLine("\t\t\t_____ 1: 3x3");
            Console.WriteLine("\t\t\t_____ 2: 4x4");
            Console.WriteLine("\t\t\t_____ 3: 5x5");
            Console.WriteLine("\t\t\t_____ 4: 6x6");
            Console.Write("___*** : ");
            string opcion = Console.ReadLine();
            if (opcion.Equals("1"))
            {
                this.fila = 3;
                this.columna = 3;
                tablero = new int[this.fila, this.columna];
                CrearMatrizLogica(3);
            }
            else if (opcion.Equals("2"))
            {
                this.fila = 4;
                this.columna = 4;
                tablero = new int[this.fila, this.columna];
                CrearMatrizLogica(4);
            }
            else if (opcion.Equals("3"))
            {
                this.fila = 5;
                this.columna = 5;
                tablero = new int[this.fila, this.columna];
                CrearMatrizLogica(5);
            }
            else if (opcion.Equals("4"))
            {
                this.fila = 6;
                this.columna = 6;
                tablero = new int[this.fila, this.columna];
                CrearMatrizLogica(6);
            }
        }
        /// <summary>
        /// Construye la matriz con los tamaños establecidos.
        /// </summary>
        public void CrearMatrizLogica(int selecionada)
        {
            if(selecionada == 3)
            {
                //Se resuelve en 6 movimientos.
                tablero[0, 0] = 2;
                tablero[0, 1] = 3;
                tablero[0, 2] = 0;
                tablero[1, 0] = 1;
                tablero[1, 1] = 4;
                tablero[1, 2] = 6;
                tablero[2, 0] = 7;
                tablero[2, 1] = 5;
                tablero[2, 2] = 8;
                /*  
                //Se resuelve en 8 movimientos.         
                tablero[0, 0] = 2;
                tablero[0, 1] = 4;
                tablero[0, 2] = 0;
                tablero[1, 0] = 1;
                tablero[1, 1] = 6;
                tablero[1, 2] = 3;
                tablero[2, 0] = 7;
                tablero[2, 1] = 5;
                tablero[2, 2] = 8;
                */
            }
            else if(selecionada == 4)
            {
                //Se resuleve en 8 movimientos
                tablero[0, 0] = 1;
                tablero[0, 1] = 7;
                tablero[0, 2] = 2;
                tablero[0, 3] = 4;
                tablero[1, 0] = 5;
                tablero[1, 1] = 0;
                tablero[1, 2] = 3;
                tablero[1, 3] = 8;
                tablero[2, 0] = 9;
                tablero[2, 1] = 6;
                tablero[2, 2] = 11;
                tablero[2, 3] = 12;
                tablero[3, 0] = 13;
                tablero[3, 1] = 10;
                tablero[3, 2] = 14;
                tablero[3, 3] = 15;

            }
            else if(selecionada == 5)
            {
                //Se resuelve en 10 movimientos.
                tablero[0, 0] = 0;
                tablero[0, 1] = 1;
                tablero[0, 2] = 2;
                tablero[0, 3] = 4;
                tablero[0, 4] = 5;
                tablero[1, 0] = 6;
                tablero[1, 1] = 8;
                tablero[1, 2] = 3;
                tablero[1, 3] = 9;
                tablero[1, 4] = 10;
                tablero[2, 0] = 11;
                tablero[2, 1] = 7;
                tablero[2, 2] = 13;
                tablero[2, 3] = 14;
                tablero[2, 4] = 15;
                tablero[3, 0] = 16;
                tablero[3, 1] = 12;
                tablero[3, 2] = 18;
                tablero[3, 3] = 19;
                tablero[3, 4] = 20;
                tablero[4, 0] = 21;
                tablero[4, 1] = 17;
                tablero[4, 2] = 22;
                tablero[4, 3] = 23;
                tablero[4, 4] = 24;
                /*
                //Se resuleve en 8 movimientos.
                tablero[0, 0] = 1;
                tablero[0, 1] = 2;
                tablero[0, 2] = 0;
                tablero[0, 3] = 4;
                tablero[0, 4] = 5;
                tablero[1, 0] = 6;
                tablero[1, 1] = 8;
                tablero[1, 2] = 3;
                tablero[1, 3] = 9;
                tablero[1, 4] = 10;
                tablero[2, 0] = 11;
                tablero[2, 1] = 7;
                tablero[2, 2] = 13;
                tablero[2, 3] = 14;
                tablero[2, 4] = 15;
                tablero[3, 0] = 16;
                tablero[3, 1] = 12;
                tablero[3, 2] = 18;
                tablero[3, 3] = 19;
                tablero[3, 4] = 20;
                tablero[4, 0] = 21;
                tablero[4, 1] = 17;
                tablero[4, 2] = 22;
                tablero[4, 3] = 23;
                tablero[4, 4] = 24;
                */
            }
            else
            {
                //Se resuleve en 12 movimientos
                tablero[0, 0] = 1;
                tablero[0, 1] = 8;
                tablero[0, 2] = 2;
                tablero[0, 3] = 4;
                tablero[0, 4] = 5;
                tablero[0, 5] = 6;
                tablero[1, 0] = 7;
                tablero[1, 1] = 0;
                tablero[1, 2] = 3;
                tablero[1, 3] = 9;
                tablero[1, 4] = 11;
                tablero[1, 5] = 12;
                tablero[2, 0] = 13;
                tablero[2, 1] = 14;
                tablero[2, 2] = 16;
                tablero[2, 3] = 10;
                tablero[2, 4] = 17;
                tablero[2, 5] = 18;
                tablero[3, 0] = 19;
                tablero[3, 1] = 20;
                tablero[3, 2] = 15;
                tablero[3, 3] = 21;
                tablero[3, 4] = 23;
                tablero[3, 5] = 24;
                tablero[4, 0] = 25;
                tablero[4, 1] = 26;
                tablero[4, 2] = 27;
                tablero[4, 3] = 22;
                tablero[4, 4] = 28;
                tablero[4, 5] = 30;
                tablero[5, 0] = 31;
                tablero[5, 1] = 32;
                tablero[5, 2] = 33;
                tablero[5, 3] = 34;
                tablero[5, 4] = 29;
                tablero[5, 5] = 35;
                /*
                //Se resuleve en 8 movimientos.
                tablero[0, 0] = 1;
                tablero[0, 1] = 2;
                tablero[0, 2] = 3;
                tablero[0, 3] = 4;
                tablero[0, 4] = 5;
                tablero[0, 5] = 6;
                tablero[1, 0] = 7;
                tablero[1, 1] = 8;
                tablero[1, 2] = 9;
                tablero[1, 3] = 0;
                tablero[1, 4] = 11;
                tablero[1, 5] = 12;
                tablero[2, 0] = 13;
                tablero[2, 1] = 14;
                tablero[2, 2] = 16;
                tablero[2, 3] = 10;
                tablero[2, 4] = 17;
                tablero[2, 5] = 18;
                tablero[3, 0] = 19;
                tablero[3, 1] = 20;
                tablero[3, 2] = 15;
                tablero[3, 3] = 21;
                tablero[3, 4] = 23;
                tablero[3, 5] = 24;
                tablero[4, 0] = 25;
                tablero[4, 1] = 26;
                tablero[4, 2] = 27;
                tablero[4, 3] = 22;
                tablero[4, 4] = 28;
                tablero[4, 5] = 30;
                tablero[5, 0] = 31;
                tablero[5, 1] = 32;
                tablero[5, 2] = 33;
                tablero[5, 3] = 34;
                tablero[5, 4] = 29;
                tablero[5, 5] = 35;
                */
            }              
        }

        public void LlamarVoraz()
        {
            int[,] nueva = new int[fila, columna];
            Array.Copy(tablero, nueva, fila * fila);
            Algoritmo_Voraz algVor = new Algoritmo_Voraz();
            algVor.Inicio(fila, nueva);
            return;
        }

        public void LlamarGenetico()
        {
            int[,] nueva = new int[fila, columna];
            Array.Copy(tablero, nueva, fila * fila);
            AlgoritmoGenetico genetico = new AlgoritmoGenetico(nueva, fila);
            genetico.inicio();
            return;
        }

        public void LlamarBacktracking()
        {
            int[,] nueva = new int[fila, columna];
            Array.Copy(tablero, nueva, fila * fila);
            Algoritmo_Backtracking back = new Algoritmo_Backtracking(fila,nueva);
            return;
        }


        public void ImprimirMatrizMovimiento()
        {
            Console.WriteLine("\n\t*** Se esta Imprimendo el tablero Raiz");
            for (int i = 0; i < fila; i++)
            {
                for (int j = 0; j < columna; j++)
                {
                    int numero = tablero[i, j];
                    if (numero < 10)
                    {
                        Console.Write("{0}", numero + " | ");
                    }
                    else
                    {
                        Console.Write("{0}", numero + "| ");
                    }
                }
                Console.WriteLine();
            }
        }



        /*
     /// <summary>
     /// Metodo que mueve las fichas por la matriz logica, este se llama en el evento del boton precionado.
     /// </summary>
     /// <param name="y"></param>
     /// <param name="x"></param>
     public void moverFicha(int y, int x)
     {
         int temp;
         try
         {
             if (tablero[y, x - 1] == 0)
             {
                 temp = tablero[y, x - 1];
                 tablero[y, x - 1] = tablero[y, x];
                 tablero[y, x] = temp;
                 ImprimirMatrizMovimiento();
             }
         }
         catch (Exception e)
         {
             Console.WriteLine("Esto paso| :\t" + e.Message);
         }
         try
         {
             if (tablero[y, x + 1] == 0)
             {
                 temp = tablero[y, x + 1];
                 tablero[y, x + 1] = tablero[y, x];
                 tablero[y, x] = temp;
                 ImprimirMatrizMovimiento();
             }
         }
         catch (Exception e)
         {

             Console.WriteLine("Esto paso| :\t" + e.Message);
         }
         try
         {
             if (tablero[y + 1, x] == 0)
             {
                 temp = tablero[y + 1, x];
                 tablero[y + 1, x] = tablero[y, x];
                 tablero[y, x] = temp;
                 ImprimirMatrizMovimiento();
             }

         }
         catch (Exception e)
         {

             Console.WriteLine("Esto paso| :\t" + e.Message);
         }

         try
         {
             if (tablero[y - 1, x] == 0)
             {
                 temp = tablero[y - 1, x];
                 tablero[y - 1, x] = tablero[y, x];
                 tablero[y, x] = temp;
                 ImprimirMatrizMovimiento();
             }

         }
         catch (Exception e)
         {
             Console.WriteLine("Esto paso| :\t" + e.Message);
         }

     }

     public void moverVacio(int y, int x)
     {
         // (1: Izq),(2: Der),(3: Arri),(4: Aba) 
         int dirección;
         Random rnd = new Random();

         dirección = rnd.Next(1, 5);
         Console.WriteLine(y + "  " + x);

         int temp;
         try
         {
             if (dirección == 1)
             {
                 temp = tablero[y, x - 1];
                 tablero[y, x - 1] = tablero[y, x];
                 tablero[y, x] = temp;
                 ImprimirMatrizMovimiento();
             }
         }
         catch (Exception e)
         {
             Console.WriteLine("Esto paso| :\t" + e.Message);
         }
         try
         {
             if (dirección == 2)
             {
                 temp = tablero[y, x + 1];
                 tablero[y, x + 1] = tablero[y, x];
                 tablero[y, x] = temp;
                 ImprimirMatrizMovimiento();
             }
         }
         catch (Exception e)
         {

             Console.WriteLine("Esto paso| :\t" + e.Message);
         }
         try
         {
             if (dirección == 4)
             {
                 temp = tablero[y + 1, x];
                 tablero[y + 1, x] = tablero[y, x];
                 tablero[y, x] = temp;
                 ImprimirMatrizMovimiento();
             }

         }
         catch (Exception e)
         {

             Console.WriteLine("Esto paso| :\t" + e.Message);
         }

         try
         {
             if (dirección == 3)
             {
                 temp = tablero[y - 1, x];
                 tablero[y - 1, x] = tablero[y, x];
                 tablero[y, x] = temp;
                 ImprimirMatrizMovimiento();
             }

         }
         catch (Exception e)
         {
             Console.WriteLine("Esto paso| :\t" + e.Message);
         }
     }


     /// <summary>
     /// Este llama a la ventana y le mete los botones de la matriz con sus posiciones.
     /// </summary>
     public void MeterComponentesEnVentana()
     {
         for (int i = 0; i < fila; i++)
         {
             for (int j = 0; j < columna; j++)
             {
                 Console.Write(tablero[i, j]);
             }
             Console.WriteLine();
         }
     }
        /// <summary>
        /// Imprime la matriz cada ves se mueve un objeto en ella
        /// </summary>

        /*
        /// <summary>
        /// No esta en funcionamiento, es para resetear el tamaño de la matriz.
        /// </summary>
        /// <param name="tam"></param>
        public void setTamañoNuevo(int tam)
        {
            this.fila = tam;
            this.columna = tam;
            tablero = new int[this.fila, this.columna];
        }
        */
    }
}
