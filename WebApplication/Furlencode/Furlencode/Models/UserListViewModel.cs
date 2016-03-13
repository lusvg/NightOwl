using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurlenCode.Models
{
    public class UserListViewModel
    {
        public  int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public long StartTime { get; set; }
        public bool IsOpen { get; set; }
    }

    public class RootObject
    {
        public List<UserListViewModel> UserList { get; set; }
    }

    public class UserLoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginListViewModel
    {
        public List<UserLoginViewModel> LoginUserList { get; set; }
    }
}