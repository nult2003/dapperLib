using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib
{
    public interface IDALQuoteLane : ICrud<Customers, int>, IDALCache, ITransaction
    {
        bool UpdateCustomerCurrencyId(int quoteLaneId, int customerCurrencyId);
        int CreateLaneForRevised(int quoteRequestId);
        //QuoteLane GetForHTO(int id);
        //QuoteLane GetShipperAndConsigneeForHTOByLane(int id);
        //QuoteLane GetForQuotationTool(int id);
        //List<QuoteLane> GetListByCrmId(string CrmId);
        //QuoteLane GetForQuotationToolCustomer(int id);
        //int GetIdByQuoteRequestId(int reqId);
        //QuoteLane ForHTO(int id);
        //QuoteLane SetQuoteLaneCodes(int quoteLaneId, string codeCountry, string codeYear);
        //bool SetNeedValidationByQuoteLane(int quoteLaneId);
        //QuoteLane GetForQuotationApi(string quoteRequestRef);

        /// <summary>
        ///     If the SQLTransaction is done, commits this transaction instance.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Rollbacks this instance.
        /// </summary>
        void Rollback();

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void Dispose();
    }
}
