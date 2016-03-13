using Furlencode.Core.Error;
using Furlencode.Core.ViewModels;
using MvvmCross.WindowsCommon.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace Furlencode.Win.Views
{
    public class BasePage : MvxWindowsPage
    {
        public BasePage()
        {

        }
        public new BaseViewModel ViewModel
        {
            get { return (BaseViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ViewModel.EvntThrowError == null)
            {
                ViewModel.EvntThrowError += new EventHandler((object sender, EventArgs ea) =>
                {
                     ShowError((ErrorEventArgs)ea);

                });
            }
        }

        private async void ShowError(ErrorEventArgs errorEventArgs)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog(string.Join("\n", errorEventArgs.Errors), "Error");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Ok",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }
        private void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            //rootPage.NotifyUser("The '" + command.Label + "' command has been selected.",
            //    NotifyType.StatusMessage);

        }





    }
}
