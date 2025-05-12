using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_VersionChingona
{
    internal class Escenario
    {
        private Casilla[][] escenario;
        private Pieza piezaActual;
        private int[] filasQuitar;
        private int alto;
        private int ancho;

        //cazzo putanna

        //Nada que decir sobre esta clase bastante 💤💤💤😴😴😴😴

        public Escenario()
        {
            alto = 21;
            ancho = 10;

            escenario = new Casilla[alto][];
            for (int i = escenario.Length - 1; i >= 0; i--)
            {
                escenario[i] = new Casilla[ancho];

                for (int j = escenario[i].Length -1; j >= 0 ; j--)
                {
                    escenario[i][j] = new Casilla(j, i);
                }
            }
        }

        public void mostrarEscenario()
        {
            for (int i = escenario.Length - 1; i >= 0; i--)
            {
                Console.Write("|");
                for(int j = 0; j < escenario[i].Length; j++)
                {
                    escenario[i][j].imprimirCasilla();//Console.Write(escenario[i][j]);
                }
                Console.Write("|");
                Console.WriteLine();
            }


            for (int i = 0; i < escenario.Length; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine();
        }


        public void generarPieza()
        {
            Random random = new Random();

            piezaActual = new Pieza(random.Next(7)/*4*/);
            piezaActual.generarPieza(escenario);
        }
        

        public bool comprobarMover(ConsoleKey input)
        {
            foreach (var item in piezaActual.Casillas)
            {
                switch (input)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        if (item.X == 0) return false;
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        if (item.X == ancho - 1) return false;
                        break;


                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                    case ConsoleKey.Enter:
                        if (comprobarCaer()) return true;

                        else return false;

                    case ConsoleKey.Spacebar:
                        return true;

                    case ConsoleKey.UpArrow:
                        return true;

                    default:
                        return false;
                }
            }
            return true;
        }

        public void moverPieza(ConsoleKey input)
        {

            if (input == ConsoleKey.S || input == ConsoleKey.DownArrow || input == ConsoleKey.Enter)
            {
                caer();
                return;
            }

            if (input == ConsoleKey.Spacebar)
            {
                while (comprobarCaer()) caer();
                return;
            }

            if (input == ConsoleKey.UpArrow)
            {
                girar();
                return;
            }

            foreach (var casPieza in piezaActual.Casillas)
            {
                int x = casPieza.X;
                int y = casPieza.Y;

                escenario[y][x] = new Casilla(x, y);
            }

            foreach (var casPieza in piezaActual.Casillas)
            {
                int x = casPieza.X;
                int y = casPieza.Y;

                switch (input)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:

                        escenario[y][x - 1] = casPieza;
                        casPieza.X--;
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:

                        escenario[y][x + 1] = casPieza;
                        casPieza.X++;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        break;

                    

                    default:
                        break;
                }
            }
            
        }


        public void fijarPieza()
        {
            foreach (var casPieza in piezaActual.Casillas)
            {
                casPieza.Ocupado = true;
                casPieza.Pieza = false;
            }
        }


        public bool comprobarCaer()
        {
            foreach (var casPieza in piezaActual.Casillas)
            {   //Por cada casilla de la pieza se comprueba si la casilla de abajo está ocupada

                if (casPieza.Y == 0)
                {
                    return false;
                }
                else 
                { 
                    Casilla casillaAbajo = escenario[casPieza.Y - 1][casPieza.X];
                    if (casillaAbajo.Ocupado) return false;
                }

            }

            return true;
        }


        public void caer()
        {
            foreach (var casPieza in piezaActual.Casillas)
            {
                int x = casPieza.X;
                int y = casPieza.Y;

                escenario[y][x] = new Casilla(x, y);                
            }

            foreach (var casPieza in piezaActual.Casillas)
            {
                int x = casPieza.X;
                int y = casPieza.Y;

                escenario[y - 1][x] = casPieza;
                casPieza.Y--;
            }
        }


        /*
        /// <summary>
        /// Devuelve un array con las posiciones de las filas que hay que eliminar
        /// </summary>
        public int[] comprobarLinea()
        {
            int ancho = escenario[0].Length;
            int contador = 0;
            int numFilas = 0;
            int pos = 0;

            foreach (var item in escenario)
            {
                
                foreach (var item1 in item)
                {
                    if (item1.Ocupado) contador++;                    
                }

                if (contador == ancho) numFilas++;

                contador = 0;
            }



            if (numFilas == 0) return new int[0];
            
            int[] filas = new int[numFilas];




            for (int i = 0; i < escenario.Length; i++)
            {

                for (int j = 0; j < escenario[i].Length; j++)
                {
                    if (escenario[i][j].Ocupado) contador++;
                }

                if (contador == ancho) filas[pos] = i;                 

                contador = 0;
            }

            filasQuitar = filas;

            return filas;
        }*/


        /// <summary>
        /// Devuelve un array con las posiciones de las filas que hay que eliminar
        /// </summary>
        public int[] comprobarLinea()
        {
            int ancho = escenario[0].Length;
            int contador = 0;
            int numFilas = 0;
            int pos = 0;

            foreach (var item in escenario)
            {

                foreach (var item1 in item)
                {
                    if (item1.Ocupado) contador++;
                }

                if (contador == ancho) numFilas++;

                contador = 0;
            }



            if (numFilas == 0) return new int[0];

            int[] filas = new int[numFilas];




            for (int i = 0; i < escenario.Length; i++)
            {

                for (int j = 0; j < escenario[i].Length; j++)
                {
                    if (escenario[i][j].Ocupado) contador++;
                }

                if (contador == ancho) 
                { 
                    filas[pos] = i; 
                    pos++;
                }

                contador = 0;
            }

            filasQuitar = filas;

            return filas;
        }


        public void quitarFilas()
        {
            int contador = 0;

            foreach (var numFila in filasQuitar)
            {
                Casilla[] fila = escenario[numFila - contador];

                for (int i = 0; i < fila.Length; i++) 
                {
                    fila[i].Ocupado = false;
                    fila[i].Y = escenario.Length - 1;
                }
                
                for (int i = numFila - contador; i < escenario.Length - 1; i++)
                {
                    Casilla[] filaAnterior = escenario[i + 1];

                    foreach (var item in filaAnterior) item.Y--;

                    escenario[i] = filaAnterior;
                }

                escenario[escenario.Length - 1] = fila;
                contador++;
            }
        }

        public void girar()
        {
            piezaActual.rotar(escenario);
        }
    }
}
