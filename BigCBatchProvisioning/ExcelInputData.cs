﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigCBatchProvisioning
{
    class ExcelInputData
    {
        public string merchant_name
        { get; set; }
        public string merchant_website
        { get; set; }
        public string merchant_hq_street
        { get; set; }
        public string merchant_hq_city
        { get; set; }
        public string merchant_hq_state
        { get; set; }
        public string merchant_hq_country
        { get; set; }
        public string merchant_hq_postalcode
        { get; set; }
        public string contact_first_name
        { get; set; }
        public string contact_last_name
        { get; set; }
        public string contact_title
        { get; set; }
        public string contact_email
        { get; set; }
        public string contact_phone
        { get; set; }
        public string contact_street
        { get; set; }
        public string contact_city
        { get; set; }
        public string contact_state
        { get; set; }
        public string contact_country
        { get; set; }
        public string contact_postalcode { get; set; }
        public bool? acceptAvalaraTermsAndConditions { get; set; }
        public bool? haveReadAvalaraTermsAndConditions { get; set; }
    }
}