using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furlencode.Core.Model
{
    public class StoreDetails
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }
        public string UpdatedDateTime { get; set; }
        public UserProfile AddedBy { get; set; }
        public List<UserProfile> FlaggedBy { get; set; }
        public List<StoreImages> Images { get; set; }
    }

    public class lList
    {
        public List<StoreDetails> list { get; set; }
    }

    public class uList
    {
        public lList UserList { get; set;}
    }
}
