using System;
using System.Collections;
using System.Linq;


namespace RompecabezaN2016Consola
{
    class AlgoritmoGenetico
    {
        //Los cromosomas de las familias.
        private int[] cromosomaPadre;
        private int[] cromosomaMadre;
        private int[] cromosomaHijo1;
        private int[] cromosomaHijo2;
        private int[] cromosomaMeta;
        //Variables temporales, para acomodar o averiguar la posición de un elemento.
        private int[,] tablero;
        private ArrayList listaTemporal;
        //Contadores.
        private int dimencion; //Tamaño del arrglo.
        private int tamaño; //Tamaño de las filas y columnas.
        private int cantidadAciertos; //Para averiguar la adaptabilidad de cada cromosoma.
        private int genBuscar; //Numero que se va a buscar, el que este desordenado en el padre.
        private bool encontroSolución; //Para saber si ya esta la solución y no seguir generando desencientes.
        private int cantidadDecendientes; //Para saber los descendientes que lleva y ponerle un limite. 
        private int cantidadMovimientos; //Los movimientos que hace en el tablero con el 0.

        private int cantidadAciertosP; //Adaptabilidad  Padre
        private int cantidadAciertosM; //Adaptabilidad Madre
        private int cantidadAciertosH1; //Adaptabilidad Hijo 1
        private int cantidadAciertosH2;//Adaptabilidad Hijo 2
        private int cantMaxOrdenado; //La cantidad de numeros ordenados en cada decendencia.
        private int memoriaUtilizada; //Cantidad de memoria utilizada en tiempo de ejeución.

        public AlgoritmoGenetico(int[,] tablero, int tamaño)
        {
            memoriaUtilizada = 0;
            this.tamaño = tamaño;
            dimencion = tamaño * tamaño;

            cromosomaPadre = new int[dimencion];
            cromosomaMadre = new int[dimencion];
            cromosomaHijo1 = new int[dimencion];
            cromosomaHijo2 = new int[dimencion];
            cromosomaMeta = new int[dimencion];
            this.tablero = new int[tamaño, tamaño];
            cantidadAciertos = 0;
            cantidadAciertos = 0;
            listaTemporal = new ArrayList(dimencion);

            encontroSolución = false;
            int contador = 0;

            memoriaUtilizada += (32*1); //Memoria utilizada por el contador.

            int j; //Se sacaran los j para que no pida memoria nuevamente.
            for (int i = 0; i < tamaño; i++)
            {
                for (j = 0; j < tamaño; j++)
                {
                    cromosomaPadre[contador] = tablero[i, j];
                    cromosomaMadre[contador] = tablero[i, j];
                    contador++;
                }
            }

            memoriaUtilizada += (32 * 2); //La memoria nueva que pide los for, se piden una ves.

            Adaptabilidad(cromosomaPadre);
            cantMaxOrdenado = cantidadAciertosP;

            //GenerarMadre();
            //Mutacion(cromosomaMadre);
            MatrizTemporal(cromosomaMadre);
            GenerarMadre();
            GenerarMadre(); //Otra

            //int conta = 1;
            contador = 1;
            for (int i = 0; i < dimencion; i++)
            {
                if (i == dimencion - 1)
                    cromosomaMeta[i] = 0;

                else
                {
                    cromosomaMeta[i] = contador;
                    
                }
                contador++;
            }
            Imprimir(cromosomaMeta,"Meta");
            memoriaUtilizada += (32 * 1); //La memoria nueva que pide el for, se piden una ves.
        }

