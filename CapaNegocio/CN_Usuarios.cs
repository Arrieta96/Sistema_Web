using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


//Agregamos
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrEmpty(obj.Nombres))
            {
                Mensaje = "El Nombre del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrEmpty(obj.Apellidos))
            {
                Mensaje = "El Apellido del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrEmpty(obj.Correo))
            {
                Mensaje = "El Correo del usuario no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();

                string asunto = "Creaccion de Cuenta";
                string mensaje_correo = "<h3>Su cuenta fue creada satisfactoriamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);


                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }

            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrEmpty(obj.Nombres))
            {
                Mensaje = "El Nombre del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrEmpty(obj.Apellidos))
            {
                Mensaje = "El Apellido del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrEmpty(obj.Correo))
            {
                Mensaje = "El Correo del usuario no puede estar vacio";
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

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }


        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {

            return objCapaDato.CambiarClave(idusuario, nuevaclave, out Mensaje);
        }


        public bool ReestablecerClave(int idusuario, string correo, out string Mensaje)
        {

            Mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idusuario, CN_Recursos.ConvertirSha256(nuevaclave), out Mensaje);

            if (resultado)
            {

                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);


                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {

                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }

            }
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";

                return false;
            }


        }



    }
}
