using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furlencode.Core.IServices
{
    public interface ILoginService
    {
        Task<string> GetLoginDetails(string username, string password);
    }
}