        public void inicio()
        {
            //Imprimir(cromosomaPadre, "Padre");
            //Imprimir(cromosomaMadre, "Madre");
            ImprimirCromosomaMatriz(cromosomaPadre, "Padre");
            ImprimirCromosomaMatriz(cromosomaMadre, "Madre");

            while (true)
            {
                LlenarHijo(cromosomaHijo1);
                LlenarHijo(cromosomaHijo2);

                //CopiarMadre();
                //Mutacion(cromosomaMadre);

                CopiarMadre();
                MatrizTemporal(cromosomaMadre);
                GenerarMadre();
                GenerarMadre();//otra

                Cruce(cromosomaPadre, cromosomaMadre, cromosomaHijo1);
                Cruce(cromosomaMadre, cromosomaPadre, cromosomaHijo2);

                cantidadDecendientes++;

                Console.WriteLine("\n\t\t **********++++++++++ Imprimiendo familia: ++++++++++**********\n");
                //Imprimir(cromosomaPadre, "Padre"); //Los imprime como cromosoma.
                //Imprimir(cromosomaMadre, "Madre");
                ImprimirCromosomaMatriz(cromosomaPadre, "Padre");
                ImprimirCromosomaMatriz(cromosomaMadre, "Madre");

                MutaciónMatriz(cromosomaHijo1, "Hijo1");
                //Imprimir(cromosomaHijo1, "Hijo1");
                ImprimirCromosomaMatriz(cromosomaHijo1, "Hijo 1");

                MutaciónMatriz(cromosomaHijo2, "Hijo2");
                //Imprimir(cromosomaHijo2, "Hijo2");
                ImprimirCromosomaMatriz(cromosomaHijo2, "Hijo 2");

                Evaluacion();

                if ((encontroSolución == true) || (cantidadDecendientes == 50))
                {
                    if (encontroSolución == true)
                    {
                        MatrizTemporal(cromosomaPadre);
                        ImprimirMatriz("Solución: \n");
                        MemoriaUtilizada();
                        Console.WriteLine("\n\tCantidad de descencientes hechos: {0}\n\tCantidad de movimientos realizados {1}",
                            cantidadDecendientes, cantidadMovimientos);
                    }
                    else
                    {
                        MatrizTemporal(cromosomaPadre);
                        ImprimirMatriz("Solución más cercana: !Se duplico un GEN en la mutación¡\n");
                        MemoriaUtilizada();
                        Console.WriteLine("\n\tCantidad de descencientes hechos: {0}\n\t Cantidad de movimientos realizados {1}",
                            cantidadDecendientes,cantidadMovimientos);
                    }
                    break;
                }
            }
            return;
        }

        /// <summary>
        /// Llena los hijos con -1 para limpiar los cromosomas y luego poder hacer el cruce.
        /// </summary>
        /// <param name="tempHijo">El hijo que va a llenar</param>
        private void LlenarHijo(int[] tempHijo)
        {
            for (int i = 0; i < dimencion; i++)
            {
                tempHijo[i] = -1;
            }
            memoriaUtilizada += (32 * 1); //La memoria nueva que pide el for, se piden una ves.
        }

        /// <summary>
        /// Copia en el cromosoma madre todo lo que esta en el cromosoma padre.
        /// </summary>
        private void CopiarMadre()
        {
            for (int i = 0; i < dimencion; i++)
            {
                cromosomaMadre[i] = cromosomaPadre[i];
            }
            memoriaUtilizada += (32 * 1); //La memoria nueva que pide el for, se piden una ves.
        }

