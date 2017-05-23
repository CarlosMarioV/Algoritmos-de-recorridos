using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RompecabezaN2016Consola
{

    class Algoritmo_Backtracking
    {
        /// <summary>
        /// Arreglos utilizados.
        /// </summary>
        private int[,] tableroObjetivo;
        LinkedList<int[,]> listaMovimientos = new LinkedList<int[,]>(); // Lista que tiene las movimientos de la matriz
        LinkedList<int[,]> Solucion = new LinkedList<int[,]>(); // Contien las soluciones.
        /// <summary>
        /// Contadores Globales
        /// </summary>
        private int cuentaResultados = 0;
        private int profMaxima = 0;
        private int mejorProfundidad;
        private int totalMovimientos = 0;
        private int nivel = 0;
        private double espacioMemoria = 0;
        private int cantidadNodos = 0;
        private int profundidad = 0;
        private int tamano;
        private int dimension;
        private int j;
        public Algoritmo_Backtracking(int tam, int[,] tablero)
        {
            tamano = tam;
            dimension = (tam * tam);
            int[,] papa;
            papa = new int[tam,tam];
            espacioMemoria += (tam * 32);
            profMaxima = 25; //Profundidad. 25
            int[] arregloTablero = new int[dimension];
            espacioMemoria += dimension * 32; //arregloTablero
            int cont = 0;
            espacioMemoria += 64;  //i y cont
            for (int i = 0; i < tam; i++)
            {
                for (j = 0; j < tam; j++)
                {
                    arregloTablero[cont] = tablero[i, j];
                    cont++;
                }
            }
            GenerarObjetivo();
            prepararArreglo(tam, tam, tablero, arregloTablero, papa);
        }

        private void GenerarObjetivo()
        {
            tableroObjetivo = new int[tamano, tamano];
            int cont = 1;
            espacioMemoria += 32; //cont
            espacioMemoria += 32; // i
            for (int i = 0; i < tamano; i++)
            {
                for (j = 0; j < tamano; j++)
                {
                    tableroObjetivo[i, j] = cont;
                    cont++;
                }
            }
            tableroObjetivo[tamano - 1, tamano - 1] = 0;
        }

        /// <summary>
        /// Prepara los arreglos para ser utilizados en el algoritmo.
        /// </summary>
        /// <param name="filas"></param>
        /// <param name="columnas"></param>
        /// <param name="tablero"></param>
        /// <param name="arregloTablero"></param>
        /// <param name="padre"></param>
        private void prepararArreglo(int filas, int columnas, int[,] tablero, int[] arregloTablero, int[,] padre)
        {
            mejorProfundidad = profMaxima;
            espacioMemoria += 32; //Se suma la memoria apartada para movimientos y mejoresMovimientos
            for (int i = 0; i < tamano; i++)
            {
                for (j = 0; j < tamano; j++)
                {
                    padre[i,j] = 1;
                }
            }
            ObtenerProfundidad(tablero, padre, arregloTablero);
        }

        private void ObtenerProfundidad(int[,] tablero, int[,] tableroPadre, int[] arreglotablero)
        {
            Console.WriteLine("____ Nivel de referencia: si o no");
            Console.Write("___*** : ");
            String w = Console.ReadLine();
            if (w.Equals("si"))
            {
                Console.WriteLine("____ Ingrese el nivel");
                Console.Write("___*** : ");
                int nivelEscogido = Int32.Parse(Console.ReadLine());
                nivel = nivelEscogido;
                OperacionPrincipal(tablero, tableroPadre, arreglotablero);
            }
            else if (w.Equals("no"))
            {
                OperacionPrincipal(tablero, tableroPadre, arreglotablero);
            }
            else
            {
                Console.WriteLine("***** NO se entendio la instrucción *****");
                Console.ReadKey();
                ObtenerProfundidad(tablero, tableroPadre, arreglotablero);
            }

        }

        private void OperacionPrincipal(int[,] tablero, int[,] tableroPadre, int[] arregloTablero)
        {
            Console.WriteLine("\t\t\t::::::: Ejecutando \n");
            ProcesoRecursivo(tablero, tableroPadre, 0, tamano, tamano);
            Console.WriteLine("\n\t\t\t::::::: Resultados \n");
            if (Solucion.Count.Equals(0))
            {
                Console.WriteLine("\t\t\t***** NO se encontro Solucion, revise profundidad ***");
            }
            else
            {
                Console.WriteLine("___### : Memoria utilizada: " + espacioMemoria + " bits. ("+(espacioMemoria * 0.000125)+" kb).");
                Console.WriteLine("___### : Nodos creados: " + cantidadNodos);
                Console.WriteLine("___### : Movimientos solución: " + totalMovimientos);
                Console.WriteLine("___### : Se encontraron : {0} soluciones pero el recorrido más óptimo es el siguiente\n (profundidad:' {1} ')", cuentaResultados, +profundidad );
                Console.WriteLine("\n\t\t\t ### Enter: Imprimir ### ");
                Console.ReadKey();
                foreach (int[,] objeto in Solucion)
                {
                    ImprimirMatriz(objeto);
                }
                Console.WriteLine("\t\t\t Movimientos: {0} ", (Solucion.Count - 1));

            }
            espacioMemoria += (10 * 32) + (listaMovimientos.Count * dimension * 32) + (Solucion.Count * dimension * 32);
            Console.WriteLine("\n");

        }

        private void ProcesoRecursivo(int[,] tablero, int[,] tableroPapa, int profundidadActual, int fila, int columna)
        {
            espacioMemoria += (fila * columna * 32 * 2); //tablero padre e hijo
            int columnaVacio = 0;
            int filaVacio = 0;
            espacioMemoria += 32 * 3; //filaVacio, columnaVacio, i
            bool hallarVacio = false;
            espacioMemoria += 8;
            for (int i = 0; i < tamano; i++)
            {
                for (j = 0; j < tamano; j++)
                {
                    if (tablero[i,j].Equals(0))
                    {
                        filaVacio = i;
                        columnaVacio = j;
                        hallarVacio = true;
                        break;
                    }
                }
                if (hallarVacio.Equals(true))
                {
                    break;
                }
            }
            if (profundidadActual.Equals(mejorProfundidad)) //Poda para evitar buscar en ramas innecesarias
            {
                return;
            }
            else
            {
                cantidadNodos++;
                addListaMovimientos(tablero);
                LinkedList<int[,]> listaHijos = new LinkedList<int[,]>();
                bool sigue = false;
                espacioMemoria += 8;
                int[,] opcionA = new int[tamano,tamano];
                int[,] opcionB = new int[tamano, tamano];
                int[,] opcionC = new int[tamano, tamano];
                int[,] opcionD = new int[tamano, tamano];

                espacioMemoria += 32;
                for (int i = 0; i < tamano; i++)
                {
                    for (j = 0; j < tamano; j++)
                    {
                        opcionA[i,j] = tablero[i,j];
                        opcionB[i,j] = tablero[i,j];
                        opcionC[i,j] = tablero[i,j];
                        opcionD[i,j] = tablero[i,j];
                    }
                }
                espacioMemoria += (fila * columna * 4 * 32);
                if (filaVacio == tamano-1 && columnaVacio == tamano-1)
                {
                    if (orden(tablero).Equals(true))
                    {
                        if (nivel.Equals(0) || profundidadActual.Equals(nivel))
                        {
                            Console.WriteLine("\t\tProfundida Sol: " + profundidadActual);
                            meterSolucion(profundidadActual);
                        }
                        mejorProfundidad = profundidadActual;
                        listaMovimientos.RemoveLast();
                        return;
                    }
                    else
                    {
                        sigue = true;
                    }
                }
                if ((profundidadActual + 1) >= mejorProfundidad)
                {
                    listaMovimientos.Remove(tablero);
                }
                sigue = true;
                if (sigue.Equals(true))
                {
                    if (filaVacio == 0 && columnaVacio == 0)
                    {
                        int tempA1 = opcionA[filaVacio,columnaVacio + 1];
                        int tempA2 = opcionA[filaVacio + 1,columnaVacio];
                        espacioMemoria += 64;
                        opcionA[filaVacio,columnaVacio] = tempA1;
                        opcionB[filaVacio,columnaVacio] = tempA2;
                        opcionA[filaVacio,columnaVacio + 1] = 0;
                        opcionB[filaVacio + 1,columnaVacio] = 0;
                        listaHijos.Clear();
                        listaHijos.AddLast(opcionA);
                        listaHijos.AddLast(opcionB);
                    }
                    else if (filaVacio == tamano-1 && columnaVacio == 0)
                    {
                        int a1 = opcionA[filaVacio,columnaVacio + 1];
                        int a2 = opcionA[filaVacio - 1,columnaVacio];
                        espacioMemoria += 64;
                        opcionA[filaVacio,columnaVacio] = a1;
                        opcionB[filaVacio,columnaVacio] = a2;
                        opcionA[filaVacio,columnaVacio + 1] = 0;
                        opcionB[filaVacio - 1,columnaVacio] = 0;
                        listaHijos.Clear();
                        listaHijos.AddLast(opcionA);
                        listaHijos.AddLast(opcionB);
                    }
                    else if (filaVacio == tamano-1 && columnaVacio == tamano-1)
                    {
                        int a1 = opcionA[filaVacio - 1,columnaVacio];
                        int a2 = opcionA[filaVacio,columnaVacio - 1];
                        espacioMemoria += 64;
                        opcionA[filaVacio,columnaVacio] = a1;
                        opcionB[filaVacio,columnaVacio] = a2;
                        opcionA[filaVacio - 1,columnaVacio] = 0;
                        opcionB[filaVacio,columnaVacio - 1] = 0;
                        listaHijos.Clear();
                        listaHijos.AddLast(opcionA);
                        listaHijos.AddLast(opcionB);
                    }
                    else if (filaVacio == 0 && columnaVacio == tamano-1)
                    {
                        int a1 = opcionA[filaVacio,columnaVacio - 1];
                        int a2 = opcionA[filaVacio + 1,columnaVacio];
                        espacioMemoria += 64;
                        opcionA[filaVacio,columnaVacio] = a1;
                        opcionB[filaVacio,columnaVacio] = a2;
                        opcionA[filaVacio,columnaVacio - 1] = 0;
                        opcionB[filaVacio + 1,columnaVacio] = 0;
                        listaHijos.Clear();
                        listaHijos.AddLast(opcionA);
                        listaHijos.AddLast(opcionB);
                    }
                    else if (filaVacio == 0)
                    {
                        if ((columnaVacio > 0) && (columnaVacio < tamano-1))
                        {
                            int a1 = opcionA[filaVacio + 1,columnaVacio];
                            int a2 = opcionA[filaVacio,columnaVacio + 1];
                            int a3 = opcionA[filaVacio,columnaVacio - 1];
                            espacioMemoria += 96;
                            opcionA[filaVacio,columnaVacio] = a1;
                            opcionB[filaVacio,columnaVacio] = a2;
                            opcionC[filaVacio,columnaVacio] = a3;
                            opcionA[filaVacio + 1,columnaVacio] = 0;
                            opcionB[filaVacio,columnaVacio + 1] = 0;
                            opcionC[filaVacio,columnaVacio - 1] = 0;
                            listaHijos.Clear();
                            listaHijos.AddLast(opcionA);
                            listaHijos.AddLast(opcionB);
                            listaHijos.AddLast(opcionC);
                        }
                    }
                    else if (filaVacio == tamano-1)
                    {
                        int a1 = opcionA[filaVacio - 1,columnaVacio];
                        int a2 = opcionA[filaVacio,columnaVacio + 1];
                        int a3 = opcionA[filaVacio,columnaVacio - 1];
                        espacioMemoria += 96;
                        opcionA[filaVacio,columnaVacio] = a1;
                        opcionB[filaVacio,columnaVacio] = a2;
                        opcionC[filaVacio,columnaVacio] = a3;
                        opcionA[filaVacio - 1,columnaVacio] = 0;
                        opcionB[filaVacio,columnaVacio + 1] = 0;
                        opcionC[filaVacio,columnaVacio - 1] = 0;
                        listaHijos.Clear();
                        listaHijos.AddLast(opcionA);
                        listaHijos.AddLast(opcionB);
                        listaHijos.AddLast(opcionC);
                    }
                    else if ((filaVacio > 0) && (filaVacio < tamano-1))
                    {
                        if (columnaVacio == 0)
                        {
                            int a1 = opcionA[filaVacio - 1,columnaVacio];
                            int a2 = opcionA[filaVacio + 1,columnaVacio];
                            int a3 = opcionA[filaVacio,columnaVacio + 1];
                            espacioMemoria += 96;
                            opcionA[filaVacio,columnaVacio] = a1;
                            opcionB[filaVacio,columnaVacio] = a2;
                            opcionC[filaVacio,columnaVacio] = a3;
                            opcionA[filaVacio - 1,columnaVacio] = 0;
                            opcionB[filaVacio + 1,columnaVacio] = 0;
                            opcionC[filaVacio,columnaVacio + 1] = 0;
                            listaHijos.Clear();
                            listaHijos.AddLast(opcionA);
                            listaHijos.AddLast(opcionB);
                            listaHijos.AddLast(opcionC);
                        }
                        else if (columnaVacio == tamano-1)
                        {
                            int a1 = opcionA[filaVacio - 1,columnaVacio];
                            int a2 = opcionA[filaVacio + 1,columnaVacio];
                            int a3 = opcionA[filaVacio,columnaVacio - 1];
                            espacioMemoria += 96;
                            opcionA[filaVacio,columnaVacio] = a1;
                            opcionB[filaVacio,columnaVacio] = a2;
                            opcionC[filaVacio,columnaVacio] = a3;
                            opcionA[filaVacio - 1,columnaVacio] = 0;
                            opcionB[filaVacio + 1,columnaVacio] = 0;
                            opcionC[filaVacio,columnaVacio - 1] = 0;
                            listaHijos.Clear();
                            listaHijos.AddLast(opcionA);
                            listaHijos.AddLast(opcionB);
                            listaHijos.AddLast(opcionC);
                        }
                        else
                        {
                            int a1 = opcionA[filaVacio - 1,columnaVacio];
                            int a2 = opcionA[filaVacio + 1,columnaVacio];
                            int a3 = opcionA[filaVacio,columnaVacio - 1];
                            int a4 = opcionA[filaVacio,columnaVacio + 1];
                            espacioMemoria += 96;
                            opcionA[filaVacio,columnaVacio] = a1;
                            opcionB[filaVacio,columnaVacio] = a2;
                            opcionC[filaVacio,columnaVacio] = a3;
                            opcionD[filaVacio,columnaVacio] = a4;
                            opcionA[filaVacio - 1,columnaVacio] = 0;
                            opcionB[filaVacio + 1,columnaVacio] = 0;
                            opcionC[filaVacio,columnaVacio - 1] = 0;
                            opcionD[filaVacio,columnaVacio + 1] = 0;
                            listaHijos.AddLast(opcionA);
                            listaHijos.AddLast(opcionB);
                            listaHijos.AddLast(opcionC);
                            listaHijos.AddLast(opcionD);
                        }
                    }
                }
                int filtrarMejoresHijos = 0;
                espacioMemoria += 32;
                espacioMemoria += (fila * columna) * listaHijos.Count * 32;
                foreach (var item in listaHijos)
                {
                    if (compara2Tableros(item, tableroPapa).Equals(false))
                    {
                        filtrarMejoresHijos++;
                    }
                }
                int[][] nuevoArreglo = new int[filtrarMejoresHijos][];
                int vuelta = 0;
                espacioMemoria += 64;
                foreach (var item in listaHijos)
                {
                    if (compara2Tableros(item, tableroPapa).Equals(false))
                    {
                        nuevoArreglo[vuelta] = convertirArreglo(tamano, tamano, item);
                        vuelta++;
                    }
                }
                espacioMemoria += filtrarMejoresHijos * vuelta;

                espacioMemoria += 32;
                for (int i = 0; i < nuevoArreglo.Length; i++)
                {
                    if (compara2Tableros(convertirMatriz(nuevoArreglo[i], tamano, tamano), tableroPapa).Equals(false))
                    {
                            ProcesoRecursivo(convertirMatriz(nuevoArreglo[i], tamano, tamano), tablero, profundidadActual + 1,tamano, tamano);
                    }
                }
                listaMovimientos.Remove(tablero);
            }
        }

        private bool orden(int[,] tablero) 
        {
            espacioMemoria += 32;
            for (int i = 0; i < tamano; i++)
            {
                for (j = 0; j < tamano; j++)
                {
                    if (tablero[i,j] != tableroObjetivo[i, j]){
                        return false;
                    }
                }
            }
            return true;
        }

        private void addListaMovimientos(int[,] tablero)
        {
            listaMovimientos.AddLast(tablero);
        }

        private int[,] convertirMatriz(int[] arregloConvertir, int filas, int columnas)
        {

            int[,] tableroNuevo = new int[filas,columnas];
            int indice = 0;
            espacioMemoria += (dimension * 32) + 64; //TableroNuevo + Indice + i
            for (int i = 0; i < tamano; i++)
            {
                for (j = 0; j < tamano; j++)
                {
                    tableroNuevo[i,j] = arregloConvertir[indice];
                    indice++;
                }
            }
            return tableroNuevo;
        }

        private int[] convertirArreglo(int filas, int columnas, int[,] tableroConvertir)
        {
            int[] arreglo = new int[dimension];
            int vuelta = 0;

            espacioMemoria += (dimension * 32) + 64; //  arreglo + vuelta + i
            for (int i = 0; i < tamano; i++)
            {
                for (j = 0; j < tamano; j++)
                {
                    arreglo[vuelta] = tableroConvertir[i,j];
                    vuelta++;
                }
            }
            return arreglo;
        }

        private bool compara2Tableros(int[,] tablero, int[,] tablero2)
        {
            espacioMemoria += 32;
            for (int i = 0; i < tamano; i++)
            {
                for (j = 0; j < tamano; j++)
                {
                    if (!tablero2[i,j].Equals(tablero[i,j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void meterSolucion(int profParam)
        {
            cuentaResultados++;
            totalMovimientos += (listaMovimientos.Count - 1);
            if (Solucion.Count.Equals(0))
            {
                Solucion.Clear();
                foreach (var tablero in listaMovimientos)
                {
                    Solucion.AddLast(crearNuevoTablero(tablero));
                }
                profundidad = profParam;
            }
            else
            {
                if (listaMovimientos.Count < Solucion.Count)
                {
                    Solucion.Clear();
                    foreach (var tablero in listaMovimientos)
                    {
                        Solucion.AddLast(crearNuevoTablero(tablero));
                    }
                    profundidad = profParam;
                }
            }
        }

        private int[,] crearNuevoTablero(int[,] tablero)
        {
            int[,] auxTablero = new int[tamano,tamano];
            espacioMemoria += (32 * dimension) + 32;
            for (int i = 0; i < tamano; i++)
                for (j = 0; j < tamano; j++)
                    auxTablero[i,j] = tablero[i,j];

            return auxTablero;
        }

        private void ImprimirMatriz(int[,] tablero)
        {
            int numero;
            Console.WriteLine("\n\n\n\t {0}: ", "Movimiento: ");
            for (int i = 0; i < tamano; i++)
            {
                Console.Write("\t\t\t  | ");
                for (j = 0; j < tamano; j++)
                {
                    numero = tablero[i, j];
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
            Console.WriteLine("\n");
        }      
    }
}
