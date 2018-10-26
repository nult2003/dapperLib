using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperRepositoryLib
{
    public class BLLProfileProfile : AutoMapper.Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public BLLProfileProfile()
        {
            CreateMap<ConcertoInvitationBO, ConcertoInvitationDO>().ReverseMap();
        }
    }
}
