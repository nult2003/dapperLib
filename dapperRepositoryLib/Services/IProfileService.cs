using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperRepositoryLib
{
    public interface IProfileService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        IEnumerable<ConcertoInvitationBO> GetInvitationByEmail(string email);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool InsertOrUpdate(ConcertoInvitationBO element);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        bool InsertMultiple(IEnumerable<ConcertoInvitationBO> elements);

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
