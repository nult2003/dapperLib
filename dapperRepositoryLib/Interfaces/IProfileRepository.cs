using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperRepositoryLib
{
    public interface IProfileRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        IEnumerable<ConcertoInvitationDO> GetInvitationByEmail(string email);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        bool InsertMultiple(IEnumerable<ConcertoInvitationDO> elements);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool InsertOrUpdate(ConcertoInvitationDO element);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responses"></param>
        /// <param name="invitedEmail"></param>
        void SetInvitationResponses(Dictionary<int, bool> responses, string invitedEmail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseIds"></param>
        void SetInvitationAsInactive(IEnumerable<int> responseIds);
    }
}
