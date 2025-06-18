using System.IO;

namespace FileManager_v2.FileManagerClass
{
    internal class Binary : FileManager
    {
        private BinaryReader br;
        private BinaryWriter bw;
        private long numBytes;

        public Binary(string ruta) : base(ruta)
        {
            br = new BinaryReader(fs);
            numBytes = fs.Length;
        }

        public override void abrir()
        {
            fs = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            switch (estado)
            {
                case Estado.ESCRIBIENDO:
                    bw = new BinaryWriter(fs);
                    break;

                case Estado.LEYENDO:
                    br = new BinaryReader(fs);
                    numBytes = fs.Length;
                    break;
            }
        }

        /// <summary>
        /// Escribir en binario un número, si el estado no es ESCRIBIENDO se cambia automáticamente
        /// </summary>
        /// <param name="input">número a escribir</param>
        public void Escribir(int input)
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            bw.Write(input);
        }

        /// <summary>
        /// Escribir en binario un string, si el estado no es ESCRIBIENDO se cambia automáticamente
        /// </summary>
        /// <param name="input">string a escribir</param>
        public void Escribir(string input)
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            bw.Write(input);
        }

        /// <summary>
        /// Escribir en binario un double, si el estado no es ESCRIBIENDO se cambia automáticamente
        /// </summary>
        /// <param name="input">double a escribir</param>
        public void Escribir(double input)
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            bw.Write(input);
        }

        /// <summary>
        /// Escribir en binario un float, si el estado no es ESCRIBIENDO se cambia automáticamente
        /// </summary>
        /// <param name="input">float a escribir</param>
        public void Escribir(float input)
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            bw.Write(input);
        }

        /// <summary>
        /// Método que devuelve todos los números enteros de un fichero binario
        /// </summary>
        /// <returns>Array de todos los números enteros</returns>
        public int[] ReadAllInt()
        {
            if (!estado.Equals(Estado.LEYENDO)) cambiarEstado();

            int[] enteros = new int[numBytes / 4];

            for (int i = 0; i < enteros.Length; i++)
            {
                enteros[i] = br.ReadInt32();
            }

            return enteros;
        }

        /// <summary>
        /// Método que devuelve todos los strings de un fichero binario
        /// </summary>
        /// <returns>Array de todos los strings</returns>
        public string[] ReadAllStrings()
        {
            if (!estado.Equals(Estado.LEYENDO)) cambiarEstado();

            string[] strings = new string[contarStrings()];

            for (int i = 0; i < strings.Length; i++) strings[i] = br.ReadString();

            return strings;
        }

        /// <summary>
        /// Método que devuelve todos los double de un fichero binario
        /// </summary>
        /// <returns>Array de todos los double</returns>
        public double[] ReadAllDouble()
        {
            if (!estado.Equals(Estado.LEYENDO)) cambiarEstado();

            double[] doubles = new double[numBytes / 8];

            for (int i = 0; i < doubles.Length; i++)
            {
                doubles[i] = br.ReadDouble();
            }

            return doubles;
        }

        /// <summary>
        /// Método que devuelve todos los floats de un fichero binario
        /// </summary>
        /// <returns>Array de todos los floats</returns>
        public float[] ReadAllFloat()
        {
            if (!estado.Equals(Estado.LEYENDO)) cambiarEstado();

            float[] floats = new float[numBytes / 4];

            for (int i = 0; i < floats.Length; i++)
            {
                floats[i] = br.ReadSingle();
            }

            return floats;
        }

        /// <summary>
        /// Método que cuenta el número de strings que hay un fichero binario
        /// </summary>
        /// <returns>Número de strings del fichero</returns>
        public int contarStrings()
        {
            if (!estado.Equals(Estado.LEYENDO)) cambiarEstado();

            reabrir();

            int contador = 0;

            try
            {
                while (true)
                {
                    br.ReadString();
                    contador++;
                }
            }
            catch (EndOfStreamException)
            {
                reabrir();
                return contador;
            }

        }
    }
}