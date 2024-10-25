using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper
{
    public class DbHelperService
    {
        public static string connectionString = string.Empty;
        private readonly IConfiguration _configuration;
       
        public DbHelperService(IConfiguration configuration)
        {
            try
            {
                _configuration = configuration;
                var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = builder.Build();
                connectionString= _configuration["ConnectionString"];
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Database connection string not found in configuration.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("System error occurred please contact our support team.");
            }
        }
      
        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            int k = 0;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(commandParameters);
                    connection.Open();
                    k = command.ExecuteNonQuery();
                }
                return k;
            }
            catch
            {
                return k;
            }
        }

        public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                var connection = new SqlConnection(connectionString);
                var command = new SqlCommand(commandText, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                connection.Close();
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Flag");
                dt.Columns.Add("Message");

                DataRow dr = dt.NewRow();
                dr["Flag"] = "0";
                dr["Message"] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

            }
            return ds;
        }

        #region Created by maqsood
        public async Task<T> ExecuteQueryAsync<T>(string commandText, params object[] values)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] parameters = PrepareSqlParameters(commandText, connectionString, values);
                        command.Parameters.AddRange(parameters);
                        using (var da = new SqlDataAdapter(command))
                        {
                            await Task.Run(() => da.Fill(ds));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataSetToJson<DbResponseDto<T>>(ds).Table.FirstOrDefault();
        }

        public static T ExecuteQuery<T>(string commandText, params object[] values)
        {
            DataSet ds = new DataSet();
            try
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                var command = new SqlCommand(commandText, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameters = PrepareSqlParameters(commandText, connectionString, values);
                command.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataSetToJson<DbResponseDto<T>>(ds).Table.FirstOrDefault();
        }
        public static T DataSetToJson<T>(DataSet dataSet) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dataSet, Newtonsoft.Json.Formatting.Indented));


        public async Task<DbResponseDto<T>> GetAllAsync<T>(string commandText, params object[] values)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] parameters = PrepareSqlParameters(commandText, connectionString, values);
                        command.Parameters.AddRange(parameters);
                        using (var da = new SqlDataAdapter(command))
                        {
                            await Task.Run(() => da.Fill(ds));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataSetToJsonList<T>(ds);
        }

        public static DbResponseDto<T> GetAll<T>(string commandText, params object[] values)
        {
            DataSet ds = new DataSet();
            try
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                var command = new SqlCommand(commandText, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameters = PrepareSqlParameters(commandText, connectionString, values);
                command.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataSetToJsonList<T>(ds);
        }
        public static DbResponseDto<T> DataSetToJsonList<T>(DataSet dataSet) => JsonConvert.DeserializeObject<DbResponseDto<T>>(JsonConvert.SerializeObject(dataSet, Newtonsoft.Json.Formatting.Indented));
        public static SqlParameter[] PrepareSqlParameters(string spname, string conn, params object[] values)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand(spname, connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(command);
            SqlParameterCollection _sqlParametersCollection = command.Parameters;
            connection.Close();
            var sqlParameters = new SqlParameter[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                var parameterValue = values[i] ?? DBNull.Value;
                var parameterName = _sqlParametersCollection[i + 1].ParameterName;
                sqlParameters[i] = new SqlParameter(parameterName, parameterValue);
            }
            return sqlParameters;
        }
        #endregion
    }
}
