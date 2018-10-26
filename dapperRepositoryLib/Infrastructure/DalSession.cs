using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperRepositoryLib
{
    public sealed class DalSession : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public DalSession(bool autoBeginTransaction = false)
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LinkDB"].ConnectionString);
            _connection.Open();
            _unitOfWork = new UnitOfWork(_connection);
            if (autoBeginTransaction)
            {
                _unitOfWork.Begin();
            }
        }

        IDbConnection _connection = null;
        UnitOfWork _unitOfWork = null;
        /// <summary>
        /// 
        /// </summary>
        public UnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _unitOfWork.Dispose();
            _connection.Dispose();
        }
    }

}
