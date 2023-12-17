using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class HttpContextHelper
    {
        private IHttpContextAccessor _httpContextAccessor;

        public HttpContextHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetLoggedInUser()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
