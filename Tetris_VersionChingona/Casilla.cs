using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_VersionChingona
{
    internal class Casilla
    {
        private bool ocupado = false;
        private bool pieza = false;
        private ConsoleColor color;
        private int x;
        private int y;

        public bool Pieza { get => pieza; set => pieza = value; }
        public ConsoleColor Color { get => color; set => color = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public bool Ocupado { get => ocupado; set => ocupado = value; }

        public Casilla() { }


        public Casilla(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    

        public override string ToString() //este toString va duro de pelotas
        {
            if (!pieza) return " ";                                       
            else return "1";
        }


        public void imprimirCasilla()
        {
            if (Pieza || ocupado)
            {
                Console.ForegroundColor = color;
                Console.Write("▓▓"/*"▒"*/);
                Console.ResetColor();
            }
            else
            {
                Console.Write("  "); //ARTURO ES UN PERUANITO 
            }
        }
    }
}
