using Prism.Mvvm;

namespace WalledCityLahore.ViewModels
{
    public abstract class BaseViewModel : BindableBase
    {
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isSearching = false;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { SetProperty(ref _isSearching, value); }
        }
    }
}
