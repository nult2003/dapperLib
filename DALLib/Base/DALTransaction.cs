using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib
{
    public class DALTransaction : IDisposable
    {
        private bool _disposed;
        public SqlTransaction Transaction { get; }
        public SqlConnection Connection { get; }
        
        public DALTransaction()
        {
            Connection = DALBase.GetSqlConnection();
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public DALTransaction(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            Connection = connection;
            Transaction = connection.BeginTransaction();
        }


        
        public void Commit()
        {
            Transaction.Commit();
        }

        
        public void Rollback()
        {
            Transaction.Rollback();
        }

        #region Dispose pattern

        
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                Transaction?.Dispose();
                Connection?.Close();
            }
            _disposed = true;
        }

        
        ~DALTransaction()
        {
            Dispose(false);
        }

        #endregion
    }
}
