using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris_VersionChingona
{
    internal class Tetris
    {
        private Escenario escenario;
        private ConsoleKey input;
        private bool inputLeido = false;
        private int velocidad = 500;


        internal Escenario Escenario { get => escenario; set => escenario = value; }

        /// <summary>
        /// Cuando se inicializa el tetris, se crea el escenario también
        /// </summary>
        public Tetris() //Que tetris mas chingon
        {
            escenario = new Escenario();
        }

        public void jugarThread() //Bro realmente llamó jugarThread al método.
        {
            escenario.generarPieza();
            while (true)
            {
                Console.Clear();
                escenario.mostrarEscenario();
                if (escenario.comprobarCaer())
                {
                    escenario.caer();
                }
                else
                {
                    escenario.fijarPieza();
                    escenario.generarPieza();
                }

                if (escenario.comprobarLinea().Length != 0)
                {
                    escenario.quitarFilas();
                }
                Thread.Sleep(1000);
            }
        }

        //HACER MENOS PERUANO
        /// <summary>
        /// Método para iniciar el juego
        /// </summary>
        public void jugar()
        {
            /*while (true)
            {
                Thread thread = new Thread(jugarThread);
                thread.Start();
                input = Console.ReadKey().Key;
                if (escenario.comprobarMover(input)) escenario.moverPieza(input);
            }*/
            
            escenario.generarPieza();
            while (!escenario.Perder)
            {
                Console.Clear();

                escenario.mostrarEscenario();

                Console.WriteLine(escenario.Perder);
                Console.WriteLine(escenario.comprobarCaer());

                //Console.WriteLine($"{"Juan pedro".Pastel(Color.FromArgb(165, 229, 250))}");

                leerInput(velocidad);

                Console.WriteLine(input);

                if (!inputLeido) 
                {
                    if (escenario.comprobarCaer())
                    {
                        escenario.caer();
                        velocidad = 500;
                    }
                    else
                    {
                        escenario.fijarPieza();
                        escenario.generarPieza();
                        if (escenario.comprobarLinea().Length != 0)
                        {
                            escenario.quitarFilas();
                        }                        
                    }
                }
                else
                {
                    if (input == ConsoleKey.S || input == ConsoleKey.DownArrow || input == ConsoleKey.Enter)
                    {
                        if (escenario.comprobarCaer())
                        {
                            escenario.caer();
                            velocidad = 500;
                        }
                        else
                        {
                            escenario.fijarPieza();
                        }
                    }
                    else if (escenario.comprobarMover(input)) escenario.moverPieza(input);
                    
                }
                inputLeido = false;                                                       
                

                //Thread.Sleep(500);
            }
            Console.Clear();
            escenario.mostrarEscenario();
            Console.WriteLine("HAS PERDIDO");
        }

        //BORRAR 
        public void jugarPrueba()
        {
            escenario.generarPieza();
            while (true)
            {
                Console.Clear();

                escenario.mostrarEscenario();

                Console.WriteLine(escenario.comprobarCaer());


                input = Console.ReadKey().Key;

                if (input != ConsoleKey.S && input != ConsoleKey.DownArrow)
                {
                    if (escenario.comprobarMover(input))
                    {
                        escenario.moverPieza(input);
                    }
                }
                else
                {
                    if (escenario.comprobarCaer())
                    {
                        escenario.caer();
                    }
                    else
                    {
                        if (escenario.comprobarLinea().Length != 0)
                        {
                            escenario.quitarFilas();
                        }
                        escenario.fijarPieza();
                        escenario.generarPieza();
                    }

                }

                //Thread.Sleep(500);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void leerInput()
        {
            input = Console.ReadKey().Key;
            inputLeido = true;
        }

        /// <summary>
        /// Método sobrecargado que se encarga de
        /// esperar para que la pieza caiga hasta que
        /// se pase el tiempo o se reciba un input
        /// </summary>
        /// <param name="mili">Tiempo a esperar</param>
        public void leerInput(int mili)
        {
            Stopwatch cronometro = new Stopwatch();

            cronometro.Start();
            
            Thread t = new Thread(leerInput);

            t.Start();

            while (cronometro.ElapsedMilliseconds < mili && !inputLeido) { Thread.Sleep(1); }

            if (inputLeido) velocidad -= (int)cronometro.ElapsedMilliseconds;
            else velocidad = 500;

            cronometro.Stop();
        }


        public void mostrarEscenario()
        {
            escenario.mostrarEscenario();
        }

    }
}
