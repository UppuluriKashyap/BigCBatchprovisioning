using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigCBatchProvisioning
{
    class NewAccountRequestModel
    {
        public string offer { get; set; }
        public DateTime? effectiveDate { get; set; }
        public DateTime? endDate { get; set; }
        public string accountName { get; set; }
        public string website { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string title { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string userPassword { get; set; }
        public string welcomeEmail { get; set; }
        public CompanyAddress companyAddress { get; set; }
        public List<string> properties { get; set; }
        public bool? acceptAvalaraTermsAndConditions { get; set; }
        public bool? haveReadAvalaraTermsAndConditions { get; set; }
    }
}
