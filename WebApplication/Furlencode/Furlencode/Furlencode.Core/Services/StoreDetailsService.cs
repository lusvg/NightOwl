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
    public class StoreDetailsService : IStoreDetails
    {
        public async Task<uList> GetAllStores()
        {
            string getLoginURI = new Uri("http://192.168.2.141/localhost/home/getcategorylist").ToString();
            var getLoginDetailRequest = new HttpRequest<uList>(getLoginURI, RequestMethod.GET, null, null, null, false, null, null);
            var serviceModel = await getLoginDetailRequest.GetResponseForJSON();
            return serviceModel;
        }
    }
}
