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
        private AppConnector _appConnector = new AppConnector();
        public AccountController()
        {

        }
        [Route("ReadMail")]
        [HttpGet]
        public IHttpActionResult ReadMail()
        {
            var result = _appConnector.AcquireToken();
            return Ok();
        }
    }
}
