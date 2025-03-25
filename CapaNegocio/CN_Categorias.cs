using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Categorias
    {
        private CD_Categorias cd_Categorias = new CD_Categorias();

        public List<Categoria> Listar(out string Mensaje)
        {
            Mensaje = string.Empty;
            return cd_Categorias.Listar(out Mensaje);
        }

        public int Registrar(Categoria categoria, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
            {
                Mensaje = "Por favor, ingrese la descripción de la categoría.";
                return resultado;
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                resultado = cd_Categorias.Registrar(categoria, out Mensaje);

            }
            return resultado;
        }

        public bool Actualizar(Categoria categoria, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
            {
                Mensaje = "Por favor, ingrese la descripción de la categoría.";
                return resultado;
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                resultado = cd_Categorias.Actualizar(categoria, out Mensaje);
            }

            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return cd_Categorias.Eliminar(id, out Mensaje);
        }

    }
}
