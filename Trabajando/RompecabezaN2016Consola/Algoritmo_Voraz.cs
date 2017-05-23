using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RompecabezaN2016Consola
{
    class Algoritmo_Voraz
    {

        double espacioDisco = 0; //Variable para guardar el espacio en memoria
        int filaActual = 0;     //variable para conocer la fila que se esta ordenando
        int dimension;          //dimensión del tablero
        int[,] tableroObjetivo; //tablero meta o solución
        int[,] tablero;         //tablero de juego, inicia desordenado
        int numMain = 1;        //Contador para manejar el numero que se va a ordenar
        int filaNumMain = 0;    //Fila del numero a ordenar
        int columnaNumMain = 0; //Columna del numero a ordenar
        int filaDestino = 0;    //Fila de la posición en donde debe colocarse el numero
        int columnaDestino = 0; //Columna de la posición donde debe colocarse el número
        int filaDel0 = 0;       //Fila del elemento vacío
        int columnaDel0 = 0;    //Columna del elemento vacío
        int movimientos = 0;
        int j;
        ArrayList ordenados = new ArrayList();  // Array con los números que ya se encuentran ordenados, para evitar moverlos

        // Proceso donde se inicializa el tablero y se llama al inicio del ordenamiento
        public void Inicio(int dim, int[,] tableroOriginal)
        {
            tablero = new int[dim, dim];
            Array.Copy(tableroOriginal, tablero, dim * dim);
            dimension = dim;
            GenerarObjetivo();
            ImprimirTablero(tablero);
            ProcOrdenamiento();
            Console.WriteLine("Proceso finalizado. Total de movimientos realizados: " + movimientos);
            CalcularEspacioDisco();
            Console.WriteLine("Espacio en memoria utilizado: " + espacioDisco + " bits. ");


        }

        // Función que genera el tablero objetivo dependiendo de las dimensiones dadas
        private void GenerarObjetivo()
        {   
            tableroObjetivo = new int[dimension, dimension];
            int cont = 1;
            espacioDisco += 32; //cont
            espacioDisco += 32; // i
            for (int i = 0; i < dimension; i++)
            {
                for (j = 0; j < dimension; j++)
                {
                    tableroObjetivo[i, j] = cont;
                    cont++;
                }
            }
            tableroObjetivo[dimension - 1, dimension - 1] = 0;
        }

        //Función para imprimir el tablero
        private void ImprimirTablero(int[,] tableroParam)
        {
            espacioDisco += 32;
            for (int i = 0; i < dimension; i++)
            {
                Console.Write("\t\t|");
                for (j = 0; j < dimension; j++)
                {
                    int numero = tableroParam[i, j];
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
            Console.WriteLine();
        }

        /// <summary>
        /// Proceso principal de ordenamiento. Se lleva un conteo de las vueltas para saber en que fila se están ordenando los números.
        /// Para las filas superiores se utiliza una estrategia de ordenamiento común. No así para la penútima y última fila, que requieren de 
        /// un proceso específico. Es por esto que surgen la primera y segunda estrategia.
        /// </summary>
        private void ProcOrdenamiento()
        {

            int vuelta = 1;
            espacioDisco += 32;
            ObtenerPosicion0();
            while (true)
            {
                if (filaActual == dimension - 2)
                {
                    break;
                }
                if (vuelta == dimension)
                {
                    filaActual++;
                    vuelta = 0;
                }
                Proceso1();
                vuelta++;
                Console.ReadLine();
            }
            Proceso2();
        }

        /// <summary>
        /// Primera estrategia consiste en ir ordenando los números de uno en uno, de izquierda a derecha.
        /// Para los números que se encuentran en la última columna se aplica un algoritmo especial, que requiere un proceso previo y
        /// luego aplica una serie de movimientos fijos.
        /// </summary>
        private void Proceso1()
        {
            Console.WriteLine("Primer Caso...");
            ObtenerPosicionNumero();
            if (columnaDestino == dimension - 1) //Caso de los numeros que estan en la última columna.
            {
                if (tablero[filaDestino, columnaDestino] == tableroObjetivo[filaDestino, columnaDestino])//Ya esta ordenado
                {
                    //
                }
                else if (filaDel0 == filaDestino && columnaDel0 == columnaDestino &&
                    filaNumMain - 1 == filaDestino && columnaNumMain == columnaDestino)//Caso en q esta listo para acomodar
                {
                    MoverNumMain();
                }
                else //Caso especial que requiere un proceso previo de ubicación del número y el vacío.
                {
                    AcomodarNumeroLateral(filaDestino + 2, columnaDestino);
                    ColocarVacioEnLateralIzq();
                    IntercambiarNumero(tablero[filaDestino, columnaDestino - 1], filaDestino, columnaDestino - 1);
                    IntercambiarNumero(tablero[filaDel0 + 1, columnaDel0], filaDel0 + 1, columnaDel0);
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                    IntercambiarNumero(tablero[filaDel0 + 1, columnaDel0], filaDel0 + 1, columnaDel0);
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 - 1], filaDel0, columnaDel0 - 1);
                    IntercambiarNumero(tablero[filaDel0 - 1, columnaDel0], filaDel0 - 1, columnaDel0);
                    IntercambiarNumero(tablero[filaDel0 - 1, columnaDel0], filaDel0 - 1, columnaDel0);
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                    IntercambiarNumero(tablero[filaDel0 + 1, columnaDel0], filaDel0 + 1, columnaDel0);
                }
            }
            else
            {
                while (tablero[filaDestino, columnaDestino] != tableroObjetivo[filaDestino, columnaDestino])//Ciclo que se repite hasta colocar el numero donde corresponde
                {
                    //Se requirió hacer una validación en un caso especial, donde el vacío deberá rodear el número a acomodar para no afectar otros ya ordenados
                    if ((filaDel0 == filaNumMain && columnaDel0 + 1 == columnaNumMain) && (ordenados.Contains(numMain - 1))
                        && (filaDestino + 1 == filaNumMain && columnaDestino == columnaNumMain))
                    {
                        IntercambiarNumero(tablero[filaDel0 + 1, columnaDel0], filaDel0 + 1, columnaDel0);
                        IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                        IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                    }
                    else if ((filaDel0 == filaNumMain + 1 && columnaDel0 == columnaNumMain) &&
                         (filaNumMain - 1 == filaDestino && columnaNumMain == columnaDestino))
                    {
                        IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                    }
                    else if (filaNumMain > filaDestino && !(ordenados.Contains(tablero[filaNumMain - 1, columnaNumMain])))//Mover el número hacia arriba
                    {
                        UbicarVacio(filaNumMain - 1, columnaNumMain);
                        MoverNumMain();
                    }
                    else if (filaNumMain + 1 < dimension && !(filaNumMain + 1 == filaDel0 && columnaNumMain == columnaDel0))//Ubicar el vacío debajo del número
                    {
                        UbicarVacio(filaNumMain + 1, columnaNumMain);
                        ImprimirTablero(tablero);
                        Console.ReadLine();
                    }
                    else if (columnaNumMain > columnaDestino)
                    {
                        UbicarVacio(filaNumMain, columnaNumMain - 1); //Mover a izquierda
                        MoverNumMain();
                    }
                    else if (columnaNumMain < columnaDestino)   //Mover a derecha
                    {
                        UbicarVacio(filaNumMain, columnaNumMain + 1);
                        MoverNumMain();
                    }

                }
            }
            Console.WriteLine("Logrado con el " + numMain);
            columnaDestino++;
            if (columnaDestino == dimension)//Se reestablece la columnaDestino en 0 cada vez que se inicia en una nueva fila
            {
                filaDestino++;
                columnaDestino = 0;
            }
            ordenados.Add(numMain);
            numMain++;
        }

        ///Función de ordenamiento previo al caso especial de los números en la última columna
        /// También se utiliza en la segunda estrategia, ya que se trata de un algoritmo similar
        private void AcomodarNumeroLateral(int tFilaDest, int tColumnaDest)
        {
            while (tablero[tFilaDest, tColumnaDest] != tablero[filaNumMain, columnaNumMain])
            {
                if (filaNumMain + 1 < dimension)
                {
                    UbicarVacio(filaNumMain + 1, columnaNumMain);
                }
                if (filaNumMain < tFilaDest)
                {
                    UbicarVacio(filaNumMain + 1, columnaNumMain);
                    MoverNumMain();
                }
                else if (filaNumMain > tFilaDest)
                {
                    UbicarVacio(filaNumMain - 1, columnaNumMain);
                    MoverNumMain();
                }
                else if (columnaNumMain < tColumnaDest)
                {
                    UbicarVacio(filaNumMain, columnaNumMain + 1);
                    MoverNumMain();
                }
                else if (columnaNumMain > tColumnaDest)
                {
                    UbicarVacio(filaNumMain, columnaNumMain - 1);
                    MoverNumMain();
                }
                ImprimirTablero(tablero);
            }
        }
        //Funcion previa del caso especial de números en última columna, para colocar el vacío en una posición específica
        private void ColocarVacioEnLateralIzq()
        {
            while (filaDel0 != filaDestino || columnaDel0 != columnaDestino)
            {
                if (columnaDel0 < columnaDestino && columnaDel0 + 1 < dimension && !(filaDel0 == filaNumMain && columnaDel0 + 1 == columnaNumMain) && !(filaDel0 == filaDestino && columnaDel0 + 1 == columnaDestino - 1))
                {
                    //Derecha
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                    ImprimirTablero(tablero);

                }
                else if (filaDel0 > filaDestino && filaDel0 - 1 >= 0 && !(filaDel0 - 1 == filaNumMain && columnaDel0 == columnaNumMain) && !(filaDel0 - 1 == filaDestino && columnaDel0 == columnaDestino - 1))
                {
                    //Arriba
                    IntercambiarNumero(tablero[filaDel0 - 1, columnaDel0], filaDel0 - 1, columnaDel0);
                    ImprimirTablero(tablero);
                }
            }
        }

        //Función utilizada en el proceso de las dos últimas filas. Es similar al anterior pero sigue un orden distinto 
        private void ColocarVacioEnLateralDer(int filaDest, int columnaDest)
        {
            while (filaDel0 != filaDest || columnaDel0 != columnaDest)
            {
                Console.ReadLine();
                if (columnaDel0 > columnaDest && columnaDel0 - 1 >= 0 && !(filaDel0 == filaNumMain && columnaDel0 - 1 == columnaNumMain)
                    && !(filaDel0 == filaDestino && columnaDel0 - 1 == columnaDestino))
                {
                    //Izquierda
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 - 1], filaDel0, columnaDel0 - 1);

                }
                else if (filaDel0 <= filaDest && filaDel0 + 1 < dimension && !(filaDel0 + 1 == filaDestino && columnaDel0 == columnaDestino))
                {
                    //Abajo
                    IntercambiarNumero(tablero[filaDel0 + 1, columnaDel0], filaDel0 + 1, columnaDel0);
                }
                else if (filaDel0 > filaDest && filaDel0 - 1 >= filaActual && !(filaDel0 - 1 == filaNumMain && columnaDel0 == columnaNumMain))
                {
                    //Arriba
                    IntercambiarNumero(tablero[filaDel0 - 1, columnaDel0], filaDel0 - 1, columnaDel0);

                }

            }
        }

        /// <summary>
        /// Segunda estrategia se utiliza para ordenar las dos ultimas filas del tablero. El procesp consiste en ir ordenando las columnas de izquierda
        /// a derecha, hasta llegar a una matriz de 2x2 que se puede ordenar simplemente girando piezas
        /// </summary>
        private void Proceso2()
        {
            Console.WriteLine("Segundo Caso");
            filaDestino = dimension - 1;
            columnaDestino = 0;
            while (columnaDestino < dimension - 2)
            {
                numMain = tableroObjetivo[filaDestino, columnaDestino];
                ObtenerPosicionNumero();
                ColocarNumeroEnUltFila();
                if ((filaDel0 == filaNumMain - 1) && (columnaDel0 + 1 < dimension))
                {
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                }
                ImprimirTablero(tablero);
                ordenados.Add(numMain);
                numMain = tableroObjetivo[filaDestino - 1, columnaDestino];
                ObtenerPosicionNumero();
                if (numMain == tablero[filaDestino - 1, columnaDestino])
                {
                    columnaDestino++;
                }
                else
                {
                    if (tablero[filaDestino - 1, columnaDestino + 2] != numMain)
                    {
                        AcomodarNumeroLateral(filaDestino - 1, columnaDestino + 2);
                        ImprimirTablero(tablero);
                    }
                    ColocarVacioEnLateralDer(filaDestino - 1, columnaDestino);
                    ImprimirTablero(tablero);
                    IntercambiarNumero(tablero[filaDel0 + 1, columnaDel0], filaDel0 + 1, columnaDel0);
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                    IntercambiarNumero(tablero[filaDel0 - 1, columnaDel0], filaDel0 - 1, columnaDel0);
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                    IntercambiarNumero(tablero[filaDel0 + 1, columnaDel0], filaDel0 + 1, columnaDel0);
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 - 1], filaDel0, columnaDel0 - 1);
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 - 1], filaDel0, columnaDel0 - 1);
                    IntercambiarNumero(tablero[filaDel0 - 1, columnaDel0], filaDel0 - 1, columnaDel0);
                    IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
                    ordenados.Add(numMain);
                    columnaDestino++;
                }
            }
            while (!(tablero[dimension - 2, dimension - 2] == tableroObjetivo[dimension - 2, dimension - 2] &&
                tablero[dimension - 1, dimension - 2] == tableroObjetivo[dimension - 1, dimension - 2] &&
                tablero[dimension - 2, dimension - 1] == tableroObjetivo[dimension - 2, dimension - 1]))
            {
                GirarPiezas();
            }
        }

        //Proceso para acomodar la matriz 2x2 que se presenta en la segunda estrategia.
        private void GirarPiezas()
        {
            if (filaDel0 == dimension - 2 && columnaDel0 == dimension - 2)
            {
                IntercambiarNumero(tablero[filaDel0 + 1, columnaDel0], filaDel0 + 1, columnaDel0);
            }
            else if (filaDel0 == dimension - 1 && columnaDel0 == dimension - 2)
            {
                IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);
            }
            else if (filaDel0 == dimension - 1 && columnaDel0 == dimension - 1)
            {
                IntercambiarNumero(tablero[filaDel0 - 1, columnaDel0], filaDel0 - 1, columnaDel0);
            }
            else if (filaDel0 == dimension - 2 && columnaDel0 == dimension - 1)
            {
                IntercambiarNumero(tablero[filaDel0, columnaDel0 - 1], filaDel0, columnaDel0 - 1);
            }
        }

        ///Proceso necesario en la segunda estrategia para ordenar cada columna de izquierda a derecha. Solo se trabaja en dos filas,
        /// por lo que primero se debe ordenar el número de la fila final y después el de arriba.
        private void ColocarNumeroEnUltFila()
        {
            while (tablero[filaDestino, columnaDestino] != numMain)
            {
                if (filaNumMain + 1 < dimension)
                {
                    UbicarVacio(filaNumMain + 1, columnaNumMain);
                    MoverNumMain();
                }
                else if (filaNumMain - 1 > filaActual - 1 && !(filaDel0 == filaNumMain - 1 && columnaDel0 == columnaNumMain))
                {
                    UbicarVacio(filaNumMain - 1, columnaNumMain);
                }
                else if (columnaNumMain > columnaDestino)
                {
                    UbicarVacio(filaNumMain, columnaNumMain - 1);
                    MoverNumMain();
                }
            }
        }

        // Proceso utilizado para llevar al elemento vacío a una determinada posición del tablero.
        private void UbicarVacio(int fila, int columna)
        {
            while (filaDel0 != fila || columnaDel0 != columna)
            {
                if (filaDel0 > fila && filaDel0 - 1 >= 0 && !(filaDel0 - 1 == filaNumMain && columnaDel0 == columnaNumMain)
                    && !(ordenados.Contains(tablero[filaDel0 - 1, columnaDel0]))) //arriba
                {
                    IntercambiarNumero(tablero[filaDel0 - 1, columnaDel0], filaDel0 - 1, columnaDel0);

                }
                else if (columnaDel0 >= columna && columnaDel0 - 1 >= 0 && !(filaDel0 == filaNumMain && columnaDel0 - 1 == columnaNumMain)
                    && !(ordenados.Contains(tablero[filaDel0, columnaDel0 - 1])))//Izquierda
                {

                    IntercambiarNumero(tablero[filaDel0, columnaDel0 - 1], filaDel0, columnaDel0 - 1);

                }
                else if (columnaDel0 <= columna && columnaDel0 + 1 < dimension && !(filaDel0 == filaNumMain && columnaDel0 + 1 == columnaNumMain))// derecha
                {

                    IntercambiarNumero(tablero[filaDel0, columnaDel0 + 1], filaDel0, columnaDel0 + 1);

                }
                if (filaDel0 < fila && filaDel0 + 1 < dimension && !(filaDel0 + 1 == filaNumMain && columnaDel0 == columnaNumMain)) //Abajo
                {
                    IntercambiarNumero(tablero[filaDel0 + 1, columnaDel0], filaDel0 + 1, columnaDel0);

                }
            }
        }

        //Función para obtener la posición del elemento vacío
        private void ObtenerPosicion0()
        {
            espacioDisco += 32; //i
            for (int i = 0; i < dimension; i++)
                for (j = 0; j < dimension; j++)
                {
                    if (tablero[i, j] == 0)
                    {
                        filaDel0 = i;
                        columnaDel0 = j;
                        break;
                    }
                }
        }

        //Función para conocer la ubicación del número que se desea ordenar
        private void ObtenerPosicionNumero()
        {
            espacioDisco += 32; //i
            for (int i = 0; i < dimension; i++)
                for (j = 0; j < dimension; j++)
                {
                    if (tablero[i, j] == numMain)
                    {
                        filaNumMain = i;
                        columnaNumMain = j;
                        break;
                    }
                }
        }

        //Proceso para intercambiar la posición de un número de la matriz con el elemento vacío
        private void IntercambiarNumero(int numero, int nuevaFila, int nuevaColumna)
        {
            tablero[filaDel0, columnaDel0] = numero;
            tablero[nuevaFila, nuevaColumna] = 0;
            filaDel0 = nuevaFila;
            columnaDel0 = nuevaColumna;
            movimientos++;
            ImprimirTablero(tablero);
            Console.ReadLine();
        }

        private void MoverNumMain()//Función para intercambiar el número a ordenar con el vacío
        {
            espacioDisco += 32 * 2; 
            int tempFilaVacio = filaDel0;
            int tempColVacio = columnaDel0;
            tablero[filaDel0, columnaDel0] = numMain;
            tablero[filaNumMain, columnaNumMain] = 0;
            filaDel0 = filaNumMain;
            columnaDel0 = columnaNumMain;
            filaNumMain = tempFilaVacio;
            columnaNumMain = tempColVacio;
            movimientos++;
            ImprimirTablero(tablero);
            Console.ReadLine();
        }
        private void CalcularEspacioDisco()
        {
            espacioDisco += 9 * 32;                          // 9 Variables globales que corresponden a enteros.
            espacioDisco += (Math.Pow(dimension, 2) * 2) * 32;   // El tablero a ordenar y el tablero objetivo
            espacioDisco += ordenados.Count * 32;           // Cantidad de numeros enteros almacenados en el ArrayList ordenados
        }

    }
}
