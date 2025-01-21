using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    string sql = "SELECT IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo FROM Usuario;";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader rst = cmd.ExecuteReader())
                    {
                        while (rst.Read())
                        {
                            Usuario usuario = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(rst["IdUsuario"]),
                                Nombres = Convert.ToString(rst["Nombres"]),
                                Apellidos = Convert.ToString(rst["Apellidos"]),
                                Correo = Convert.ToString(rst["Correo"]),
                                Clave = Convert.ToString(rst["Clave"]),
                                Reestablecer = Convert.ToBoolean(rst["Reestablecer"]),
                                Activo = Convert.ToBoolean(rst["Activo"])
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                usuarios = new List<Usuario>();
            }

            return usuarios;
        }
    }
}
