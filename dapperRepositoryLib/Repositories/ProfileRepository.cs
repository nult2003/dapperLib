using System.Collections.Generic;
using System.Data;
using Dapper;

namespace dapperRepositoryLib
{
    public class ProfileRepository : DalRepositoryBase, IProfileRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="transaction"></param>
        public ProfileRepository(IDbConnection dbConnection, IDbTransaction transaction)
         : base(dbConnection, transaction)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ProfileRepository()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IEnumerable<ConcertoInvitationDO> GetInvitationByEmail(string email)
        {
            var p = new DynamicParameters();
            p.Add("@Email", email);

            return QueryDbCommand<ConcertoInvitationDO>(StoredProcedure.CONCERTOINVITATION_GETALLBYEMAIL, p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public bool InsertMultiple(IEnumerable<ConcertoInvitationDO> elements)
        {
            var dt = new DataTable("ConcertoInvitationList");
            dt.Columns.Add("InvitedEmail");
            dt.Columns.Add("AccountCrmId");
            dt.Columns.Add("SenderCrmId");
            dt.Columns.Add("SenderHelloId");
            dt.Columns.Add("SenderEmail");

            foreach (var element in elements)
            {
                dt.Rows.Add(element.InvitedEmail, element.AccountCrmId, element.SenderCrmId, element.SenderHelloId, element.SenderEmail);
            }

            var p = new DynamicParameters();
            p.Add("@ConcertoInvitationList", dt.AsTableValuedParameter());

            return ExecuteDbCommand(StoredProcedure.CONCERTOINVITATION_INSERTMULTIPLE, p);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool InsertOrUpdate(ConcertoInvitationDO element)
        {
            var p = new DynamicParameters();
            p.Add("@InvitedEmail", element.InvitedEmail);
            p.Add("@AccountCrmId", element.AccountCrmId);
            p.Add("@SenderCrmId", element.SenderCrmId);
            p.Add("@SenderHelloId", element.SenderHelloId);
            p.Add("@SenderEmail", element.SenderEmail);

            return ExecuteDbCommand(StoredProcedure.CONCERTOINVITATION_INSERTORUPDATE, p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseIds"></param>
        public void SetInvitationAsInactive(IEnumerable<int> responseIds)
        {
            var p = new DynamicParameters();
            var dt = new DataTable("ReponseIds");
            dt.Columns.Add("Id");
            foreach (var responseId in responseIds)
            {
                dt.Rows.Add(responseId);
            }

            p.Add("@Responses", dt.AsTableValuedParameter());

            ExecuteDbCommand(StoredProcedure.CONCERTOINVITATION_SETINVITATIONASINACTIVE, p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responses"></param>
        public void SetInvitationResponses(Dictionary<int, bool> responses, string invitedEmail)
        {
            var p = new DynamicParameters();
            var dt = new DataTable("ReponseIds");
            dt.Columns.Add("Id");
            dt.Columns.Add("Response");
            foreach (var response in responses)
            {
                dt.Rows.Add(response.Key, response.Value);
            }

            p.Add("@Responses", dt.AsTableValuedParameter());
            p.Add("@Email", invitedEmail);

            ExecuteDbCommand(StoredProcedure.CONCERTOINVITATION_SETINVITATIONRESPONSE, p);
        }
    }
}
