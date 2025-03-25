using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Categorias
    {
        public List<Categoria> Listar(out string Mensaje)
        {
            Mensaje = string.Empty;
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    string sql = "SELECT IdCategoria, Descripcion, Activo FROM Categoria;";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader rst = cmd.ExecuteReader())
                        {
                            while (rst.Read())
                            {
                                Categoria categoria = new Categoria()
                                {
                                    IdCategoria = Convert.ToInt32(rst["IdCategoria"]),
                                    Descripcion = Convert.ToString(rst["Descripcion"]),
                                    Activo = Convert.ToBoolean(rst["Activo"])
                                };
                                categorias.Add(categoria);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Mensaje = ex.Message;
                categorias = new List<Categoria>();
            }
            return categorias;
        }

        public int Registrar(Categoria categoria, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SPRegistrarCategoria", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion);
                        cmd.Parameters.AddWithValue("Activo", categoria.Activo);
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

        public bool Actualizar(Categoria categoria, out string Mensaje)
        {
            bool actualizado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SPActualizarCategoria", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("IdCategoria", categoria.IdCategoria);
                        cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion);
                        cmd.Parameters.AddWithValue("Activo", categoria.Activo);
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
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SPEliminarCategoria", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("IdCategoria", id);
                        cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                        Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
