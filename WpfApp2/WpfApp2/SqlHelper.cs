using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Add the following references
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WpfApp2
{
    class SqlHelper
    {
        #region Private field variables
        private string connString;
        #endregion Private fiels variables

        #region Constructors
        public SqlHelper()
        {
            //Get the connection string from the App.config file
            connString = ConfigurationManager.ConnectionStrings["cnnStrWsmp"].ConnectionString;
        }
        #endregion Constructors

        #region Data Access Methods
        /// <summary>
        /// Executes a string of SQL code and returns a result set.
        /// </summary>
        /// <param name="sql"> String of SQL code to execute.</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteSql(string sql)
        {
            // Create a connection object
            SqlConnection conn = new SqlConnection(connString);

            // Create a command object
            // Get the SQL to execute from the parameter passed as input to this method

            SqlCommand cmd = new SqlCommand(sql, conn);


            //Open the database connection
            conn.Open();

            //Execute the SQL and return a DataReader. A DataReader is a Forward Only, Read Only Cursor.
            //Which means we can't iterate through it, we have to put its contents into some  'iterable' object,
            //like a DataTable.
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // Put the data into a DataTable
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }
        /// <summary>
        /// Executes a string of SQL code and returns a result set, using and array of SqlParameter objects
        /// </summary>
        /// <param name="sql">String of SQL code to execute.</param>
        /// <param name="parameters">Array of SqlParameter objects</param>
        /// <returns></returns>
        public DataTable ExecuteSql(string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            FillParameters(cmd, parameters);
            cmd.Connection.Open();
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }
        /// <summary>
        /// Fills a sqlCommand with the supplied SqlParameters.
        /// </summary>
        /// <param name="cmd">the Sqlcommand object to add parameters to.</param>
        /// <param name="parameters">Array of SqlParameter objects.</param>

        private void FillParameters(SqlCommand cmd, SqlParameter[] parameters)
        {
            //Loop through the parameters and add each one to the command object's parameter collection.
            //for(int i=0;i<parameters.Length; i++)
            //{
            //    cmd.Parameters.Add(parameters[i]);
            //}
            // this is an easier way to do the above
            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
        }

        public DataTable ExecuteStoredProc(string SPName)
        {
            // Create a connection object
            SqlConnection conn = new SqlConnection(connString);

            // Create a command object
            // Get the SQL to execute from the parameter passed as input to this method

            SqlCommand cmd = new SqlCommand(SPName, conn);

            // Specify the command type as Stored Procedure. The default commandType is  'Text' i.e. some sql
            cmd.CommandType = CommandType.StoredProcedure;

            //Open the database connection
            conn.Open();

            //Execute the SQL and return a DataReader. A DataReader is a Forward Only, Read Only Cursor.
            //Which means we can't iterate through it, we have to put its contents into some  'iterable' object,
            //like a DataTable.
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // Put the data into a DataTable
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }

        public DataTable ExecuteStoredProc(string SPName, SqlParameter[] parameters)
        {
            // Create a connection object
            SqlConnection conn = new SqlConnection(connString);

            // Create a command object
            // Get the SQL to execute from the parameter passed as input to this method

            SqlCommand cmd = new SqlCommand(SPName, conn);

            // Specify the command type as Stored Procedure. The default commandType is  'Text' i.e. some sql
            cmd.CommandType = CommandType.StoredProcedure;
            //Call the FillParameters method, passing it the command object and array of parameters.....
            FillParameters(cmd, parameters);

            //Open the database connection
            conn.Open();

            //Execute the SQL and return a DataReader. A DataReader is a Forward Only, Read Only Cursor.
            //Which means we can't iterate through it, we have to put its contents into some  'iterable' object,
            //like a DataTable.
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // Put the data into a DataTable
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }
        /// <summary>
        /// Executes a non-query (INSERT,UPDATE,DELETE) from a string of SQL Code.
        /// </summary>
        /// <param name="sql">A string of SQL</param>
        /// <param name="paramerers">An array of Sqlparameter.</param>
        /// 
        /// <returns>Number of rows affected.</returns>
        public int NonQuerySql(string sql, SqlParameter[] paramerers)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            FillParameters(cmd, paramerers);
            conn.Open();
            //Execute non query will return the number of rows affected by the INSERT,UPDATE or DELETE
            int rowsAffected = cmd.ExecuteNonQuery();

            //Close connection
            conn.Close();
            return rowsAffected;
        }
        /// <summary>
        /// Execute a non-query(INSERT,UPDATE,DELETE) from a Stored Procedure.
        /// </summary>
        /// <param name="SPName">The name of the Stored Procedure to execute</param>
        /// <param name="paramerers">An array of SqlParameter.</param>
        /// <returns>Number of rows affected.</returns>
        public int NonQueryStoredProc(string SPName, SqlParameter[] paramerers)
        {
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmd = new SqlCommand(SPName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            FillParameters(cmd, paramerers);

            conn.Open();
            //Execute non query will return the number of rows affected by the INSERT,UPDATE or DELETE
            int rowsAffected = cmd.ExecuteNonQuery();

            //Close connection
            conn.Close();
            return rowsAffected;
        }
        #endregion Data Access Methods


    }
}

