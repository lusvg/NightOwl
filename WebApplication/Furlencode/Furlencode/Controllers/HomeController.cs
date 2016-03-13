using FurlenCode.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurlenCode.Controllers
{
    public class HomeController : Controller
    {
        string FilePath = "C:\\inetpub\\wwwroot\\FurlenCode\\FurlenCode\\";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddList()
        {
            return View();
        }

        public ActionResult AddUser(string Name, string Address, string Category, bool IsOpen)
        {
            UserListViewModel objUserListViewModel = new UserListViewModel();
            List<UserListViewModel> listObjUserListViewModel = new List<UserListViewModel>();

            objUserListViewModel.Name = Name;
            objUserListViewModel.Address = Address;
            objUserListViewModel.Category = Category;
            objUserListViewModel.StartTime = System.DateTime.Now.Ticks;
            objUserListViewModel.IsOpen = IsOpen;
            string json = "";

            string AddFields = "{ 'UserList':[";
            try
            {
                if (System.IO.File.Exists(@FilePath + "UserList.json"))
                {
                    using (StreamReader r = new StreamReader(FilePath + "UserList.json"))
                    {
                        json = r.ReadToEnd();
                        var list = JsonConvert.DeserializeObject<RootObject>(json);
                        string FileContent = json.Substring(0, json.Length - 4);
                        AddFields = "";
                        AddFields = json.Substring(0, json.Length - 4) + "," + "{UserId: " + list.UserList.Select(x => x.UserId).Max() + 1 
                            + ", Name: '" + objUserListViewModel.Name + "', Address: '" + objUserListViewModel.Address 
                            + "', Category: '" + objUserListViewModel.Category + "', StartTime: '" 
                            + objUserListViewModel.StartTime + "', IsOpen: '" + objUserListViewModel.IsOpen + "'}]}";
                    }
                    System.IO.File.Delete(@FilePath + "UserList.json");
                }
                else
                {
                    AddFields = AddFields + "{UserId: 1" + ", Name: '" + objUserListViewModel.Name + "', Address: '" + objUserListViewModel.Address 
                        + "', Category: '" + objUserListViewModel.Category + "', StartTime: '" + objUserListViewModel.StartTime + "', IsOpen: '"
                        + objUserListViewModel.IsOpen + "'}]}";
                }
            }
            catch (Exception ex)
            {

            }
            using (StreamWriter writer = new StreamWriter(FilePath + "UserList.json", true))
            {
                writer.WriteLine(AddFields);
            }
            return Json(new { jsonContent = AddFields },JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserLogin(string UserName, string Password)
        {
            string str = GetLoginDetail(UserName, Password);
            if(str != "")
            {
                Session["LoggedIn"] = UserName;
            }

            return Json(new { result = str},JsonRequestBehavior.AllowGet);
        }

        public string GetLoginDetail(string UserName, string Password)
        {
            string json = "";
            //UserName = "admin";
            //Password = "admin";
            string IsValid = "";

            if (System.IO.File.Exists(@FilePath + "LoginDetails.json"))
            {
                using (StreamReader r = new StreamReader(FilePath + "LoginDetails.json"))
                {
                    json = r.ReadToEnd();
                    var list = JsonConvert.DeserializeObject<UserLoginListViewModel>(json);
                    if(list.LoginUserList.Where(x => x.UserName == UserName && x.Password == Password).Any())
                    {
                        IsValid = "success";
                    }
                }
            }
            return IsValid;
        }

        public ActionResult GetCategoryList()
        {
            string json = "";
            if (System.IO.File.Exists(@FilePath + "UserList.json"))
            {
                using (StreamReader r = new StreamReader(FilePath + "UserList.json"))
                {
                    json = r.ReadToEnd();
                }
            }
            return Json(new { list = json }, JsonRequestBehavior.AllowGet);
        }
    }
}