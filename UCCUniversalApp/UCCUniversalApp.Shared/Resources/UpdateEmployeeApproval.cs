using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UCCUniversalApp.Model;

namespace UCCUniversalApp.Resources
{
    class UpdateEmployeeApproval
    {
        private HttpClient httpClient;
        private HttpResponseMessage response;
        OnlineURI webURI = new OnlineURI();

        public UpdateEmployeeApproval()
        {

            httpClient = new HttpClient();

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;

            // HttpProductInfoHeaderValueCollection is a collection of 
            // HttpProductInfoHeaderValue items used for the user-agent header

            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

        }

        public async void ChangeApproval(string address, List<object> _empList, int _listLength, string approval)
        {
            response = new HttpResponseMessage();


            Uri resourceUri;
            if (!Uri.TryCreate(address.Trim(), UriKind.Absolute, out resourceUri))
            {
                return; //"Invalid URI, please re-enter a valid URI";

            }
            if (resourceUri.Scheme != "http" && resourceUri.Scheme != "https")
            {
                return; //"Only 'http' and 'https' schemes supported. Please re-enter URI";
            }
            // ---------- end of test---------------------------------------------------------------------

            string responseText;

            try
            {
                int x = 0;
                MultipartFormDataContent content = new MultipartFormDataContent();
                foreach (EmployeeCheckIn emp in _empList)
                {
                    content.Add((new StringContent(emp.CheckID.ToString(), System.Text.Encoding.UTF8, "text/plain")), "CheckID" + x);

                    x++;
                }
                content.Add((new StringContent(approval, System.Text.Encoding.UTF8, "text/plain")), "Approval");
                content.Add((new StringContent(_listLength.ToString(), System.Text.Encoding.UTF8, "text/plain")), "Limit");


                response = await httpClient.PostAsync(resourceUri, content);
                response.EnsureSuccessStatusCode();
                responseText = await response.Content.ReadAsStringAsync();

                return; //responseText;

            }
            catch (Exception)
            {
                // Need to convert int HResult to hex string
                responseText = "";

                return; //responseText;
            }

        }

    }
}
