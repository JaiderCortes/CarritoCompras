using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using CapaNegocio;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios cd_Usuarios = new CD_Usuarios();

        public List<Usuario> Listar(out string Mensaje)
        {
            Mensaje = string.Empty;
            return cd_Usuarios.Listar(out Mensaje);
        }

        public int Registrar(Usuario usuario, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Nombres) || string.IsNullOrWhiteSpace(usuario.Nombres))
            {
                Mensaje = "Por favor, ingrese el nombre del usuario.";
                return resultado;
            }
            if (string.IsNullOrEmpty(usuario.Apellidos) || string.IsNullOrWhiteSpace(usuario.Apellidos))
            {
                Mensaje = "Por favor, ingrese el apellido del usuario.";
                return resultado;
            }
            if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                Mensaje = "Por favor, ingrese el correo del usuario.";
                return resultado;
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = CN_Recursos.GenerarClave();

                string asunto = "Creación de usuario | Carrito de compras";
                string mensaje = $@"
                <div style=""max-width: 600px; background: #ffffff; padding: 20px; border-radius: 5px; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1); text-align: center; font-family: Arial, sans-serif;"">
                    <p style=""font-size: 16px; color: #333;"">Hola,</p>
                    <p style=""font-size: 16px; color: #333;"">Se ha generado una nueva contraseña para tu cuenta. Te recomendamos cambiarla después de iniciar sesión.</p>
                    <p style=""font-size: 16px; color: #333;"">Tu nueva contraseña es:</p>
                    <p style=""font-size: 18px; font-weight: bold; color: #d9534f;"">{clave}</p>
                    <p style=""font-size: 16px; color: #333;"">Si no solicitaste esta contraseña, por favor ignora este mensaje o contacta con el soporte.</p>
                    <p style=""margin-top: 20px; font-size: 14px; color: #777;"">&copy; 2025 Carrito de compras. Todos los derechos reservados.</p>
                </div> ";

                usuario.Clave = CN_Recursos.ConvertirSHA256(clave);
                resultado = cd_Usuarios.Registrar(usuario, out Mensaje);
                if (resultado != 0)
                {
                    bool respuesta = CN_Recursos.EnviarCorreo(usuario.Correo, asunto, mensaje, out Mensaje);
                    if (!respuesta)
                    {
                        Mensaje = $"Ocurrió un error al enviar el correo al usuario con la contraseña autogenerada. {Mensaje}";
                        return 0;
                    }
                }
                else
                {
                    return resultado;
                }
            }
            return resultado;
        }

        public bool Actualizar(Usuario usuario, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Nombres) || string.IsNullOrWhiteSpace(usuario.Nombres))
            {
                Mensaje = "Por favor, ingrese el nombre del usuario.";
                return resultado;
            }
            if (string.IsNullOrEmpty(usuario.Apellidos) || string.IsNullOrWhiteSpace(usuario.Apellidos))
            {
                Mensaje = "Por favor, ingrese el apellido del usuario.";
                return resultado;
            }
            if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                Mensaje = "Por favor, ingrese el correo del usuario.";
                return resultado;
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                resultado = cd_Usuarios.Actualizar(usuario, out Mensaje);
            }

            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return cd_Usuarios.Eliminar(id, out Mensaje);
        }
    }
}
