using Furlencode.Core.Error;
using Furlencode.Core.IServices;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furlencode.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;

        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public string UserName { get; set; }
        public string Password { get; set; }

        public async void Login()
        {
            var a = UserName;
            var b = Password;
            string login = await _loginService.GetLoginDetails(UserName, Password);
            //successfull
            if (string.Equals(login, "success", StringComparison.OrdinalIgnoreCase))
            {
                this.ShowViewModel<FirstViewModel>();
            }
            else
            {
                //Through Error
                ThrowError(new ErrorEventArgs("Cannot login. Please check credentials."));
            }
        }



    }
}
