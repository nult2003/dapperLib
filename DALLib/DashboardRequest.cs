using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib
{
    public class DashboardRequest
    {
        public int QuoteRequestId { get; set; }
        public string StatusCode { get; set; }
        public DateTime Date { get; set; }
        public string CrmName { get; set; }
        public string CrmId { get; set; }
        
        public string CodeCountry { get; set; }
        public string CodeYear { get; set; }
        public int? CodeId { get; set; }
        public int? CodeVersion { get; set; }
        public string CodeIdentifier { get { return string.Format("{0}{1}{2}.{3}", CodeCountry, CodeYear, CodeId.HasValue ? CodeId.Value.ToString("D6") : "XXXXXX", CodeVersion.HasValue ? CodeVersion.Value.ToString() : "1"); } }
    }
}
