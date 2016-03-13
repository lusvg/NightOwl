using Furlencode.Core.Error;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furlencode.Core.ViewModels
{
    public class BaseViewModel :MvxViewModel
    {

        public EventHandler EvntThrowError;
        public void ThrowError(ErrorEventArgs args)
        {
            LogError(args);
            if (EvntThrowError != null)
                EvntThrowError(this, args);
        }
        protected void LogError(ErrorEventArgs args)
        {
            if (args.IsNetworkConnectivityError)
            {
                // UpdateNetworkConnectivity();       
            }

            if (args.IsAuthorizationError)
            {
                // LogAndLogoutUser(args.Errors.FirstOrDefault());
            }
            //Error is passed to UI for logging in Crtittercism using UI SDK
            LogErrorInUI(args);
        }

        public EventHandler EvntLogError;
        protected void LogErrorInUI(ErrorEventArgs args)
        {
            if (EvntLogError != null)
            {
                EvntLogError(this, args);
            }
        }
    }
}
