using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DALLib
{
    public interface ITransaction
    {
        void UseTransaction(DALTransaction transaction);
        DALTransaction BeginTransaction();
        TransactionScope BeginTransactionScope();
        void Commit();
        void Rollback();
    }
}
