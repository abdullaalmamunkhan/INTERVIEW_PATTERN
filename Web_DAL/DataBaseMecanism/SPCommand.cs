using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_DAL.DataBaseMecanism
{
    public interface ISPCommand
    {
        public DataSet GetDataSet(string strSQL);
        public DataTable ExecuteStoredProcedureDataTable(string strProcedureName, SqlParameter[] arlParams);
        public Task<int> ExecuteNonQueryStoredProcedure(string strProcedureName, SqlParameter[] arlParams);
        public Task<int> ExecuteNonQueryStoredProcedure(string strProcedureName, SqlParameter[] arlParams, SqlConnection connection, SqlTransaction objTransaction);

    }

    public class SPCommand : ISPCommand
    {
        private readonly ISPConnection _sPConnection;
        public SPCommand(ISPConnection sPConnection)
        {
            _sPConnection = sPConnection;
        }

        /// <summary>
        /// Get data sets
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string strSQL)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataSet dataSet = null;
            try
            {

                connection = new SqlConnection(_sPConnection.GetConnectionString());
                command = new SqlCommand();
                command.CommandText = strSQL;
                command.Connection = connection;
                dataSet = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                connection.Open();
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (command != null) command.Dispose();
                if (connection != null && connection.State != ConnectionState.Closed) connection.Close();
            }
            return null;
        }


        /// <summary>
        /// Execute stored procedure data table
        /// </summary>
        /// <param name="strProcedureName"></param>
        /// <param name="arlParams"></param>
        /// <returns></returns>
        public DataTable ExecuteStoredProcedureDataTable(string strProcedureName, SqlParameter[] arlParams)
        {

            using (SqlConnection connection = new SqlConnection(_sPConnection.GetConnectionString()))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = strProcedureName;
                command.CommandType = CommandType.StoredProcedure;


                if (arlParams != null)
                {
                    for (int i = 0; i < arlParams.Length; i++)
                    {
                        if (arlParams[i].Value == null)
                        {
                            arlParams[i].Value = DBNull.Value;
                        }
                        command.Parameters.Add(arlParams[i]);
                    }

                }
                try
                {

                    connection.Open();
                    DataTable dt = new DataTable(strProcedureName);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                    return dt;
                }
                catch (Exception)
                {
                    throw;
                }
            }


        }


        /// <summary>
        /// Execute non query procedure
        /// </summary>
        /// <param name="strProcedureName"></param>
        /// <param name="arlParams"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryStoredProcedure(string strProcedureName, SqlParameter[] arlParams)
        {
            int intResult = 0;

            using (SqlConnection connection = new SqlConnection(_sPConnection.GetConnectionString()))
            using (SqlCommand command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = strProcedureName;
                    command.Connection = connection;

                    if (arlParams != null)
                    {
                        for (int i = 0; i < arlParams.Length; i++)
                        {
                            if (arlParams[i].Value == null)
                            {
                                arlParams[i].Value = DBNull.Value;
                            }
                            command.Parameters.Add(arlParams[i]);
                        }
                    }

                    SqlString rowID = "1";

                    intResult = await command.ExecuteNonQueryAsync();

                    return intResult;

                }
                catch (Exception)
                {

                    throw;
                }
            }

        }


        /// <summary>
        /// Execute non query procedure param
        /// </summary>
        /// <param name="strProcedureName"></param>
        /// <param name="arlParams"></param>
        /// <param name="connection"></param>
        /// <param name="objTransaction"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryStoredProcedure(string strProcedureName, SqlParameter[] arlParams, SqlConnection connection, SqlTransaction objTransaction)
        {
            int intResult = 0;

            using (SqlCommand command = new SqlCommand())
            {
                try
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = strProcedureName;
                    command.Transaction = objTransaction;
                    command.Connection = connection;
                    if (arlParams != null)
                    {
                        for (int i = 0; i < arlParams.Length; i++)
                        {
                            if (arlParams[i].Value == null)
                            {
                                arlParams[i].Value = DBNull.Value;
                            }
                            command.Parameters.Add(arlParams[i]);
                        }
                    }

                    SqlString rowID = "1";

                    intResult = await command.ExecuteNonQueryAsync();
                    command.Parameters.Clear();
                    return intResult;

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }



        /// <summary>
        /// get data bytes
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<byte[]> GetBytes(string sql)
        {

            byte[] b = new byte[0];
            using (SqlConnection connection = new SqlConnection(_sPConnection.GetConnectionString()))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {


                try
                {
                    connection.Open();
                    SqlDataReader dr = await command.ExecuteReaderAsync();
                    dr.Read();
                    if (dr[0] != DBNull.Value)
                        b = (byte[])dr[0];

                    connection.Close();
                    return b;

                }
                catch (Exception)
                {

                    throw;
                }
            }

        }



    }
}
