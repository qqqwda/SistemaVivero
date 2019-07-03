using SistemaViveros.API.Helpers;
using SistemaViveros.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SistemaViveros.API.Controllers
{
    public class UsersController : ApiController
    {
        public IHttpActionResult PostUser(UserRequest user)
        {
            var answer = UsersHelper.CreateUserASP(user);
            if (answer.IsSuccess)
            {
                return Ok(answer);
            }
            return BadRequest(answer.Message);
        }
    }
}
