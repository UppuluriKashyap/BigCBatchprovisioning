using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigCBatchProvisioning
{
    class OnboardingResponse
    {
        public string accountId { get; set; }
        public string accountDetailsEmailedTo { get; set; }
        public string createdDate { get; set; }
        public string emailedDate { get; set; }
        public string licenseKey { get; set; }
        public string errorMessage { get; set; }
    }
}