        /// <summary>
        /// Genera una madre, moviendo el cero con las respectivas reglas del juego a lo random.
        /// </summary>
        public void GenerarMadre()
        {
            int y = 0;
            int x = 0;
            bool final = false;
            memoriaUtilizada += (32 * 2) + 8; //El x e y con el boleano.

            int j;
            for (int i = 0; i < tamaño; i++)
            {
                for (j = 0; j < tamaño; j++)
                {
                    if (tablero[i, j] == 0)
                    {
                        y = i;
                        x = j;
                        final = true;
                        break;
                    }
                }
                if(final == true){
                    break;
                }
            }
            memoriaUtilizada += (32 * 2); //Los entereros requeridos por los for
            if ((y == dimencion - 1) && (x == dimencion - 1))
                return;
            //(0-3: Izq),(4-7: Der),(8-11: Arri),(12-16: Aba) 
            int dirección;
            int temp;
            Random rnd = new Random();
            dirección = rnd.Next(0, 16);
            memoriaUtilizada += (32 * 2);

            try
            {
                if ((dirección >= 0) && (dirección < 4))
                {
                    temp = tablero[y, x - 1];
                    tablero[y, x - 1] = tablero[y, x];
                    tablero[y, x] = temp;
                    Console.WriteLine("Movimientos Madre:  ({0},{1}) -> ({2}, {3})", y, x, y, x - 1);
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("Esto paso| :\t" + e.Message);
            }
            try
            {
                if ((dirección >= 4) && (dirección < 8))
                {
                    temp = tablero[y, x + 1];
                    tablero[y, x + 1] = tablero[y, x];
                    tablero[y, x] = temp;
                    Console.WriteLine("Movimientos Madre:  ({0},{1}) -> ({2}, {3})", y, x, y, x + 1);
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("Esto paso| :\t" + e.Message);
            }
            try
            {
                if ((dirección >= 8) && (dirección < 12))
                {
                    temp = tablero[y + 1, x];
                    tablero[y + 1, x] = tablero[y, x];
                    tablero[y, x] = temp;
                    Console.WriteLine("Movimientos Madre:  ({0},{1}) -> ({2}, {3})", y, x, y + 1, x);
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine("Esto paso| :\t" + e.Message);
            }

            try
            {
                if ((dirección >= 12) && (dirección < 16))
                {
                    temp = tablero[y - 1, x];
                    tablero[y - 1, x] = tablero[y, x];
                    tablero[y, x] = temp;
                    Console.WriteLine("Movimientos Madre:  ({0},{1}) -> ({2}, {3})", y, x, y - 1, x);
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine("Esto paso| :\t" + e.Message);
            }

            int cont = 0;
            for (int i = 0; i < tamaño; i++)
            {
                for (j = 0; j < tamaño; j++)
                {
                    cromosomaMadre[cont] = tablero[i, j];
                    cont++;
                }
            }
            memoriaUtilizada += (32 * 2) + 32; //Los entereros requeridos por los for y el cont
        }

        /// <summary>
        /// Realiza los cruces respectivos para generar nuevos decendiente, se utiliza la logica de cruce en 1 obtenida en las busqueda
        /// de referencias y lo que se vio en clases.
        /// para no copiar numeros, aunque a veces lo hace.
        /// </summary>
        /// <param name="tempPadre">Cromosoma Padre</param>
        /// <param name="tempMadre">Cromosoma Madre</param>
        /// <param name="tempHijo">Cromosoma hijo</param>
        private void Cruce(int[] tempPadre, int[] tempMadre, int[] tempHijo)
        {

            int indice = 0;
            int genIndiceM = 0;
            memoriaUtilizada += (32 * 2); //Por las variables, indice y ger...
            //ciclo que recorre el padre y va copiando en el hijo los numeros que si estan acomodados
            //de ultimo si hay uno q no esta acomodado de una vez se sale y se va a buscarlo en la madre 
            //para realizar el cruce.
            for (int genP = 0; genP < dimencion; genP++)
            {
                if (tempPadre[genP] == cromosomaMeta[genP])
                {
                    tempHijo[genP] = tempPadre[genP];
                }
                else
                {
                    genBuscar = cromosomaMeta[genP];
                    indice = genP;
                    break;
                }
            }
            memoriaUtilizada += (32 * 1); //Los entereros requeridos por los for
            //ciclo q va a recorrer la madre he insertar los numeros q estan depues del gen a 
            //buscar y ademas validar q no se repitan, si llega al final de la madre y aun le hacen falta 
            //numeros entonces volverse a madre[0] y de ahi en adelante comienza a comparar si el numero
            //q esta en la madre ya existe en el hijo y sino existe entonces lo mete en el hijo.
            //genBuscar++;
            for (int genM = 0; genM < dimencion; genM++)
            {
                if (tempMadre[genM] == genBuscar)
                {
                    genIndiceM = genM;
                    break;
                }
            }
            memoriaUtilizada += (32 * 1); //Los entereros requeridos por los for
            //Copia en el hijo los numero de la madre, desde donde quedo con el padre.;
            for (int gen = indice; gen < dimencion; gen++)
            {
                //si esta en la ultima
                if (genIndiceM == (dimencion - 1))
                {
                        tempHijo[gen] = tempMadre[genIndiceM];
                        genIndiceM = 0;
                }
                else
                {
                    if (tempHijo.Contains(tempMadre[genIndiceM]))
                    {
                        gen--;
                        genIndiceM++;
                    }
                    else
                    {
                        tempHijo[gen] = tempMadre[genIndiceM];
                        genIndiceM++;
                    }
                }
            }
            memoriaUtilizada += (32 * 1); //Los entereros requeridos por los for
        }

       /// <summary>
        /// Muta el cromosoma hacia el lado derecho
        /// </summary>
        /// <param name="arregloAMutar">El cromosoma a Mutar</param>
        private void Mutacion(int[] arregloAMutar)
        {
            int indiceCero = -1;
            int temp;
            memoriaUtilizada += (32 * 2); //El entero utilizado por indiceCero,temp
            listaTemporal.Clear();
            for (int i = 0; i < dimencion; i++)
            {
                if(arregloAMutar[i] == 0)
                {
                    indiceCero = i;

                }
            }
            memoriaUtilizada += (32 * 2); //Utilizado con el for.
            Console.WriteLine("Mutacion en la madre: ");
            if (indiceCero != dimencion - 1)
            {
                temp = arregloAMutar[indiceCero + 1];
                arregloAMutar[indiceCero + 1] = arregloAMutar[indiceCero];
                arregloAMutar[indiceCero] = temp;
                Console.WriteLine("Se hizo el movimento de: {0} -> {1}", arregloAMutar[indiceCero], arregloAMutar[indiceCero + 1]);
            }
            else
            {
                temp = arregloAMutar[indiceCero - 1];
                arregloAMutar[indiceCero - 1] = arregloAMutar[indiceCero];
                arregloAMutar[indiceCero] = temp;
                Console.WriteLine("Se hizo el movimento de: {0} -> {1}", arregloAMutar[indiceCero], arregloAMutar[indiceCero - 1]);
            }
        }

       /// <summary>
        /// Muta el cromosoma, pero lo pasa a la matri, hace el movimiento del juego respetando la leyes
        /// y lo devuelve al cromosoma ya mutado.
        /// </summary>
        /// <param name="cromosomaAMutar">Lo que va a mutar.</param>
        /// <param name="tipo">Quien es el cromosoma</param>
        private void MutaciónMatriz(int[] cromosomaAMutar, string tipo)
        {
            MatrizTemporal(cromosomaAMutar);
            int y = 0;
            int x = 0;
            bool encontro0 = false;
            memoriaUtilizada += (32 * 2) + 8; //Los x,y y el bolean.
            int j;
            for (int i = 0; i < tamaño; i++)
            {
                for (j = 0; j < tamaño; j++)
                {
                    if (tablero[i, j] == 0)
                    {
                        y = i;
                        x = j;
                        encontro0 = true;
                        break;
                    }
                }
                if (encontro0 == true)
                    break;
            }
            memoriaUtilizada += (32 * 2);//Los for.
            if ((y == dimencion - 1) && (x == dimencion - 1))
            {
                return;
            }
            int temp;
            int cont;
            memoriaUtilizada += (32 * 2);
            try
            {
                temp = tablero[y, x + 1];
                tablero[y, x + 1] = tablero[y, x];
                tablero[y, x] = temp;
                Console.WriteLine("\tMutación {0}:  ({1}, {2}) -> ({3}, {4})", tipo, y, x, y, x + 1);
                cantidadMovimientos++;
                cont = 0;
                for (int i = 0; i < tamaño; i++)
                {
                    for (j = 0; j < tamaño; j++)
                    {
                        cromosomaAMutar[cont] = tablero[i, j];
                        cont++;
                    }
                }
                memoriaUtilizada += (32 * 3); //Los for y el cont.
                return;
            }
            catch (Exception)
            {

            }
            try
            {
                temp = tablero[y - 1, x];
                tablero[y - 1, x] = tablero[y, x];
                tablero[y, x] = temp;
                Console.WriteLine("\tMutación {0}:  ({1}, {2}) -> ({3}, {4})", tipo, y, x, y - 1, x);
                cantidadMovimientos++;
                cont = 0;
                for (int i = 0; i < tamaño; i++)
                {
                    for (j = 0; j < tamaño; j++)
                    {
                        cromosomaAMutar[cont] = tablero[i, j];
                        cont++;
                    }
                }
                memoriaUtilizada += (32 * 3); //Los for y el cont.
                return;
            }
            catch (Exception)
            {

            }
            cont = 0;
            for (int i = 0; i < tamaño; i++)
            {
                for (j = 0; j < tamaño; j++)
                {
                    cromosomaAMutar[cont] = tablero[i, j];
                    cont++;
                }
            }
            memoriaUtilizada += (32 * 3); //Los for y el cont.
        }

        /// <summary>
        /// Evalua cual cromosoma esta mejor adaptado al ambiente de toda la familia
        /// y lo seleciona como Padre
        /// </summary>
        private void Evaluacion()
        {
            Adaptabilidad(cromosomaPadre);
            cantidadAciertosP = cantidadAciertos;

            Adaptabilidad(cromosomaMadre);
            cantidadAciertosM = cantidadAciertos;

            Adaptabilidad(cromosomaHijo1);
            cantidadAciertosH1 = cantidadAciertos;

            Adaptabilidad(cromosomaHijo2);
            cantidadAciertosH2 = cantidadAciertos;

            if(cantidadAciertosP > cantMaxOrdenado && cantidadAciertosP != 0)
            {
                CopiarPadre(cromosomaPadre);
                cantMaxOrdenado = cantidadAciertosP;
                //return;
            }
            if (cantidadAciertosM > cantMaxOrdenado && cantidadAciertosM != 0)
            {
                CopiarPadre(cromosomaMadre);
                cantMaxOrdenado = cantidadAciertosM;
                //return;
            }
            if (cantidadAciertosH1 > cantMaxOrdenado && cantidadAciertosH1 != 0)
            {
                CopiarPadre(cromosomaHijo1);
                cantMaxOrdenado = cantidadAciertosH1;
                //return;
            }
            if (cantidadAciertosH2 > cantMaxOrdenado && cantidadAciertosH2 != 0)
            {
                CopiarPadre(cromosomaHijo2);
                cantMaxOrdenado = cantidadAciertosH2;
                //return;
            }
        }

        /// <summary>
        /// Evalua que tanta coincidencia tiene un cromosoma con el ambiente.
        /// </summary>
        /// <param name="tablero">Cromosoma a evaluar</param>
        /// <returns></returns>
        private bool Adaptabilidad(int[] tablero)
        {
            cantidadAciertos = 0;
            int contadorOrden = 1;
            memoriaUtilizada += (32 * 1);//Contador Orden.
            for (int i = 0; i < dimencion; i++)
            {
                if (tablero[i] == contadorOrden)
                {
                    cantidadAciertos++;
                }
                if ((i == dimencion - 1) && tablero[i] == 0)
                {
                    cantidadAciertos++;
                }
                else if (tablero[i] != contadorOrden)
                {
                    return false;
                }
                contadorOrden++;
            }
            memoriaUtilizada += (32 * 1);//El for.
            if (cantidadAciertos == dimencion)
            {
                encontroSolución = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Copia la respuestas correcta al Padre, es llamada por Evaluación.
        /// </summary>
        /// <param name="origen">El cromosoma que va a copiar</param>
        private void CopiarPadre(int[] origen)
        {
            for (int i = 0; i < dimencion; i++)
            {
                cromosomaPadre[i] = origen[i];
            }
            memoriaUtilizada += (32 * 1);//El for.
        }

        private void Imprimir(int[] cromosoma,string miembro)
        {
            Console.Write("\n\t\t Cromosoma->{0}: ",miembro);
            foreach (int item in cromosoma)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine("\n");
            memoriaUtilizada += (32 * 1);//El inte en el foreach.
        }

        /// <summary>
        /// Pasa el cromosoma a la matriz temporal
        /// </summary>
        /// <param name="cromosoma"></param>
        private void MatrizTemporal(int[] cromosoma)
        {
            int indice = 0;
            //memoriaUtilizada += (32 * 1);//Indice.
            int j;
            for (int i = 0; i < tamaño; i++)
            {
                for (j = 0; j < tamaño; j++)
                {
                    tablero[i, j] = cromosoma[indice];
                    indice++;
                }
            }
            //memoriaUtilizada += (32 * 2);//Los 2 for.
        }

        private void ImprimirMatriz(string tipo)
        {
            int numero;
            memoriaUtilizada += (32 * 1);//Numero.
            Console.WriteLine("\n\n\n\t {0}: ",tipo);
            int j;
            for (int i = 0; i < tamaño; i++)
            {
                Console.Write("\t\t\t  | ");
                for (j = 0; j < tamaño; j++)
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
            memoriaUtilizada += (32 * 2);//Los for
        }

        private void MemoriaUtilizada()
        {
            memoriaUtilizada += (dimencion * 7) * 32; //cantidad de enteros de las listas por 32 que pesa cada uno.
            memoriaUtilizada += (12 * 32) + 8; //12 Enteros de varibles globales.

            Console.WriteLine("\t++++++ Espacio de memoria Ram: {0} Bits\n",memoriaUtilizada);
        }

        private void ImprimirCromosomaMatriz(int[] cromosoma,string miembro)
        {
            MatrizTemporal(cromosoma);
            int numero;
            Console.WriteLine("\n\t Cromosoma-> {0}: ", miembro);
            for (int i = 0; i < tamaño; i++)
            {
                Console.Write("\t\t\t  | ");
                for (int j = 0; j < tamaño; j++)
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
