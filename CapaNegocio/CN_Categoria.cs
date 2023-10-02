using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Agregamos
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria
    {

        private CD_Categoria objCapaDato = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrEmpty(obj.Descripcion))
            {
                Mensaje = "La Descripcion de la Categoria no puede estar en blanco";
            }
            

            if (string.IsNullOrEmpty(Mensaje))
            {
                
                return objCapaDato.Registrar(obj, out Mensaje);            

            }
            else
            {
                return 0;
            }

        }

        // ---

        public bool Editar(Categoria obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrEmpty(obj.Descripcion))
            {
                Mensaje = "La Descripcion de la Categoria no puede estar en blanco";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Editar(obj, out Mensaje);

            }
            else
            {
                return false;
            }

        }

        // ---

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

    }
}
