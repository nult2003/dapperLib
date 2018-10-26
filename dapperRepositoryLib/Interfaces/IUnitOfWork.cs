using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperRepositoryLib
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// 
        /// </summary>
        IDbConnection Connection { get; }
        /// <summary>
        /// 
        /// </summary>
        IDbTransaction Transaction { get; }
        /// <summary>
        /// 
        /// </summary>
        void Begin();
        /// <summary>
        /// 
        /// </summary>
        void Commit();
        /// <summary>
        /// 
        /// </summary>
        void Rollback();
    }
}
