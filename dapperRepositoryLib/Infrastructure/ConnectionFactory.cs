using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace dapperRepositoryLib
{
    /// <summary>
    /// Factory for manage connections to database 
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private string connectionString = ConfigurationManager.ConnectionStrings["LinkDB"]?.ConnectionString;

        /// <summary>
        /// Gets the command timeout.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        public int CommandTimeout { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFactory"/> class.
        /// </summary>
        public ConnectionFactory()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFactory"/> class.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        public ConnectionFactory(String databaseName = null)
        {
            if (!String.IsNullOrWhiteSpace(databaseName))
                this.connectionString = ConfigurationManager.ConnectionStrings[databaseName].ConnectionString;
            this.CommandTimeout = ConfigurationManager.AppSettings.Get("CommandTimeout") != null ? Int32.Parse(ConfigurationManager.AppSettings.Get("CommandTimeout")) : 0;
        }

        #region Connection

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Gets the SQL connection.
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetSqlConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Gets the SQL connection asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<SqlConnection> GetSqlConnectionAsync()
        {
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }

        #endregion Connection

        #region Command

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns></returns>
        public IDbCommand GetCommand(IDbConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            command.CommandTimeout = this.CommandTimeout;

            return command;
        }

        /// <summary>
        /// Gets the SQL command.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <returns></returns>
        public SqlCommand GetSqlCommand(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, int? commandTimeout = null)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            command.CommandTimeout = commandTimeout ?? command.CommandTimeout;

            return command;
        }

        #endregion Command

        #region Parameters

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void AddParam(IDbCommand command, string name, object value)
        {
            var param = command.CreateParameter();
            param.ParameterName = "@" + name.TrimStart('@');
            param.Value = value;
            command.Parameters.Add(param);
        }

        /// <summary>
        /// Adds the parameter dictionary.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="dico">The dico.</param>
        public static void AddParamDictionary(IDbCommand command, Dictionary<string, object> dico)
        {
            foreach (KeyValuePair<string, object> item in dico)
            {
                var param = command.CreateParameter();
                param.ParameterName = "@" + item.Key.TrimStart('@');
                param.Value = item.Value;
                command.Parameters.Add(param);
            }
        }

        /// <summary>
        /// Adds the int output parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public static SqlParameter AddIntOutputParam(IDbCommand command, string name, object value = null, ParameterDirection direction = ParameterDirection.Output)
        {
            SqlParameter outputParam = new SqlParameter("@" + name.TrimStart('@'), SqlDbType.Int)
            {
                Direction = direction
            };
            if (value != null)
            {
                outputParam.SqlValue = value;
            }

            command.Parameters.Add(outputParam);
            return outputParam;
        }

        /// <summary>
        /// Adds the string output parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public static SqlParameter AddStringOutputParam(IDbCommand command, string name, object value = null, ParameterDirection direction = ParameterDirection.Output)
        {
            SqlParameter outputParam = new SqlParameter("@" + name.TrimStart('@'), SqlDbType.VarChar)
            {
                Direction = direction,
                Size = 200 //https://stackoverflow.com/questions/3759285/ado-net-the-size-property-has-an-invalid-size-of-0
            };
            if (value != null)
            {
                outputParam.SqlValue = value;
            }
            else
            {
                outputParam.SqlValue = DBNull.Value;
            }

            command.Parameters.Add(outputParam);
            return outputParam;
        }

        /// <summary>
        /// Adds the return parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public static SqlParameter AddReturnParam(IDbCommand command)
        {
            SqlParameter outputParam = new SqlParameter()
            {
                Direction = ParameterDirection.ReturnValue
            };
            command.Parameters.Add(outputParam);
            return outputParam;
        }

        /// <summary>
        /// Adds the parameter to command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void AddParamToCommand<T>(IDbCommand command, string name, T value)
        {
            var _param = command.CreateParameter();
            _param.ParameterName = name;
            _param.Value = value != null ? value : (object)DBNull.Value;
            command.Parameters.Add(_param);
        }

        /// <summary>
        /// Adds the string to command with empy is null.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void AddStringToCommandWithEmpyIsNull(IDbCommand command, string name, string value)
        {
            var _param = command.CreateParameter();
            _param.ParameterName = name;
            _param.Value = !string.IsNullOrEmpty(value) ? value : (object)DBNull.Value;
            command.Parameters.Add(_param);
        }

        #region DataTables

        /// <summary>
        /// Creates the identifier data table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        private static DataTable CreateIdDataTable<T>(IEnumerable<T> items)
        {
            DataTable ListIds = new DataTable();

            ListIds.Columns.Add("Id", typeof(T));

            if (items != null)
            {
                foreach (var item in items)
                {
                    DataRow ListIdsRow = ListIds.NewRow();
                    ListIdsRow["Id"] = item;
                    ListIds.Rows.Add(ListIdsRow);
                    ListIds.AcceptChanges();
                }
            }

            return ListIds;
        }

        /// <summary>
        /// Adds the parameter list ids.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="items">The items.</param>
        public static void AddParamListIds<T>(IDbCommand command, string parameterName, IEnumerable<T> items)
        {
            DataTable ListIds = CreateIdDataTable(items);

            var _paramIds = command.CreateParameter();
            _paramIds.ParameterName = parameterName;
            _paramIds.Value = ListIds;
            command.Parameters.Add(_paramIds);
        }

        /// <summary>
        /// Adds the parameter list ids.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="items">The items.</param>
        /// <param name="typename">The typename.</param>
        public static void AddParamListIds<T>(SqlCommand command, string parameterName, IEnumerable<T> items, string typename = null)
        {
            DataTable ListIds = CreateIdDataTable(items);

            var _paramIds = command.CreateParameter();
            _paramIds.ParameterName = "@" + parameterName.TrimStart('@');
            _paramIds.Value = ListIds;
            if (typename != null)
                _paramIds.TypeName = typename;
            command.Parameters.Add(_paramIds);
        }

        #endregion DataTables

        #endregion Parameters

        #region Reader
        /// <summary>
        /// V2 du GetValue, sans le try-catch et le GetOrdinal bullshit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static T GetValueUnsafe<T>(IDataReader reader, string colName) => ConvertReaderValue<T>(reader[colName]);

        /// <summary>
        /// GetValue avec un vrai test d'existence de la colonne, mais du coup beaucoup plus lourd
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static T GetValueSafe<T>(IDataReader reader, string colName)
        {
            // Get the column names
            var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            // If the column exists in the reader
            if (columns.Any(c => c == colName))
            {
                return ConvertReaderValue<T>(reader[colName]);
            }
            return default(T);
        }

        /// <summary>
        /// Convertit la valeur récupéré du DbReader (hopefully)dans le bon type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ConvertReaderValue<T>(object value)
        {
            try
            {
                // if the generic type is nullable and the value in the DB is null, we return the default value
                if (value == null || value == DBNull.Value)
                    return default(T);
                else if (typeof(T) == typeof(bool))
                {
                    // Cas particulier des bit SQL qui se castent mal
                    return (T)(object)Convert.ToBoolean(value);
                }
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
                {
                    // Cas particulier des bit SQL qui se castent mal
                    return (T)(object)Convert.ToInt32(value);
                }
                else if (typeof(T) == typeof(string))
                {
                    return (T)(object)Convert.ToString(value);
                }
                else if (typeof(T) == typeof(double) || typeof(T) == typeof(double?))
                {
                    return (T)(object)Convert.ToDouble(value);
                }
                else if (typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?))
                {
                    return (T)(object)Convert.ToDecimal(value);
                }
                else
                {
                    return (T)value;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Canno't cast value with type " + value.GetType().Name + " to type " + typeof(T).Name);
                throw ex;
            }
        }
        #endregion Reader
    }
}
