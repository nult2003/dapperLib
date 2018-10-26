using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperRepositoryLib
{
    public class ConcertoInvitationBO
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InvitedEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountCrmId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SenderCrmId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SenderHelloId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SenderEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int InvitedResponse { get; set; }
    }

}
