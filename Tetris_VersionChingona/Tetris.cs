using FileManager_v2.FileManagerClass;
using System;
using System.Diagnostics;
using System.Threading;

namespace Tetris_VersionChingona
{
    internal class Tetris
    {
        private Escenario escenario;
        private ConsoleKey input;
        private Thread t;
        private bool inputLeido = false;
        private int velocidad = 500;
        private int puntuacion = 0;
        int[] puntuaciones = new int[10];
        private string[] lineasFile = new string[10];
        private Character puntuacionesFile = new Character(@"puntuaciones.txt");

        internal Escenario Escenario { get => escenario; set => escenario = value; }

        /// <summary>
        /// Cuando se inicializa el tetris, se crea el escenario también
        /// </summary>
        public Tetris() //Que tetris mas chingon
        {
            escenario = new Escenario();

            for (int i = lineasFile.Length - 1; i >= 0; i--) lineasFile[i] = puntuacionesFile.leerLinea();

            for (int i = puntuaciones.Length - 1; i >= 0; i--) puntuaciones[i] = int.Parse(lineasFile[i].Substring(0, (lineasFile[i]).IndexOf(' ')));

            puntuacionesFile.reabrir();
        }

        public void iniciar()
        {
            mostrarPuntuaciones();

            Console.WriteLine("Pulsa cualquier tecla para jugar: ");
            Console.ReadKey();

            jugar();

            while (!inputLeido) { }

            t.Abort();

            if (comprobarPuntuacion())
            {
                Console.WriteLine("Tu puntuación está dentro del top 10, quieres registrar tu puntuación? (s/n): ");
                ConsoleKey decision = Console.ReadKey(true).Key;
                
                if (decision.Equals(ConsoleKey.S))
                {
                    Console.WriteLine("Introduce tu nombre: ");

                    string nombre = Console.ReadLine();

                    foreach (var item in getPosiciones()) lineasFile[item] = puntuacion + " " + nombre;

                    puntuacionesFile.borrarArchivo();

                    for (int i = lineasFile.Length - 1; i >= 0; i--) puntuacionesFile.EscribirLinea(lineasFile[i]);

                    mostrarPuntuaciones();

                    puntuacionesFile.cerrar();
                }
            }
        }

        /// <summary>
        /// Método para iniciar el juego
        /// </summary>
        public void jugar()
        {
            t = new Thread(leerInput);
            t.Start();

            escenario.generarPieza();
            while (!escenario.Perder)
            {
                Console.Clear();

                escenario.mostrarEscenario();
                Console.WriteLine("Puntuación: " + puntuacion);

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

                        puntuacion += 32;

                        escenario.generarPieza();

                        int numFilas = 0;
                        if ((numFilas = escenario.comprobarLinea().Length) != 0)
                        {
                            escenario.quitarFilas();
                            puntuacion += (200 * numFilas);
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
                            puntuacion += 32;

                            int numFilas = 0;
                            if ((numFilas = escenario.comprobarLinea().Length) != 0)
                            {
                                escenario.quitarFilas();
                                puntuacion += (200 * numFilas);
                            }
                        }
                    }
                    else if (escenario.comprobarMover(input)) escenario.moverPieza(input);

                }
                inputLeido = false;


                //Thread.Sleep(500);
            }
            Console.Clear();
            escenario.mostrarEscenario();
            Console.WriteLine("HAS PERDIDO  puntuación: " + puntuacion);
            //t.Abort();

            inputLeido = false;

            Console.WriteLine("Pulsa cualquier tecla para continuar: ");
        }

        /// <summary>
        /// 
        /// </summary>
        public void leerInput()
        {
            while (!escenario.Perder)
            {
                input = Console.ReadKey(true).Key;
                inputLeido = true;
            }
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

            while (cronometro.ElapsedMilliseconds < mili && !inputLeido) { }

            //t.Abort(true);

            Console.WriteLine((int)cronometro.ElapsedMilliseconds);

            if (inputLeido) velocidad -= (int)cronometro.ElapsedMilliseconds;
            else velocidad = 500;

            cronometro.Stop();
        }


        public void mostrarPuntuaciones()
        {
            Console.WriteLine("PUNTUACIONES: ");

            puntuacionesFile.reabrir();

            for (int i = puntuaciones.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(puntuaciones.Length - i + ". " + puntuaciones[i] + " puntos hechos por " + puntuacionesFile.leerLinea().Substring((lineasFile[i]).IndexOf(' ') + 1));
            }
        }

        public bool comprobarPuntuacion()
        {
            foreach (var item in puntuaciones) if (puntuacion >= item) return true;

            return false;
        }

        public int[] getPosiciones()
        {
            int contador = 0;

            foreach (var item in puntuaciones) if (puntuacion >= item) contador++;

            int[] posiciones = new int[contador];

            contador = 0;

            for (int i = puntuaciones.Length - 1; i >= 0; i--)
            {
                if (puntuacion >= puntuaciones[i])
                {
                    posiciones[contador] = i;

                    contador++;
                }
            }

            return posiciones;
        }
    }
}