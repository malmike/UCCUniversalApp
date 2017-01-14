using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UCCUniversalApp.Model;


namespace UCCUniversalApp.Resources
{
    class RetrieveValues
    {
        private HttpClient httpClient;
        private HttpResponseMessage response;

        public RetrieveValues()
        {
            httpClient = new HttpClient();

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;

            // HttpProductInfoHeaderValueCollection is a collection of 
            // HttpProductInfoHeaderValue items used for the user-agent header

            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
        }

        public async Task getValues(string address, string userID)
        {
            response = new HttpResponseMessage();

            Uri resourceUri;
            if (!Uri.TryCreate(address.Trim(), UriKind.Absolute, out resourceUri))
            {
                //return "Invalid URI, please re-enter a valid URI";

            }
            if (resourceUri.Scheme != "http" && resourceUri.Scheme != "https")
            {
                //return "Only 'http' and 'https' schemes supported. Please re-enter URI";
            }
            // ---------- end of test---------------------------------------------------------------------

            string responseText = null;

            try
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add((new StringContent(userID, System.Text.Encoding.UTF8, "text/plain")), "UserID");
                
                response = await httpClient.PostAsync(resourceUri, content);
                response.EnsureSuccessStatusCode();
                responseText = await response.Content.ReadAsStringAsync();
                EmployeeCheckInDataContext context = new EmployeeCheckInDataContext();
                context.storeMultipleElements(responseText);
                //return responseText;

            }
            catch (Exception ex)
            {
                // Need to convert int HResult to hex string
                //responseText = "Error = " + ex.HResult.ToString("X") +
                //    "  Message: " + ex.Message;

                //return responseText;
            }
        }
    }
}
