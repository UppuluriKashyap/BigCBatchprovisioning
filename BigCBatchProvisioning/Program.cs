using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
        /// <summary>
        /// Creates the Onboarding API request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static HttpRequestMessage CreateOnboardingAPIRequest(string url, string userName, string password)
        {
            var request = new HttpRequestMessage();

            request.SetBaseUri(url);
            request.Headers.Add("Accept", "application/json");
            request.SetBasicAuth(userName, password);

            return request;
        }

        /// <summary>
        /// Calls the Onboarding API to provision the account.
        /// </summary>
        /// <param name="accountRequest"></param>
        /// <param name="url"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static async Task<OnboardingResponse> callOnboarding(NewAccountRequestModel accountRequest, string url, string userName, string password)
        {
            using (var request = CreateOnboardingAPIRequest(url, userName, password)) {

                var json = JsonConvert.SerializeObject(accountRequest);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Method = HttpMethod.Post;
                var onboardingResponse = new OnboardingResponse();
                var response = await HttpClientPool.ClientPool.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (result != null && result.Contains("error")) {
                    onboardingResponse.errorMessage = result;
                    return onboardingResponse;
                }
                onboardingResponse = JsonConvert.DeserializeObject<OnboardingResponse>(result);
                return onboardingResponse;
            }

        }

        static void Main(string[] args)
        {
            // Reading the username and password from the user
            Console.Write("Enter url: ");
            string url = Console.ReadLine();
            Console.Write("Enter the username: ");
            string userName = Console.ReadLine();
            Console.Write("Enter the password: ");
            string password = Console.ReadLine();

            // Starting the batch provisioning
            Console.WriteLine("Starting BigC batch provisioning");
            Console.WriteLine("Merchant Name =======================  AccountId =================== LicenseKey");
            try {
                var excel =
                    new ExcelMapper(
                        "C:/git/develop/BigCBatchprovisioning/BC Migration Customers - 10 percent.xlsx");
                var actualSheetName = "Sheet1";
                var excelData = excel.Fetch<ExcelInputData>().ToList();
                List<ExcelInputData> results = new List<ExcelInputData>();
                foreach (var account in excelData) {
                    var companyAddress = new CompanyAddress
                    {
                        line = account.merchant_hq_street,
                        city = account.merchant_hq_city,
                        region = account.updatedMerchantHqState,
                        country = account.merchant_hq_country,
                        postalCode = account.merchant_hq_postalcode
                    };
                    var accountRequest = new NewAccountRequestModel()
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
                        companyCode = account.company_code,
                        acceptAvalaraTermsAndConditions = true,
                        haveReadAvalaraTermsAndConditions = true
                    };
                    var onboardingResponse = callOnboarding(accountRequest, url, userName, password).Result;
                    account.taxAvalaraAccountNumber = onboardingResponse.accountId;
                    account.avaTaxSoftwareLicenseKey = onboardingResponse.licenseKey;
                    account.errorMessage = onboardingResponse.errorMessage;
                    Console.WriteLine(account.merchant_name + " ======================= " + onboardingResponse.accountId + " =================== " + onboardingResponse.licenseKey);
                    results.Add(account);
                }

                // Update this with the actual sheet name befor shipping this.

                excel.Save("C:/git/develop/BigCBatchprovisioning/BC Migration Customers - 10 percent.xlsx", results, actualSheetName);

                Console.WriteLine("Completed batch provisioning.");
                Console.Read();

            }
            catch (AggregateException) {
                Console.WriteLine("Service Unavailable. Please try again later.");
                Console.Read();
            }
            catch (IOException) {
                Console.WriteLine("The excel sheet is open. Please close the excel sheet and try again.");
                Console.Read();
            }
        }
    }
}
