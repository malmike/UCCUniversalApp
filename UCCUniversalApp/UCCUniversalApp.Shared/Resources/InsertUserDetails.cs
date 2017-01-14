using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace UCCUniversalApp.Resources
{
    class InsertUserDetails
    {
        private HttpClient httpClient;
        private HttpResponseMessage response;

        public InsertUserDetails()
        {
            httpClient = new HttpClient();

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;

            // HttpProductInfoHeaderValueCollection is a collection of 
            // HttpProductInfoHeaderValue items used for the user-agent header

            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
        }
        public async void InsertDetails(string address, string UserID, string EmployeeName, string SupervisorID, string Location, string Reason, string CheckType)
        {
            response = new HttpResponseMessage();
            string responseText;

            Uri resourceUri;
            if (!Uri.TryCreate(address.Trim(), UriKind.Absolute, out resourceUri))
            {
                //return "Invalid URI, please re-enter a valid URI";
                return;

            }
            if (resourceUri.Scheme != "http" && resourceUri.Scheme != "https")
            {
                //return "Only 'http' and 'https' schemes supported. Please re-enter URI";
                return;
            }
            // ---------- end of test---------------------------------------------------------------------

            try
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add((new StringContent(UserID, System.Text.Encoding.UTF8, "text/plain")), "UserID");
                content.Add((new StringContent(EmployeeName, System.Text.Encoding.UTF8, "text/plain")), "EmployeeName");
                content.Add((new StringContent(SupervisorID, System.Text.Encoding.UTF8, "text/plain")), "SupervisorID");
                content.Add((new StringContent(Location, System.Text.Encoding.UTF8, "text/plain")), "Location");
                content.Add((new StringContent(Reason, System.Text.Encoding.UTF8, "text/plain")), "Reason");
                content.Add((new StringContent(CheckType, System.Text.Encoding.UTF8, "text/plain")), "CheckType");

                response = await httpClient.PostAsync(resourceUri, content);
                response.EnsureSuccessStatusCode();
                responseText = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                // Need to convert int HResult to hex string
                //Result.Text = "Error = " + ex.HResult.ToString("X") +
                //    "  Message: " + ex.Message;
                responseText = "";

            }

        }

    }
}
