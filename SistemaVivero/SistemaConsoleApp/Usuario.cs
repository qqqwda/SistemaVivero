using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaConsoleApp
{
    public class Usuario
    {
        public string nombreUsuario;
        public string contrasenia;
        public List<Muro> muros;

        public Usuario(string Nombre, string Contrasenia)
        {
            muros = new List<Muro>();
            this.nombreUsuario = Nombre;
            this.contrasenia = Contrasenia;

        }

        public void AgregarMuro(Muro muro)
        {
            muros.Add(muro);
        }

        public void RegarTodosMuros()
        {
            foreach (var muro in this.muros)
                muro.Regar();
            
        }

        public void ListarTodosRiegos()
        {
            foreach (var muro in muros)
            {
                Console.WriteLine(string.Format("El {0} ha sido regando en:",muro.nombre));
                muro.ListarRiegos();
                Console.WriteLine("-----------------------------");
            }
        }

    }
}
