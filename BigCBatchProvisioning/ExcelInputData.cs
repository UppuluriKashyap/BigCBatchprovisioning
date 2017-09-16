using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using Ganss.Excel;

namespace BigCBatchProvisioning
{
    class ExcelInputData
    {
        public string storeId { get; set; }
        public string storeHash { get; set; }
        [Column("merchant_name")]
        public string merchant_name { get; set; }
        [Column("merchant_website")]
        public string merchant_website { get; set; }
        [Column("merchant_hq_street")]
        public string merchant_hq_street { get; set; }
        [Column("merchant_hq_city")]
        public string merchant_hq_city { get; set; }
        [Column("merchant_hq_state")]
        public string merchant_hq_state { get; set; }
        [Column("Updated Merchant_HQ_State")]
        public string updatedMerchantHqState { get; set; }
        [Column("merchant_hq_country")]
        public string merchant_hq_country{ get; set; }
        [Column("merchant_hq_postalcode")]
        public string merchant_hq_postalcode{ get; set; }
        [Column("contact_first_name")]
        public string contact_first_name{ get; set; }
        [Column("contact_last_name")]
        public string contact_last_name{ get; set; }
        [Column("contact_title")]
        public string contact_title{ get; set; }
        [Column("contact_email")]
        public string contact_email{ get; set; }
        [Column("contact_phone")]
        public string contact_phone { get; set; }
        [Column("contact_street")]
        public string contact_street{ get; set; }
        [Column("contact_city")]
        public string contact_city { get; set; }
        [Column("contact_state")]
        public string contact_state{ get; set; }
        [Column("contact_country")]
        public string contact_country{ get; set; }
        [Column("contact_postalcode")]
        public string contact_postalcode { get; set; }
        [Column("taxAvalaraAccountNumber")]
        public string taxAvalaraAccountNumber { get; set; }
        [Column("AvaTax Software License Key")]
        public string avaTaxSoftwareLicenseKey { get; set; }
        [Column("Error Message")]
        public string errorMessage { get; set; }
    }
}
