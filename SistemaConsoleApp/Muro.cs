using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SistemaConsoleApp
{
    

    public class Muro
    {
        public String nombre;
        public String sector;
        public EClima clima;
        public DateTime dia;
        public int hora;
        public bool estaRegando;
        public List<DateTime> listaRiegos;

        public Muro(String Nombre, String Sector, EClima Clima)
        {
            this.nombre = Nombre;
            this.sector = Sector;
            this.clima = Clima;
            this.estaRegando = false;
            this.clima = Clima;
            this.listaRiegos = new List<DateTime>();
            
        }

        public override String ToString()
        {
            return string.Format("Nombre:{0} | Sector:{1} | Clima: {2} | Dia: {3} | Hora: {4} | EstaRegando: {5}", this.nombre,
                this.sector, this.clima, this.dia, this.hora,this.estaRegando);

        }

        public void Actualizar()
        {
            this.dia = DateTime.UtcNow;
            //this.hora = DateTime.UtcNow.Hour;
            this.hora = 13;
            if (this.clima == EClima.Soleado | this.hora >= 12 && this.hora <= 13)
            {
                Regar();
            }
            else
            {
                this.estaRegando = false;
            }
        }

        public void Regar()
        {
            Console.WriteLine(string.Format("Ha empezado a regar {0}!", this.nombre));
            this.estaRegando = true;
            Console.WriteLine("Está regando...");
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
            this.dia = DateTime.UtcNow;
            listaRiegos.Add(this.dia);
            DejarDeRegar();
        }


        public void DejarDeRegar()
        {
            Console.WriteLine(string.Format("Se ha dejado de regar {0}", this.nombre));
            this.estaRegando = false;
            Console.WriteLine();
        }

        public void ListarRiegos()
        {
            foreach (var riego in listaRiegos)
            {
                Console.WriteLine(riego.ToString());
            }
        }
    }
}
