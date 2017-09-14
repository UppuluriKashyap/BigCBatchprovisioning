using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Ganss.Excel;

namespace BigCBatchProvisioning
{
    class Program
    {
        static void Main(string[] args)
        {
            var excelData =
                new ExcelMapper(
                        "C:/Users/kashyap.uppuluri/Documents/visual studio 2015/Projects/ConsoleApplication1/BigCommerceBatchLoad.xlsx")
                    .Fetch<ExcelInputData>().ToList();
            CompanyAddress companyAddress = null;
            NewAccountRequestModel accountRequest = null;
            foreach (var account in excelData)
            {
                companyAddress = new CompanyAddress
                {
                    line = account.merchant_hq_street,
                    city = account.merchant_hq_city,
                    region = account.merchant_hq_state,
                    country = account.merchant_hq_country,
                    postalCode = account.merchant_hq_postalcode
                };
                accountRequest = new NewAccountRequestModel()
                {
                    offer = "BigCommerceAvataxIncluded",
                    accountName = account.merchant_name,
                    firstName = account.contact_first_name,
                    lastName = account.contact_last_name,
                    title = account.contact_title,
                    phoneNumber = account.contact_phone,
                    email = account.contact_email,
                    welcomeEmail = "Custom",
                    companyAddress = companyAddress,
                    acceptAvalaraTermsAndConditions = true,
                    haveReadAvalaraTermsAndConditions = true
                };



            }

        }
    }
}
