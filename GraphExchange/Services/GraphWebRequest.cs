using Newtonsoft.Json;
using GraphExchange.Models;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace GraphExchange.Services
{
    public class GraphWebRequest
    {
        public OAuthAccessToken GetAccessToken()
        {
            OAuthAccessToken token = null;

            WebRequest request = WebRequest.Create("https://login.microsoftonline.com/afad13b0-38aa-4e5c-8170-2ec2440bbdb8/oauth2/v2.0/token");

            request.Method = "POST";

            string postData = "grant_type=client_credentials&client_id=71a2b13d-398e-4ce2-9994-2bf00720a982&client_secret=8iO7Q~~k3xdYW79XCD5K6Iu-bClVMtm922VtW&scope=https://graph.microsoft.com/.default";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            if (((HttpWebResponse)response).StatusDescription == "OK")
            {
                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);

                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();

                    token = JsonConvert.DeserializeObject<OAuthAccessToken>(responseFromServer);
                }
            }
            response.Close();
            return token;
        }
    }
}