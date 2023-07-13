using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.Data
{
    public class Database
    {
        #region Private fields members
        private string ConnectionString;

        private SqlConnection _connection;

        private System.Reflection.Assembly _assembly;

        private System.Reflection.Assembly Assembly
        {
            get
            {
                if (_assembly == null)
                {
                    _assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    return _assembly;
                }
                return _assembly;
            }
        }
        #endregion

        #region Public fields
        public string DataSource
        {
            get
            {
                string result = string.Empty;
                using (SqlConnection sqlConnection = CreateConnection)
                {
                    result = sqlConnection.DataSource;
                }

                return result;
            }
        }

        public string InitialCatalog
        {
            get
            {
                string result = string.Empty;
                using (SqlConnection sqlConnection = CreateConnection)
                {
                    result = sqlConnection.Database;
                }

                return result;
            }
        }
        #endregion

        #region Public methods
        public SqlConnection CreateConnection
        {
            get
            {
                if (_connection != null)
                {
                    return _connection;
                }

                return new SqlConnection(ConnectionString);
            }
        }

        public SqlConnection OpenConnection()
        {
            SqlConnection connection = null;
            try
            {
                try
                {
                    connection = CreateConnection;
                    connection.Open();
                }
                catch (Exception e)
                {                    
                    throw e;
                }                
            }
            catch
            {
                if (connection != null)
                    connection.Close();
                throw;
            }
            return connection;
        }

        public SqlCommand GetSqlStringCommand(SqlConnection connection, string query)
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentException("The value can not be null or an empty string.", "query");
            SqlCommand command = CreateCommand(CommandType.Text, query);
            InitializeCommand(command, connection);
            return command;
        }

        public SqlCommand GetSqlStringCommand(SqlTransaction transaction, string query)
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentException("The value can not be null or an empty string.", "query");
            SqlCommand command = CreateCommand(CommandType.Text, query);
            InitializeCommand(command, transaction);
            return command;
        }

        public void RunLocalStoredCommands(Assembly assembly, string resourceName)
        {
            TextReader textReader = new StreamReader(assembly.GetManifestResourceStream(resourceName));
            RunLocalStoredCommands(textReader);
        }

        public void RunLocalStoredCommands(TextReader textReader)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = textReader.ReadToEnd();
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            SqlTransaction sqlTransaction = null;
            try
            {
                sqlConnection.Open();
                sqlTransaction = (sqlCommand.Transaction = sqlConnection.BeginTransaction());
                sqlCommand.Connection = sqlConnection;
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                ex.ToString();
                sqlTransaction?.Rollback();
                throw ex;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlCommand.Connection.Close();
                }
            }
        }

        public void AddInParameter(SqlCommand command, string name, DbType dbType, object value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = name;
            param.DbType = dbType;
            param.Value = value ?? DBNull.Value;

            command.Parameters.Add(param);
        }
        #endregion

        #region Private methods

        private void InitializeCommand(SqlCommand command, SqlConnection connection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (connection == null) throw new ArgumentNullException("connection");
            command.Connection = connection;
        }

        private void InitializeCommand(SqlCommand command, SqlTransaction transaction)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (transaction == null) throw new ArgumentNullException("connection");
            InitializeCommand(command, transaction.Connection);
            command.Transaction = transaction;
        }

        private SqlCommand CreateCommand(CommandType commandType, string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentException("The value can not be null or an empty string.", "commandText");
            //SqlCommand command = CreateCommand(commandType, commandText);
            var command = new SqlCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }
        #endregion

        #region Constructors and Initialization
        public Database(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Database Administration        
        public void Seed()
        {
        }

        public void DropSchema()
        {
            RunLocalStoredCommands(Assembly, "TechBank.Data.Scripts.Drop.drop_database.sql");
        }
        #endregion
    }
}
