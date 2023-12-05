using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace back_mascota.Models
{
    public class GestorMascota
    {
        //GET
        public List<Mascota> getMascota() {
        
        List<Mascota> list = new List<Mascota>();// Se inicializa una lista de típo mascota llamada list

        string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString(); //Se recupera la cadena de conexión

            using (SqlConnection conn = new SqlConnection(strConn)) //Se crea una instancia de SqlConnection utilizando la cadena de conexión 
            {
                conn.Open(); // se abre la conexión a la base de datos.

                //Se crea un comando SQL(SqlCommand) para llamar al procedimiento almacenado
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Mascota_All";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader(); //Se ejecuta la consulta utilizando ExecuteReader().

                //Se recorren las filas devueltas por la consulta y se construyen objetos Mascota a partir de los datos leídos
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nombre = dr.GetString(1).Trim();
                    int edad = dr.GetInt32(2);
                    string desc = dr.GetString(3).Trim();

                    Mascota mascota = new Mascota(id, nombre, edad , desc);
                    list.Add(mascota);
                }

                //Se cierran el lector (SqlDataReader) y la conexión
                dr.Close();
                conn.Close();
            }
        return list; //se retorna el chorizo
        }

        //ADD
        public bool addMascota(Mascota mascota)
        {
            bool respuesta = false;

            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = conn.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = "Mascota_Add";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", mascota.nombre);
                cmd.Parameters.AddWithValue("@edad", mascota.edad);
                cmd.Parameters.AddWithValue("@descripcion", mascota.descripcion);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    respuesta = false;
                    throw;
                }
                finally
                {
                    cmd.Parameters.Clear();
                    conn.Close();
                }

                return respuesta;
            }
        }

        //UPDATE
        public bool updateMascota(int id, Mascota mascota)
        {
            bool respuesta = false;

            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = conn.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = "Mascota_Update";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombre", mascota.nombre);
                cmd.Parameters.AddWithValue("@edad", mascota.edad);
                cmd.Parameters.AddWithValue("@descripcion", mascota.descripcion);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    respuesta = false;
                    throw;
                }
                finally
                {
                    cmd.Parameters.Clear();
                    conn.Close();
                }

                return respuesta;
            }
        }
        public bool deleteMascota(int id)
        {
            bool respuesta = false;

            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = conn.CreateCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.CommandText = "Mascota_Delete";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    respuesta = false;
                    throw;
                }
                finally
                {
                    cmd.Parameters.Clear();
                    conn.Close();
                }

                return respuesta;
            }
        }

    }
}