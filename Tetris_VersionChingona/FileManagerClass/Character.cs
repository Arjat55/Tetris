using System;
using System.IO;

namespace FileManager_v2.FileManagerClass
{
    internal class Character : FileManager
    {
        protected StreamWriter sw;
        protected StreamReader sr;    

        public Character(string ruta) : base(ruta) 
        {
            sr = new StreamReader(fs);
        }

        public override void abrir()
        {
            fs = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            switch (estado)
            {
                case Estado.ESCRIBIENDO:
                    sw = new StreamWriter(fs);
                    break;

                case Estado.LEYENDO:
                    sr = new StreamReader(fs);
                    break;
            }
        }

        public override void cerrar()
        {
            switch (estado)
            {
                case Estado.ESCRIBIENDO:
                    sw.Close();
                    break;

                case Estado.LEYENDO:
                    sr.Close();
                    break;
            }

            fs.Close();
        }

        public int contarFilas()
        {
            if (!estado.Equals(Estado.LEYENDO)) cambiarEstado();

            reabrir();

            int contador = 0;

            while (sr.ReadLine() != null) contador++;

            reabrir();

            return contador;
        }

        public string leerLinea()
        {
            if (!estado.Equals(Estado.LEYENDO)) cambiarEstado();

            return sr.ReadLine();
        }

        public void cambiarLinea()
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            sw.Write("\n");
        }

        public void EscribirLinea(string linea)
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            sw.WriteLine(linea);
        }

        public void Escribir(string texto)
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            sw.Write(texto);
        }
    }
}