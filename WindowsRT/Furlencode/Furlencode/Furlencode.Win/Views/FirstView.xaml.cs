using Furlencode.Core.ViewModels;
using MvvmCross.WindowsCommon.Views;

namespace Furlencode.Win.Views
{
    public sealed partial class FirstView : BasePage
    {
        public FirstView()
        {
            this.InitializeComponent();
        }

        public new FirstViewModel ViewModel
        {
            get { return (FirstViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
