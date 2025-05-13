using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_VersionChingona
{
    internal class Pieza
    {
        private Casilla[] casillas;
        private Casilla medio;
        private TipoPieza tipoPieza;
        private int altura;
        private int mitadX;


        internal Casilla[] Casillas { get => casillas; set => casillas = value; }


        public Pieza(int pieza) //Pieza bien chingona ésta de aquí
        {
            switch (pieza)
            {
                case 0:
                    tipoPieza = TipoPieza.Z; //Alv que pieza mas chingona la Z
                    break;

                case 1:
                    tipoPieza = TipoPieza.Z_INVERTIDA; //Esta no es tan chingona está _INVERTIDA
                    break;

                case 2:
                    tipoPieza = TipoPieza.L;
                    break;

                case 3:
                    tipoPieza = TipoPieza.L_INVERTIDA;
                    break;

                case 4:
                    tipoPieza = TipoPieza.T;
                    break;

                case 5:
                    tipoPieza = TipoPieza.CUBO; //Vaya mierda la pelicula del minecraft
                    break;

                case 6:
                    tipoPieza = TipoPieza.I;
                    break;
            }
        }


        public void generarPieza(Casilla[][] escenario) //Bro realmente hizo un método GenerarPieza() 💀💀💀
        {
            ConsoleColor color = ConsoleColor.White;
            altura = escenario.Length - 1;
            mitadX = (escenario[0].Length / 2) - 1; 

            switch (tipoPieza)
            {
                case TipoPieza.L:

                    casillas = new[] {
                        escenario[altura][mitadX + 1],
                        escenario[altura - 1][mitadX - 1],
                        escenario[altura - 1][mitadX],
                        escenario[altura - 1][mitadX + 1]
                    };

                    medio = casillas[2];
                    color = ConsoleColor.DarkYellow;
                    break;

                case TipoPieza.L_INVERTIDA:

                    casillas = new[] {
                        escenario[altura][mitadX - 1],
                        escenario[altura - 1][mitadX - 1],
                        escenario[altura - 1][mitadX],
                        escenario[altura - 1][mitadX + 1],
                    };

                    medio = casillas[2];
                    color = ConsoleColor.Cyan;
                    break;

                case TipoPieza.Z_INVERTIDA:

                    casillas = new[] {
                        escenario[altura][mitadX + 1],
                        escenario[altura][mitadX],
                        escenario[altura - 1][mitadX],
                        escenario[altura - 1][mitadX - 1]
                    };

                    medio = casillas[1];
                    color = ConsoleColor.Green;
                    break;
                case TipoPieza.Z:

                    casillas = new[] {
                        escenario[altura][mitadX - 1],
                        escenario[altura][mitadX],
                        escenario[altura - 1][mitadX],
                        escenario[altura - 1][mitadX + 1]
                    };

                    medio = casillas[1];
                    color = ConsoleColor.Red;
                    break;

                case TipoPieza.CUBO: //MAINCRA

                    casillas = new[] { 
                        escenario[altura][mitadX],
                        escenario[altura][mitadX + 1],
                        escenario[altura - 1][mitadX],
                        escenario[altura - 1][mitadX + 1]
                    }; //array peruanito

                    //Asigno que cada color de la casilla y le doy el estado de pieza a las casillas
                    //buena pieza pero en mi humilde opinion deberías cambiar los colores y hacerlos mas llamativos 
                    //porque tengo 0.1 dioptrias y la verdad que creo que soy un poco daltonico porque veo el cyan y el verde igual
                    //arturo porfavor cambia los colores te lo suplico
                                        
                    color = ConsoleColor.Yellow;
                    break;

                case TipoPieza.I:

                    casillas = new[] {
                        escenario[altura][mitadX - 1],
                        escenario[altura][mitadX],
                        escenario[altura][mitadX + 1],
                        escenario[altura][mitadX + 2]
                    };

                    medio = casillas[1];
                    color = ConsoleColor.Blue;
                    break;

                case TipoPieza.T:

                    casillas = new[] {
                        escenario[altura - 1][mitadX - 1],
                        escenario[altura - 1][mitadX],
                        escenario[altura][mitadX],
                        escenario[altura - 1][mitadX + 1]                        
                    };

                    medio = casillas[1];
                    color = ConsoleColor.Magenta;
                    break;
            }

            foreach (var item in casillas)
            {
                item.Pieza = true;
                item.Color = color;
            }

        }       

        public void rotar(Casilla[][] escenario) // métele try catch pa rotar en los bordes yya
        {
            if (tipoPieza == TipoPieza.CUBO) return;

            foreach (var item in casillas)
            {
                int x = item.X;
                int y = item.Y;
                escenario[y][x] = new Casilla(x, y);
            }

            int XMedio = medio.X;
            int YMedio = medio.Y;

            int contador = 0;
            Casilla[] posInicial = new Casilla[3];

            foreach (var item in casillas)
            {
                int operacion = 0;


                if (item != medio)
                {
                    int x = item.X;
                    int y = item.Y;
                    posInicial[contador] = new Casilla(x, y);

                    if (item.Y == YMedio)
                    {
                        item.Y += (XMedio - x);
                        item.X = XMedio;
                        contador++;

                    }
                    else if (item.X == XMedio)
                    {
                        item.X += (item.Y - YMedio);
                        item.Y = YMedio;
                        contador++;
                    }
                    else
                    {   //calcular el cuadrante en el que se encuentra (soy gay) la casilla respecto a la casilla del centro y actuar respecto a eso                        
                        
                        int disX = XMedio - x;
                        int disY = YMedio - y;
                        Console.WriteLine(disX + " " + disY);
                        

                        if (disX < 0 && disY < 0)
                        {
                            Console.WriteLine("1");
                            
                            item.Y += disY * 2;
                        }
                        else if (disX < 0 && disY > 0)
                        {
                            Console.WriteLine("2");
                            
                            item.X += disX * 2;
                        }
                        else if (disX > 0 && disY < 0)
                        {
                            Console.WriteLine("3");
                            
                            item.X += disX * 2;
                        }
                        else if (disX > 0 && disY > 0)
                        {
                            Console.WriteLine("4");
                            
                            item.Y += disY * 2;
                        }
                        Console.WriteLine("Operaciones " + operacion +" ");
                        Console.WriteLine("Antes: " + x + ", " + y);
                        Console.WriteLine("Despues: "+item.X + " " + item.Y);
                        contador++;
                    }

                }
                escenario[item.Y][item.X] = item;

            }

            Console.WriteLine(contador);
            

        }

        public void comprobarRotar(Casilla[][] escenario) // falta try-catch
        {
            foreach (var item in casillas)
            {
                int x = item.X;
                int y = item.Y;

                if (y >= escenario.Length || y < 0)
                {
                    if (y >= escenario.Length)
                    {
                        if (true)
                        {
                            
                        }
                    }
                }
            }
        }


        public enum TipoPieza //Bro hizo el enum en minuscula 💀💀💀🤣🤣🤣😭😭😭
        {
            L,
            L_INVERTIDA,
            Z,
            Z_INVERTIDA,
            T,
            I,
            CUBO
        }

        public enum Movimiento //duro este enum la verdad 
        { 
            DERECHA,
            IZQUIERDA,
            ABAJO
        }
    }
}
