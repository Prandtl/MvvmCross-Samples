using System;
using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Navigation;

namespace ValueConversion.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public HomeViewModel(IMvxNavigationService navigationService)
        {

            Items = new List<MenuItem>
                {
                    new MenuItem(new StringsViewModel(), this),
                    new MenuItem(new DatesViewModel(), this),
                    new MenuItem(new ColorsViewModel(), this),
                    new MenuItem(new VisibilityViewModel(), this),
                    new MenuItem(new TwoWayViewModel(), this),
                };

            _navigationService = navigationService;
        }

        public List<MenuItem> Items { get; private set; }

        private void Show(IMvxViewModel viewModel)
        {
            _navigationService.Navigate(viewModel);
        }

        public class MenuItem
        {
            private readonly HomeViewModel _parent;

            public MenuItem(IMvxViewModel viewModel, HomeViewModel parent)
            {
                Name = viewModel.GetType().Name.Replace("ViewModel", "");
                ViewModel = viewModel;
                _parent = parent;
            }

            public string Name { get; private set; }
            public IMvxViewModel ViewModel { get; private set; }

            public ICommand ShowCommand
            {
                get { return new MvxCommand(() => _parent.Show(ViewModel)); }
            }
        }
    }
}