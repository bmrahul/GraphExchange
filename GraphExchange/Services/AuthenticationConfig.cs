using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web.Helpers;
using System.Web.Http.Results;

namespace GraphExchange.Services
{
    public class AuthenticationConfig
    {
        public string Instance { get; set; }
        public string ApiUrl { get; set; }
        public string Tenant { get; set; }
        public string ClientId { get; set; }
        public string Authority
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, Instance, Tenant, "/oauth2/token");
            }
        }
        public string ClientSecret { get; set; }
        public string CertificateName { get; set; }
    }
}