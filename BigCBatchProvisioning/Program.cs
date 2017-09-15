using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Ganss.Excel;
using Newtonsoft.Json;

namespace BigCBatchProvisioning
{
    class Program
    {
        private static HttpRequestMessage CreateNewAccountRequest()
        {
            var request = new HttpRequestMessage();

            request.SetBaseUri("https://cqa-restv2.avalara.net/api/v2/accounts/request");
            request.Headers.Add("Accept", "application/json");

            request.SetBasicAuth("DevSystemAdmin@avalara.com", "kennwort");

            return request;
        }

        private static async Task<OnboardingResponse> callOnboarding(NewAccountRequestModel accountRequest)
        {
            using (var request = CreateNewAccountRequest())
            {
                var json = JsonConvert.SerializeObject(accountRequest);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Method = HttpMethod.Post;
                var response = await HttpClientPool.ClientPool.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
              /*  if (result != null && result.Contains("error"))
                {
                    System.IO.File.WriteAllText(@"C:/git/develop/BigCBatchprovisioning/Error Details.txt", result);
                } */
                var onboardingResponse = JsonConvert.DeserializeObject<OnboardingResponse>(result);
                return onboardingResponse;
            }
        }


        static void Main(string[] args)
        {
            var excel =
                new ExcelMapper(
                        "C:/git/develop/BigCBatchprovisioning/BigCommerceBatchLoad.xlsx");
            var excelData = excel.Fetch<ExcelInputData>().ToList();
            CompanyAddress companyAddress = null;
            NewAccountRequestModel accountRequest = null;
            HttpRequestMessage request = null;
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
                var onboardingResponse = callOnboarding(accountRequest).Result;
                account.AvaTaxSoftwareLicenseKey = onboardingResponse.licenseKey;
                account.taxAvalaraAccountNumber = onboardingResponse.accountId;
            }

        }
    }
}
