using GraphExchange.Models;
using GraphExchange.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GraphExchange.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private GraphWebRequest _graphWebRequest = new GraphWebRequest();
        public AccountController()
        {

        }
        [Route("ReadMail")]
        [HttpGet]
        public IHttpActionResult ReadMail()
        {
            string ApiURL = $"https://graph.microsoft.com/v1.0/me/messages";
            OAuthAccessToken oAuthAccessToken = _graphWebRequest.GetAccessToken();

            HttpClient httpClient = new HttpClient();
            var graphApiHandler = new GraphApiHandler(httpClient);
            var result = graphApiHandler.GetEmail(oAuthAccessToken.access_token);
            return Json<Object>(result);
        }
    }
}
