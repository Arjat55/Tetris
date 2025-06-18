using System;

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
        internal TipoPieza TipoPieza1 { get => tipoPieza; set => tipoPieza = value; }

        public Pieza(int pieza) //Pieza bien chingona ésta de aquí
        {
            switch (pieza)
            {
                case 0: tipoPieza = TipoPieza.Z; //Alv que pieza mas chingona la Z
                    break;

                case 1: tipoPieza = TipoPieza.Z_INVERTIDA; //Esta no es tan chingona está _INVERTIDA
                    break;

                case 2: tipoPieza = TipoPieza.L;
                    break;

                case 3: tipoPieza = TipoPieza.L_INVERTIDA;
                    break;

                case 4: tipoPieza = TipoPieza.T;
                    break;

                case 5: tipoPieza = TipoPieza.CUBO; //Vaya mierda la pelicula del minecraft
                    break;

                case 6: tipoPieza = TipoPieza.I;
                    break;
            }
        }

        public Pieza(TipoPieza tipoPieza)
        {
            this.tipoPieza = tipoPieza;
        }


        public bool generarPieza(Casilla[][] escenario) //Bro realmente hizo un método GenerarPieza() 💀💀💀
        {
            bool perder = true;
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
                if (item.Ocupado) perder = false;
                item.Pieza = true;
                item.Color = color;
            }            
            return perder;
        }
        
        public void generarPieza(TipoPieza tipo, Casilla[][] siguientes, int pos)
        {
            int alturaS = 0;
            int mitadS = (siguientes[0].Length / 2)/* - 1*/;
            Casilla[] casillasS = new Casilla[4];
            ConsoleColor colorS = ConsoleColor.White;

            switch (pos)
            {
                case 0: alturaS = siguientes.Length - 1;
                    break;

                case 1: alturaS = siguientes.Length - 4;
                    break;

                case 2: alturaS = siguientes.Length - 7;
                    break;
            }
            Console.WriteLine(alturaS + " " + mitadS);
            switch (tipo)
            {
                case TipoPieza.L:

                    casillasS = new[] {
                        siguientes[alturaS][mitadS + 1],
                        siguientes[alturaS - 1][mitadS - 1],
                        siguientes[alturaS - 1][mitadS],
                        siguientes[alturaS - 1][mitadS + 1]
                    };                    
                    colorS = ConsoleColor.DarkYellow;
                    break;

                case TipoPieza.L_INVERTIDA:

                    casillasS = new[] {
                        siguientes[alturaS][mitadS - 1],
                        siguientes[alturaS - 1][mitadS - 1],
                        siguientes[alturaS - 1][mitadS],
                        siguientes[alturaS - 1][mitadS + 1],
                    };                    
                    colorS = ConsoleColor.Cyan;
                    break;

                case TipoPieza.Z_INVERTIDA:

                    Console.WriteLine(siguientes[alturaS][mitadS]);
                    casillasS = new[] {
                        siguientes[alturaS][mitadS + 1],
                        siguientes[alturaS][mitadS],
                        siguientes[alturaS - 1][mitadS],
                        siguientes[alturaS - 1][mitadS - 1]
                    };                    
                    colorS = ConsoleColor.Green;
                    break;
                case TipoPieza.Z:

                    casillasS = new[] {
                        siguientes[alturaS][mitadS - 1],
                        siguientes[alturaS][mitadS],
                        siguientes[alturaS - 1][mitadS],
                        siguientes[alturaS - 1][mitadS + 1]
                    };                    
                    colorS = ConsoleColor.Red;
                    break;

                case TipoPieza.CUBO:

                    casillasS = new[] {
                        siguientes[alturaS][mitadS ],
                        siguientes[alturaS][mitadS + 1],
                        siguientes[alturaS - 1][mitadS],
                        siguientes[alturaS - 1][mitadS + 1]
                    };
                    colorS = ConsoleColor.Yellow;
                    break;

                case TipoPieza.I:

                    casillasS = new[] {
                        siguientes[alturaS][mitadS - 1],
                        siguientes[alturaS][mitadS],
                        siguientes[alturaS][mitadS + 1],
                        siguientes[alturaS][mitadS + 2]
                    };                    
                    colorS = ConsoleColor.Blue;
                    break;

                case TipoPieza.T:

                    casillasS = new[] {
                        siguientes[alturaS - 1][mitadS - 1],
                        siguientes[alturaS - 1][mitadS],
                        siguientes[alturaS][mitadS],
                        siguientes[alturaS - 1][mitadS + 1]
                    };                    
                    colorS = ConsoleColor.Magenta;
                    break;
            }

            foreach (var item in casillasS)
            {
                item.Pieza = true;
                item.Color = colorS;
            }
        }

        public void rotar(Casilla[][] escenario) // métele try catch pa rotar en los bordes yya
        {
            if (tipoPieza == TipoPieza.CUBO) return;

            int contador = 0;

            Casilla[] posInicial = new Casilla[4];

            foreach (var item in casillas)
            {
                int x = item.X;
                int y = item.Y;
                escenario[y][x] = new Casilla(x, y);
                posInicial[contador] = new Casilla(x, y);
                contador++;
            }

            int XMedio = medio.X;
            int YMedio = medio.Y;

            
            

            foreach (var item in casillas)
            {
                int operacion = 0;


                if (item != medio)
                {
                    int x = item.X;
                    int y = item.Y;                    

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
                

            }
            Console.WriteLine("La altura del escenario es: " + (escenario.Length - 1) + " y el ancho es: " + (escenario[1].Length - 1));
            comprobarRotar(escenario.Length - 1, escenario[1].Length - 1);

            if (!comprobarRotar(escenario))
            {
                contador = 0;
                for (int i = 0; i < casillas.Length; i++)
                {                    
                    casillas[i].X = posInicial[contador].X;
                    casillas[i].Y = posInicial[contador].Y;
                    contador++;                    
                }

            }


            foreach (var item in casillas)
            {
                escenario[item.Y][item.X] = item;
            }

        }

        public void comprobarRotar(int altura, int ancho) // falta try-catch
        {
            int distanciaX = 0;
            int distanciaY = 0;
            int opcionX = 0;
            int opcionY = 0;

            foreach (var item in casillas)
            {
                int distancia = 0;
                int x = item.X;
                int y = item.Y;

                if (altura < y)
                {
                    distancia = y - altura;
                    if (distancia >= distanciaY) distanciaY = distancia;
                    opcionY = 1;
                }
                else if (y < 0)
                {
                    distancia = 0 - y;
                    if (distancia >= distanciaY) distanciaY = distancia;
                    opcionX = 2;
                }
                if (ancho < x)
                {
                    distancia = x - ancho;
                    if(distancia >= distanciaX) distanciaX = distancia;
                    opcionX = 1;
                }
                else if (x < 0)
                {
                    distancia = 0 - x;
                    if (distancia >= distanciaX) distanciaX = distancia;
                    opcionX = 2;
                }
                
            }

            Console.WriteLine("Distancias: " + distanciaX + " " + distanciaY);
            if (distanciaX == 0 && distanciaY == 0) return;

            for (int i = 0; i < distanciaY; i++)
            {
                if (opcionY == 1) bajar();
                else subir();
            }

            for (int i = 0; i < distanciaX; i++)
            {
                if (opcionX == 1) moverIzquierda();
                else moverDerecha();
            }
        }

        public bool comprobarRotar(Casilla[][] escenario)
        {
            Console.WriteLine("y inicial = " + medio.Y + ", x inicial = " + medio.X);
            if (comprobarEstado(escenario)) return true; 

            moverDerecha();

            if (comprobarEstado(escenario)) return true; 

            moverIzquierda();
            moverIzquierda();

            if (comprobarEstado(escenario)) return true; 

            moverDerecha();
            subir();

            if (comprobarEstado(escenario)) return true; 

            moverDerecha();

            if (comprobarEstado(escenario)) return true; 

            moverIzquierda();
            moverIzquierda();

            if (comprobarEstado(escenario)) return true; 

            moverDerecha();
            bajar();
            bajar();

            if (comprobarEstado(escenario)) return true; 

            moverDerecha();

            if (comprobarEstado(escenario)) return true; 

            moverIzquierda();
            moverIzquierda();

            if (comprobarEstado(escenario)) return true;

            moverDerecha();
            subir();
            Console.WriteLine("y final = " + medio.Y + ", x final = " + medio.X);
            return false;
        }

        public bool comprobarEstado(Casilla[][] escenario)
        {
            int contador = 0;

            try
            {
                foreach (var item in casillas)
                {
                    if (!escenario[item.Y][item.X].Ocupado) contador++;
                }
            }
            catch (Exception) { return false; }
            if (contador == 4) return true;
            else return false;
        }

        public void bajar()
        {
            foreach (var casilla in casillas)
            {
                casilla.Y--;
            }
        }

        public void subir()
        {
            foreach (var casilla in casillas)
            {
                casilla.Y++;
            }
        }

        public void moverDerecha()
        {
            foreach (var casilla in casillas)
            {
                casilla.X++;
            }
        }

        public void moverIzquierda()
        {
            foreach (var casilla in casillas)
            {
                casilla.X--;
            }
        }

        public void desaparecer()
        {
            foreach (var casilla in casillas) casilla.Pieza = false;
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
