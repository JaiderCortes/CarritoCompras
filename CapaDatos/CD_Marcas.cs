using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Marcas
    {
        public List<Marca> Listar(out string Mensaje)
        {
            Mensaje = string.Empty;
            List<Marca> marcas = new List<Marca>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    string sql = "SELECT IdMarca, Descripcion, Activo FROM Marca;";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader rst = cmd.ExecuteReader())
                        {
                            while (rst.Read())
                            {
                                Marca Marca = new Marca()
                                {
                                    IdMarca = Convert.ToInt32(rst["IdMarca"]),
                                    Descripcion = Convert.ToString(rst["Descripcion"]),
                                    Activo = Convert.ToBoolean(rst["Activo"])
                                };
                                marcas.Add(Marca);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                marcas = new List<Marca>();
            }
            return marcas;
        }

        public int Registrar(Marca marca, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SPRegistrarMarca", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion);
                        cmd.Parameters.AddWithValue("Activo", marca.Activo);
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

        public bool Actualizar(Marca marca, out string Mensaje)
        {
            bool actualizado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.con))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SPActualizarMarca", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("IdMarca", marca.IdMarca);
                        cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion);
                        cmd.Parameters.AddWithValue("Activo", marca.Activo);
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
                    using (SqlCommand cmd = new SqlCommand("SPEliminarMarca", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("IdMarca", id);
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
