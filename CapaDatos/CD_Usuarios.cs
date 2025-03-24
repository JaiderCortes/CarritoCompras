using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar(out string Mensaje)
        {
            Mensaje = string.Empty;
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
                Mensaje = ex.Message;
                usuarios = new List<Usuario>();
            }

            return usuarios;
        }

        public int Registrar(Usuario usuario, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SPRegistrarUsuario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Nombres", usuario.Nombres);
                        cmd.Parameters.AddWithValue("Apellidos", usuario.Apellidos);
                        cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                        cmd.Parameters.AddWithValue("Clave", usuario.Clave);
                        cmd.Parameters.AddWithValue("Activo", usuario.Activo);
                        cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        idGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                        Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                idGenerado = 0;
                Mensaje = ex.Message;
            }
            return idGenerado;
        }

        public bool Actualizar(Usuario usuario, out string Mensaje)
        {
            bool actualizado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SPActualizarUsuario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("IdUsuario", usuario.IdUsuario);
                        cmd.Parameters.AddWithValue("Nombres", usuario.Nombres);
                        cmd.Parameters.AddWithValue("Apellidos", usuario.Apellidos);
                        cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                        cmd.Parameters.AddWithValue("Activo", usuario.Activo);
                        cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        actualizado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                        Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                actualizado = false;
                Mensaje = ex.Message;
            }
            return actualizado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using(SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    string sql = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario;";
                    using(SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@IdUsuario", id);

                        resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }

            return resultado;
        }
    }
}
