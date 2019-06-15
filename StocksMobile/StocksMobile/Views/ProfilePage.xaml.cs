using StocksMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StocksMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public ProfilePage()
        {
            InitializeComponent();

            NameLabel.Text = CurrentUserManager.CurrentUser.Name;
        }

        public async void Logout(object sender, EventArgs eventArgs)
        {
            CurrentUserManager.RemoveCurrentUser();
            RootPage.SetLoggedOutState();
        }

        public async void Edit(object sender, EventArgs eventArgs)
        {
            await RootPage.NavigateFromMenu((int)MenuItemType.EditProfile);
        }
    }
}