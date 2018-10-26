using Dapper;
using Slapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace dapperRepositoryLib
{
    public abstract class DalRepositoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IDbTransaction Transaction { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected IDbConnection Connection { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="transaction"></param>
        public DalRepositoryBase(IDbConnection dbConnection, IDbTransaction transaction)
        {
            Transaction = transaction;
            Connection = dbConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        public DalRepositoryBase()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        protected IEnumerable<T> QueryDbCommand<T>(string procedure, DynamicParameters p = null)
        {
            var result = Connection.Query<dynamic>(sql: procedure, transaction: Transaction, commandType: CommandType.StoredProcedure, param: p);
            var dynamicMapped = Slapper.AutoMapper.MapDynamic<T>(result, keepCache: false) as IEnumerable<T>;
            return dynamicMapped;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        protected T QueryDbCommandSpecific<T>(string procedure, DynamicParameters p = null)
        {
            var dynamicMapped = QueryDbCommand<T>(procedure, p);
            return dynamicMapped.SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        protected bool ExecuteDbCommand(string procedure, DynamicParameters p)
        {
            Connection.Execute(sql: procedure, param: p, commandType: CommandType.StoredProcedure, transaction: Transaction);
            return true;
        }
    }
}
