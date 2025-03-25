using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Marcas
    {
        private CD_Marcas cd_Marcas = new CD_Marcas();

        public List<Marca> Listar(out string Mensaje)
        {
            Mensaje = string.Empty;
            return cd_Marcas.Listar(out Mensaje);
        }

        public int Registrar(Marca marca, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || string.IsNullOrWhiteSpace(marca.Descripcion))
            {
                Mensaje = "Por favor, ingrese la descripción de la marca.";
                return resultado;
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                resultado = cd_Marcas.Registrar(marca, out Mensaje);

            }
            return resultado;
        }

        public bool Actualizar(Marca marca, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || string.IsNullOrWhiteSpace(marca.Descripcion))
            {
                Mensaje = "Por favor, ingrese la descripción de la marca.";
                return resultado;
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                resultado = cd_Marcas.Actualizar(marca, out Mensaje);
            }

            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return cd_Marcas.Eliminar(id, out Mensaje);
        }
    }
}
