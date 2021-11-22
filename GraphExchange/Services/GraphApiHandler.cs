using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GraphExchange.Services
{
    public class GraphApiHandler
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient">HttpClient used to call the protected API</param>
        public GraphApiHandler(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        protected HttpClient HttpClient { get; private set; }

        public async Task ReadEmailAsync(string webApiUrl, string accessToken)
        {
            HttpResponseMessage response = null;
            if (!string.IsNullOrEmpty(accessToken))
            {
                var defaultRequestHeaders = HttpClient.DefaultRequestHeaders;
                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                try
                {
                    response = await HttpClient.GetAsync(webApiUrl);
                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject result = JsonConvert.DeserializeObject(json) as JObject;
                    //processResult(result);
                }
                else
                {
                    string content = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task GetEmail(string accessToken)
        {
            HttpClient hc = new HttpClient();
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            HttpResponseMessage hrm = await hc.GetAsync("https://graph.microsoft.com/v1.0/me/messages");

            string rez = await hrm.Content.ReadAsStringAsync();
        }

        private List<Object> processResult (JObject result)
        {
            List<Object> oDataCollection = new List<object>();
            foreach (JProperty child in result.Properties().Where(p => !p.Name.StartsWith("@")))
            {
                oDataCollection.Add($"{child.Name} = {child.Value}");
            }
            return oDataCollection;
        }
    }
}