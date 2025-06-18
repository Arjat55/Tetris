using System.IO;

namespace FileManager_v2.FileManagerClass
{
    internal abstract class FileManager
    {
        protected string ruta;
        protected FileStream fs;
        protected Estado estado = Estado.LEYENDO;

        public FileManager(string ruta)
        {
            this.ruta = ruta;
            fs = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void borrarArchivo()
        {
            cerrar();
            File.Delete(ruta);
        }

        public void crearArchivo()
        {
            abrir();
        }

        public virtual void cerrar()
        {
            fs.Close();
        }

        public abstract void abrir();

        public void reabrir()
        {
            cerrar();
            abrir();
        }

        public void cambiarEstado()
        {
            cerrar();

            if (estado.Equals(Estado.ESCRIBIENDO)) estado = Estado.LEYENDO;
            else estado = Estado.ESCRIBIENDO;

            abrir();
        }

        public enum Estado
        {
            ESCRIBIENDO,
            LEYENDO
        }

    }
}
