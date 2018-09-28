using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib
{
    public class DALQuoteLane : DALDapperBase<Customers, int>, IDALQuoteLane
    {
        //private static ClassLoggerHelper loggerHelper = new ClassLoggerHelper("DALQuoteLane");
        public DALQuoteLane(SqlConnection connection = null) : base(connection)
        {

        }

        public int CreateLaneForRevised(int quoteRequestId)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrUpdate(Customers element)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customers> GetAll()
        {
            //using (loggerHelper.CreateMethodLogger())
            //{
            return QueryDbCommand("sp_GetCustomers");
            //}
        }

        public Customers GetSpecific(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCustomerCurrencyId(int quoteLaneId, int customerCurrencyId)
        {
            throw new NotImplementedException();
        }
        /*
public List<QuoteLane> GetAll()
{
   using (loggerHelper.CreateMethodLogger())
   {
       return QueryDbCommand(StoredProcedure.QUOTELANE_GET);
   }
}

public bool UpdateCustomerCurrencyId(int quoteLaneId, int customerCurrencyId)
{
   using (loggerHelper.CreateMethodLogger($"quoteLaneId={quoteLaneId}, customerCurrencyId={customerCurrencyId}"))
   {
       var p = new DynamicParameters();
       p.SetDefaultReturnValue();
       p.Add("@quoteLaneId", quoteLaneId);
       p.Add("@currencyId", customerCurrencyId);

       ExecuteDbCommand(StoredProcedure.QUOTELANE_UPDATECUSTOMERCURRENCYID, p);

       return p.GetIfSuccess();
   }
}

public int CreateLaneForRevised(int quoteRequestId)
{
   using (loggerHelper.CreateMethodLogger(quoteRequestId.ToString()))
   {
       var p = new DynamicParameters();
       p.SetDefaultReturnValue();
       p.Add("@QuoteRequestId", quoteRequestId);
       p.Add("@NewQuoteLaneId", dbType: DbType.Int32, size: 4, direction: ParameterDirection.Output);

       ExecuteDbCommand(StoredProcedure.QUOTELANE_CREATELANEFORREVISED, p);

       return p.GetIfSuccess() ? p.Get<int>("NewQuoteLaneId") : 0;
   }
}

public QuoteLane GetSpecific(int id)
{
   using (loggerHelper.CreateMethodLogger($"Id={id}"))
   {
       var p = new DynamicParameters();
       p.Add("@Id", id);
       return QueryDbCommandSpecific(StoredProcedure.QUOTELANE_GET, p);
   }
}

public bool CreateOrUpdate(QuoteLane element)
{
   using (loggerHelper.CreateMethodLogger($"Id={element.Id};QuoteRequestId={element.QuoteRequestId}"))
   {
       var newElement = element.Id == 0;
       var p = new DynamicParameters();
       p.SetDefaultReturnValue();
       p.Add("@QuoteRequestId", element.QuoteRequestId);

       if (element.CustomerCurrencyId != null)
       {
           p.Add("@CustomerCurrencyId", element.CustomerCurrencyId);
       }

       if (newElement)
       {
           p.Add("@Id", dbType: DbType.Int32, size: 4, direction: ParameterDirection.Output);
           element.Id = ExecuteDbCommand(StoredProcedure.QUOTELANE_ADD, p, "Id");
       }
       else
       {
           p.Add("@Id", element.Id);


           ExecuteDbCommand(StoredProcedure.QUOTELANE_UPD, p);
       }

       return p.GetIfSuccess();
   }
}

public bool Delete(int id)
{
   using (loggerHelper.CreateMethodLogger($"Id={id}"))
   {
       var p = new DynamicParameters();
       p.SetDefaultReturnValue();
       p.Add("@Id", id);
       ExecuteDbCommand(StoredProcedure.QUOTELANE_DEL, p);
       return p.GetIfSuccess();
   }
}


public QuoteLane GetForHTO(int id)
{
   using (loggerHelper.CreateMethodLogger($"id={id}"))
   {
       var p = new DynamicParameters();
       p.Add("@Id", id);
       return QueryDbCommandSpecific(StoredProcedure.QUOTELANE_GETFORHTO, p);
   }
}
public QuoteLane GetShipperAndConsigneeForHTOByLane(int id)
{
   using (loggerHelper.CreateMethodLogger($"id={id}"))
   {
       var p = new DynamicParameters();
       p.Add("@Id", id);
       return QueryDbCommandSpecific(StoredProcedure.QUOTELANE_GETSHIPPERANDCONSIGNEEFORHTO, p);
   }
}

public QuoteLane GetForQuotationTool(int id)
{
   using (loggerHelper.CreateMethodLogger($"id={id}"))
   {
       var p = new DynamicParameters();
       p.Add("@Id", id);
       return QueryDbCommandSpecific(StoredProcedure.QUOTELANE_GETFORQUOTATIONTOOL, p);
   }
}
public List<QuoteLane> GetListByCrmId(string CrmId)
{
   using (loggerHelper.CreateMethodLogger($"id={CrmId}"))
   {
       var p = new DynamicParameters();
       p.SetDefaultReturnValue();
       p.Add("@CrmId", CrmId);
       return QueryDbCommand(StoredProcedure.QUOTELANE_GETLISTBYCRMID, p);
   }
}
public QuoteLane GetForQuotationToolCustomer(int id)
{
   using (loggerHelper.CreateMethodLogger($"id={id}"))
   {
       var p = new DynamicParameters();
       p.Add("@Id", id);
       return QueryDbCommandSpecific(StoredProcedure.QUOTELANE_GETFORQUOTATIONTOOLCUSTOMER, p);
   }
}

public int GetIdByQuoteRequestId(int reqId)
{
   using (loggerHelper.CreateMethodLogger($"reqId={reqId}"))
   {
       var p = new DynamicParameters();
       p.Add("@ReqId", reqId);
       var res = QueryDbCommandSpecific(StoredProcedure.QUOTELANE_GETIDBYQUOTEREQUESTID, p);

       return res.Id;
   }
}
public QuoteLane ForHTO(int id)
{
   using (loggerHelper.CreateMethodLogger($"id={id}"))
   {
       var p = new DynamicParameters();
       p.Add("@Id", id);
       return QueryDbCommandSpecific(StoredProcedure.QUOTELANE_FORHTO, p);
   }
}

public QuoteLane SetQuoteLaneCodes(int quoteLaneId, string codeCountry, string codeYear)
{
   using (loggerHelper.CreateMethodLogger())
   {
       var p = new DynamicParameters();
       p.Add("@QuoteLaneId", quoteLaneId);
       p.Add("@CodeCountry", codeCountry);
       p.Add("@CodeYear", codeYear);
       return QueryDbCommandSpecific(StoredProcedure.QUOTELANE_SETCODES, p);
   }
}
public bool SetNeedValidationByQuoteLane(int quoteLaneId)
{
   using (loggerHelper.CreateMethodLogger($"quoteLaneId={quoteLaneId}"))
   {
       var p = new DynamicParameters();
       p.Add("@QuoteLaneId", quoteLaneId);
       p.SetDefaultReturnValue();
       ExecuteDbCommand(StoredProcedure.QUOTERATE_SETNEEDVALIDATIONBYQUOTELANE, p);
       return p.GetIfSuccess();
   }
}
public QuoteLane GetForQuotationApi(string quoteRequestRef)
{
   using (loggerHelper.CreateMethodLogger($"quoteRequestRef={quoteRequestRef}"))
   {
       var p = new DynamicParameters();
       p.Add("@quoteRequestRef", quoteRequestRef);
       p.SetDefaultReturnValue();

       return QueryDbCommandSpecific(StoredProcedure.QUOTERATE_GETFORQUOTATIONAPI, p);
   }
}
*/
    }
}
