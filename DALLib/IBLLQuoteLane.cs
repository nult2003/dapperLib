using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib
{
    public interface IBLLQuoteLane : ICrud<Customers, int>
    {
        int CreateLaneForRevised(int quoteRequestId);
        bool UpdateCustomerCurrencyId(int quoteLaneId, int customerCurrencyId);
        Customers GetForHTO(int id);
        Customers GetForQuotationTool(int id);
        List<Customers> GetListByCrmId(string crmId);
        List<Customers> GetListByCrmIds(List<string> crmIds);
        Customers GetForQuotationToolCustomer(int id);
        int GetIdByQuoteRequestId(int reqId);
        Customers ForHTO(int id);
        Customers SetQuoteLaneCodes(int quoteLaneId, string CodeCountry, string CodeYear);
        bool QuoteRate_SetNeedValidationByQuoteLane(int quoteLaneId);
        Customers GetShipperAndConsigneeForHTOByLane(int id);
    }
}
