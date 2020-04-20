using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActividadCaso1.Models;

namespace ActividadCaso1
{
    public class ClsDatos
    {
        public SqlConnection LeerCadena()
        {
            SqlConnection connection = new SqlConnection("Data Source = sql5047.site4now.net;" +
                "Initial Catalog = DB_A5A76A_Truenorth; User Id = DB_A5A76A_Truenorth_admin; Password = T3csup8214; ");
            return connection;
        }


        public List<Pedido> ListarPedidoFecha ()
        {

            SqlConnection connection = LeerCadena();
            connection.Open();

            List<Pedido> pedidos = new List<Pedido>();
            Pedido pedido;
            SqlDataReader reader;

            try
            {
                SqlCommand cmd = new SqlCommand("USP_ListarPedidosAnio");
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pedido = new Pedido();
                    pedido.Anio = (int)reader[0];
                    pedidos.Add(pedido);
                }

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            System.Console.WriteLine(pedidos.Count);
            connection.Close();
            return pedidos;

        }

        public List<Pedido> ListarPedidoFecha(string x)
        {

            SqlConnection connection = LeerCadena();
            connection.Open();

            List<Pedido> pedidos = new List<Pedido>();
            Pedido pedido;
            SqlDataReader reader;

            try
            {
                SqlCommand cmd = new SqlCommand("USP_ListarPedidosMes");
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Anio", x);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pedido = new Pedido();
                    pedido.Mes = (int)(reader[0]);
                    pedidos.Add(pedido);
                }

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            connection.Close();
            return pedidos;

        }

        public List<Empleado> ListarEmpleados(int mm, int yyyy)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();
            List<Empleado> empleados = new List<Empleado>();
            Empleado empleado;
            SqlDataReader reader;

            try
            {
                SqlCommand cmd = new SqlCommand("USP_ListarEmpleados");
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mes", mm);
                cmd.Parameters.AddWithValue("@Anio", yyyy);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    empleado = new Empleado();
                    empleado.IdEmpleado = (int)(reader[0]);
                    empleado.Apellidos = (string)(reader[1]);
                    empleado.Nombre = (string)(reader[2]);
                    empleado.Cargo = (string)(reader[3]);

                    empleados.Add(empleado);

                }

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            connection.Close();
            return empleados;

        }

        public DataTable ListarCliente(int mm, int yyyy, string nombreEmpleado)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();
            SqlDataReader reader;

            DataTable dataTable = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand("USP_ListarClientes");
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mes", mm);
                cmd.Parameters.AddWithValue("@Anio", yyyy);
                cmd.Parameters.AddWithValue("@NombreEmpleado", nombreEmpleado);
                reader = cmd.ExecuteReader();

                dataTable.Load(reader);

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            connection.Close();
            return dataTable;
        }

        public DataTable ListarPedido (string idCliente)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();
            SqlDataReader reader;
            DataTable dataTable = new DataTable();

            System.Console.WriteLine("ID LLEGA A METHOD LISTARPEDIDO " + idCliente);

            try
            {
                SqlCommand cmd = new SqlCommand("USP_ListarPedidos");
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                reader = cmd.ExecuteReader();

                dataTable.Load(reader);
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            connection.Close();
            return dataTable;

        }

        //public DataTable ListaPedidoFechas(DateTime x, DateTime y)
        //{
        //    SqlConnection connection = LeerCadena();
        //    connection.Open();
        //    SqlDataAdapter sqlData = new SqlDataAdapter("USP_ListarPedidosEntreFechas", connection);
        //    sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    sqlData.SelectCommand.Parameters.AddWithValue("@Fecha1", x);
        //    sqlData.SelectCommand.Parameters.AddWithValue("@Fecha2", y);
        //    DataTable dataTable = new DataTable();
        //    sqlData.Fill(dataTable);
        //    connection.Close();
        //    return dataTable;
        //}

        //public DataTable ListarDetalle(int x)
        //{
        //    SqlConnection connection = LeerCadena();
        //    connection.Open();
        //    SqlDataAdapter sqlData = new SqlDataAdapter("USP_ListarDetalle", connection);
        //    sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    sqlData.SelectCommand.Parameters.AddWithValue("@IdPedido", x);

        //    DataTable dataTable = new DataTable();
        //    sqlData.Fill(dataTable);

        //    connection.Close();

        //    return dataTable;
        //}

        public DataTable ListarDetallePedido (int idPedido)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();
            SqlDataReader reader;
            DataTable dataTable = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand("USP_DetallePedido");
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                reader = cmd.ExecuteReader();

                dataTable.Load(reader);
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            connection.Close();
            return dataTable;
        }

        public double PedidoTotal(Int32 idPedido)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();

            SqlDataAdapter sqlData = new SqlDataAdapter("USP_Total", connection);
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.AddWithValue("@IdPedido", idPedido);
            sqlData.SelectCommand.Parameters.Add("@Total", SqlDbType.Money).Direction =
                ParameterDirection.Output;

            sqlData.SelectCommand.ExecuteScalar();
            double total = Convert.ToInt32(sqlData.SelectCommand.Parameters["@Total"].Value);
            //var s = string.Join(",", sqlData.SelectCommand.Parameters.Cast<SqlParameter>().ToList().Select(p => $"{p.ParameterName}={p.Value}"));
            //Console.WriteLine(s);

            connection.Close();

            return total;

        }


    }
}
