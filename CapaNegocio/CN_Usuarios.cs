﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios cd_Usuarios = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return cd_Usuarios.Listar();
        }
    }
}
