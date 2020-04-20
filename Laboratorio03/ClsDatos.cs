using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio03
{
    public class ClsDatos
    {
        public SqlConnection LeerCadena()
        {
            SqlConnection connection = new SqlConnection("Data Source=sql5047.site4now.net;" + "Initial Catalog=DB_A5A76A_Truenorth;" +
                "User ID=DB_A5A76A_Truenorth_admin; Password=T3csup8214");
            return connection;
        }
        public DataTable ListaPedidoFechas(DateTime x, DateTime y)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter("USP_ListarPedidosEntreFechas", connection);
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.AddWithValue("@Fecha1", x);
            sqlData.SelectCommand.Parameters.AddWithValue("@Fecha2", y);
            DataTable dataTable = new DataTable();
            sqlData.Fill(dataTable);
            connection.Close();
            return dataTable;
        }

        public DataTable ListarDetalle(int x)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter("USP_ListarDetalle", connection);
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.AddWithValue("@IdPedido", x);

            DataTable dataTable = new DataTable();
            sqlData.Fill(dataTable);

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
            // var s = string.Join(",", sqlData.SelectCommand.Parameters.Cast<SqlParameter>().ToList().Select(p => $"{p.ParameterName}={p.Value}"));
            // Console.WriteLine(s);

            connection.Close();

            return total;

        }
    }
}
