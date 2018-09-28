
using Dapper;
using System.Collections.Generic;
using System;

using System.Data;
using System.Data.SqlClient;

namespace DALLib
{
    public class DALDashboard : DALDapperBase<Customers, int>//, IDALDashboard
    {        
        public DALDashboard(SqlConnection connection = null) : base(connection)
        {

        }

        //public List<QuoteRequest> GetSpecificStats(List<string> selectedTransportList, DateTime dateFrom, DateTime toDate, int quoteUserId)
        //public List<QuoteRequest> GetSpecificStats(DateTime dateFrom, DateTime toDate, int quoteUserId, List<Guid> userOrganizations, bool IsOrganizationLevel)
        //{
        //    //using (loggerHelper.CreateMethodLogger("GetSpecificStats", $"selectedTransportList={selectedTransportList}, dateFrom={dateFrom},quoteUserId={quoteUserId}"))
        //    using (loggerHelper.CreateMethodLogger($"dateFrom={dateFrom},quoteUserId={quoteUserId}"))
        //    {
        //        var p = new DynamicParameters();
        //        p.Add("@userId", quoteUserId);
        //        p.Add("@dateFrom", dateFrom);
        //        p.Add("@dateTo", toDate);
        //        //DataTable dt = new DataTable();
        //        //dt.Columns.Add("Code");
        //        //if (selectedTransportList != null && selectedTransportList.Count > 0)
        //        //{
        //        //    foreach (var code in selectedTransportList)
        //        //    {
        //        //        if (!string.IsNullOrEmpty(code))
        //        //        {
        //        //            dt.Rows.Add(code);
        //        //        }
        //        //    }
        //        //}
        //        //p.Add("@transportModeList", dt.AsTableValuedParameter());
        //        DataTable dtorg = new DataTable();
        //        dtorg.Columns.Add("Id");
        //        if (userOrganizations != null && userOrganizations.Count > 0)
        //        {
        //            foreach (var org in userOrganizations)
        //            {
        //                dtorg.Rows.Add(org);
        //            }
        //        }

        //        p.Add("@UserOrganization", dtorg.AsTableValuedParameter());
        //        p.Add("@IsOrganizationLevel", IsOrganizationLevel);

        //        return QueryDbCommand(StoredProcedure.DASHBOARD_GETSPECIFICSTATS, p);
        //    }
        //}

        public List<Customers> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customers GetSpecific(int id)
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
    }
}
