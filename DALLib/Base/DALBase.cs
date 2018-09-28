using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.Caching;
using System.Transactions;
using System.Configuration;

namespace DALLib
{
    public abstract class DALBase : IDALCache, IDisposable
    {
        private SqlTransaction _transaction;
        private ObjectCache _cache;


        private bool _disposed;


        protected SqlConnection Connection { get; private set; }

        protected SqlTransaction Transaction { get { return _transaction; } }
        #region Constructors

        

        protected DALBase(SqlConnection connection = null)
        {
            CreateSqlConnection(connection ?? GetSqlConnection());
            _cache = MemoryCache.Default;
        }

        
        public DALBase(DALTransaction transaction, ObjectCache cache = null)
        {
            if (transaction != null)
            {
                _transaction = transaction.Transaction;
                Connection = _transaction.Connection;
            }
            else
            {
                Connection = GetSqlConnection();
                //try
                //{
                //    Connection.Open();
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception("La connexion à la base de données a été refusée", ex);
                //}
            }
            if (_cache == null)
                _cache = MemoryCache.Default;
            _cache = cache;
        }

        #endregion

        #region Helper methods for derived classes

        
        protected SqlCommand GetCommandForStoredProcedure(string storedProcedureName)
        {
            var command = new SqlCommand(storedProcedureName) { CommandType = CommandType.StoredProcedure };
            if (_transaction != null)
                command.Transaction = _transaction;
            command.Connection = Connection;
            return command;
        }

        
        protected SqlCommand GetCommandForParameteredQuery(string query)
        {
            var command = new SqlCommand(query);
            if (_transaction != null)
                command.Transaction = _transaction;
            command.Connection = Connection;

            return command;
        }

        
        protected void AddParamToCommand(SqlCommand command, string parameterName, object parameterValue)
        {
            command.Parameters.AddWithValue(parameterName, parameterValue ?? DBNull.Value);
        }

        
        protected void AddParamToCommand(SqlCommand command, string parameterName, object parameterValue,
            SqlDbType typeSql, int size)
        {
            if (parameterValue != null)
                command.Parameters.Add(parameterName, typeSql, size).Value = parameterValue;
            else
                command.Parameters.AddWithValue(parameterName, DBNull.Value);
        }

        
        protected void AddParamToCommand<T>(SqlCommand command, string parameterName, T? parameterValue)
            where T : struct, IComparable
        {
            if (parameterValue.HasValue)
                command.Parameters.AddWithValue(parameterName, parameterValue);
            else
                command.Parameters.AddWithValue(parameterName, DBNull.Value);
        }

        
        protected T GetValueFromDbValue<T>(object dbValue)
        {
            if (!Convert.IsDBNull(dbValue))
                return (T)dbValue;
            return default(T);
        }

        
        internal static SqlConnection GetSqlConnection()
        {
            var connection = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
            };
            //connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


            return connection;
        }

        
        public void Commit()
        {
            _transaction?.Commit();
        }

        
        public void Rollback()
        {
            _transaction?.Rollback();
        }

        #endregion

        public virtual void UseTransaction(DALTransaction transaction)
        {
            if (transaction != null && _transaction == null)
            {
                _transaction = transaction.Transaction;
                Connection = transaction.Connection;
            }
        }

        public virtual DALTransaction BeginTransaction()
        {
            if (Transaction == null)
            {
                //var newTransaction = Connection != null ? new DALTransaction(Connection) : new DALTransaction();
                var newTransaction = new DALTransaction(Connection);
                _transaction = newTransaction.Transaction;
                return newTransaction;

            }
            return null;
        }

        public virtual TransactionScope BeginTransactionScope()
        {
            return new TransactionScope();
        }

        private void CreateSqlConnection(SqlConnection connection)
        {
            Connection = connection;
            //if (Connection.State != ConnectionState.Open)
            //    try
            //    {
            //        Connection.Open();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("La connexion à la base de données a été refusée", ex);
            //    }
        }

        public T GetFromCache<R, T>(Func<R, T> getter, CacheItemPolicy policy = null, R param = default(R)) where T : class
        {
            //var key = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            //var key = string.Format("{0}.{1}", getter.Method.DeclaringType.Name, getter.Method.Name, param.ToString());
            var key = $"{getter.Method.DeclaringType.Name}.{getter.Method.Name}({param})";
            var items = MemoryCache.Default.Get(key) as T;
            if (items == null)
            {
                var items_ = getter(param);
                _cache.Set(key, items_, (policy ?? GetCachePolicy<R, T>(getter, param)));
                items = items_;
            }
            return items;
        }

        private CacheItemPolicy GetCachePolicy<R, T>(Func<R, T> getter = null, R param = default(R))
        {
            CacheEntryUpdateCallback callback = (CacheEntryUpdateArguments args) =>
            {
                if (args.RemovedReason == CacheEntryRemovedReason.Expired || args.RemovedReason == CacheEntryRemovedReason.Removed)
                {
                    CacheItemPolicy cachePolicy = GetCachePolicy<R, T>(getter, param);
                    var items = getter(param);
                    if (items != null)
                    {
                        args.UpdatedCacheItem = new CacheItem(args.Key, items);
                        args.UpdatedCacheItemPolicy = cachePolicy;
                    }
                }
            };

            var cip = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(24),
                UpdateCallback = new CacheEntryUpdateCallback(callback)
            };

            return cip;
        }

        public T GetFromCache<T>(Func<T> getter, CacheItemPolicy policy = null) where T : class
        {
            //var key = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            //var key = string.Format("{0}.{1}", getter.Method.DeclaringType.Name, getter.Method.Name);
            var key = $"{getter.Method.DeclaringType.Name}.{getter.Method.Name}";

            var items = MemoryCache.Default.Get(key) as T;
            if (items == null)
            {
                var items_ = getter();
                _cache.Set(key, items_, (policy ?? GetCachePolicy<T>(getter)));
                items = items_;
            }
            return items;
        }
        private CacheItemPolicy GetCachePolicy<T>(Func<T> getter = null)
        {
            CacheEntryUpdateCallback callback = (CacheEntryUpdateArguments args) =>
            {
                if (args.RemovedReason == CacheEntryRemovedReason.Expired || args.RemovedReason == CacheEntryRemovedReason.Removed)
                {
                    CacheItemPolicy cachePolicy = GetCachePolicy<T>(getter);
                    var items = getter();
                    if (items != null)
                    {
                        args.UpdatedCacheItem = new CacheItem(args.Key, items);
                        args.UpdatedCacheItemPolicy = cachePolicy;
                    }
                }
            };

            var cip = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(24),
                //AbsoluteExpiration = DateTime.Now.AddHours(2),
                UpdateCallback = new CacheEntryUpdateCallback(callback)
            };

            return cip;
        }

        #region Dispose pattern

        
        public void Dispose()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (_transaction == null)
                    Connection?.Close();
            _disposed = true;
        }

        
        ~DALBase()
        {
            Dispose(false);
        }

        #endregion
    }
}
