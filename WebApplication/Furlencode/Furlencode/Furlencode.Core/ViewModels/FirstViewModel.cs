using Furlencode.Core.IServices;
using Furlencode.Core.Model;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using System;

namespace Furlencode.Core.ViewModels
{
    public class FirstViewModel 
        : BaseViewModel
    {
        private readonly IStoreDetails _StoreDetailsService;

        public FirstViewModel(IStoreDetails StoreDetailsService)
        {
            _StoreDetailsService = StoreDetailsService;
            GetAllStores();
        }

        private async void GetAllStores()
        {
            var allStores = await _StoreDetailsService.GetAllStores();
        }

        public ObservableCollection<StoreDetails> Stores { get; set; }



    }
}
