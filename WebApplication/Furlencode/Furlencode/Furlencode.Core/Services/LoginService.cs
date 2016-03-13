using Furlencode.Core.IServices;
using Furlencode.Core.Model;
using Furlencode.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furlencode.Core.Services
{
    public class LoginService : ILoginService
    {
        public async Task<string> GetLoginDetails(string username, string password)
        {
            string getLoginURI = new Uri("http://192.168.2.141/localhost/home/getlogindetail").ToString();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("username", username);
            param.Add("password", password);
            var getLoginDetailRequest = new HttpRequest<string>(getLoginURI, RequestMethod.GET, null, param, null, false, null, null);
            string serviceModel = await getLoginDetailRequest.GetResponseForString();

            return serviceModel;
        }


    }
}
