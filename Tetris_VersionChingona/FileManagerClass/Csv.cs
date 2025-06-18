namespace FileManager_v2.FileManagerClass
{
    internal class Csv : Character
    {
        private char separador = ';';

        public char Separador { get => separador; set => separador = value; }

        public Csv(string ruta) : base(ruta) { }

        public string[] leerFilaCsv()
        {
            if (!estado.Equals(Estado.LEYENDO)) cambiarEstado();

            return sr.ReadLine().Split(separador);
        }

        public string[][] readAllCsv()
        {
            if (!estado.Equals(Estado.LEYENDO)) cambiarEstado();

            string[][] csv = new string[contarFilas()][];

            for (int i = 0; i < csv.Length; i++)
            {
                csv[i] = leerFilaCsv();
            }

            return csv;
        }

        public void EscribirTodoCsv(string[] lineas)
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            for (int i = 0; i < lineas.Length; i++) sw.WriteLine(lineas[i]);            
        }


        public void EscribirFilaCsv(string[] tokens)
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            sw.Write(tokens[0]);

            for (int i = 1; i < tokens.Length; i++) sw.Write(separador + tokens[i]);

            cambiarLinea();
        }

        /// <summary>
        /// Escribir texto pasado por parámetro y añada el separador (por defecto ;) al final del texo
        /// Tener en cuenta antes de pasar de línea
        /// </summary>
        /// <param name="column"></param>
        public void EscribirColumna(string column)
        {
            if (!estado.Equals(Estado.ESCRIBIENDO)) cambiarEstado();

            sw.Write(column + separador);
        }

    }
}