using System.Collections.Generic;
using AutoMapper;

namespace dapperRepositoryLib
{
    public class ProfileService : IProfileService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IEnumerable<ConcertoInvitationBO> GetInvitationByEmail(string email)
        {
            using (var session = new DalSession())
            {
                return Mapper.Map<IEnumerable<ConcertoInvitationBO>>(session.UnitOfWork.ProfileRepository.GetInvitationByEmail(email));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public bool InsertMultiple(IEnumerable<ConcertoInvitationBO> elements)
        {
            var elementsDo = Mapper.Map<IEnumerable<ConcertoInvitationDO>>(elements);
            using (var session = new DalSession())
            {
                session.UnitOfWork.ProfileRepository.InsertMultiple(elementsDo);
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool InsertOrUpdate(ConcertoInvitationBO element)
        {
            var elementDo = Mapper.Map<ConcertoInvitationDO>(element);
            using (var session = new DalSession())
            {
                session.UnitOfWork.ProfileRepository.InsertOrUpdate(elementDo);
                element.Id = elementDo.Id;
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseIds"></param>
        public void SetInvitationAsInactive(IEnumerable<int> responseIds)
        {
            using (var session = new DalSession())
            {
                session.UnitOfWork.ProfileRepository.SetInvitationAsInactive(responseIds);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responses"></param>
        /// <param name="invitedEmail"></param>
        public void SetInvitationResponses(Dictionary<int, bool> responses, string invitedEmail)
        {
            using (var session = new DalSession())
            {
                session.UnitOfWork.ProfileRepository.SetInvitationResponses(responses, invitedEmail);
            }
        }
    }

}
