using Pastel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Pastel;
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
            while (true)
            {
                Console.Clear();

                escenario.mostrarEscenario();

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
                    }
                    else if (escenario.comprobarMover(input)) escenario.moverPieza(input);
                    
                }
                inputLeido = false;                                                       
                

                //Thread.Sleep(500);
            }
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


        public void leerInput()
        {
            input = Console.ReadKey().Key;
            inputLeido = true;
        }


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
