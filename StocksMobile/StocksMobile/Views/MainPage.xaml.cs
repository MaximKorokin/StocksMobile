using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StocksMobile.Models;
using StocksMobile.Services;

namespace StocksMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        private static MainPage instance;
        private static Stack<int> navigationIds = new Stack<int>();

        public MainPage()
        {
            instance = this;

            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Items, (NavigationPage)Detail);

            InitialNavigation();
        }

        private void InitialNavigation()
        {
            if (CurrentUserManager.IsLoggedIn)
            {
                if (CurrentUserManager.CurrentUser.Role == "User")
                    SetLoggedInUserState();
                else
                    SetLoggedInAdminState();
            }
            else
            {
                SetLoggedOutState();
            }
        }

        public async void SetLoggedInUserState()
        {
            MenuPage.SetLoggedInUserItems();
            await NavigateFromMenu((int)MenuItemType.Profile);
        }

        public async void SetLoggedInAdminState()
        {
            MenuPage.SetLoggedInAdminItems();
            await NavigateFromMenu((int)MenuItemType.Profile);
        }

        public async void SetLoggedOutState()
        {
            MenuPage.SetLoggedOutItems();
            await NavigateFromMenu((int)MenuItemType.Login);
        }

        public async Task NavigateFromMenu(int id)
        {
            //if (!MenuPages.ContainsKey(id))
            //{
            //    switch (id)
            //    {
            //        case (int)MenuItemType.Browse:
            //            MenuPages.Add(id, new NavigationPage(new ItemsPage()));
            //            break;
            //        case (int)MenuItemType.About:
            //            MenuPages.Add(id, new NavigationPage(new AboutPage()));
            //            break;
            //        case (int)MenuItemType.Login:
            //            MenuPages.Add(id, new NavigationPage(new LoginPage()));
            //            break;
            //        case (int)MenuItemType.Profile:
            //            MenuPages.Add(id, new NavigationPage(new ProfilePage()));
            //            break;
            //        case (int)MenuItemType.EditProfile:
            //            MenuPages.Add(id, new NavigationPage(new EditProfilePage()));
            //            break;
            //    }
            //}

            //var newPage = MenuPages[id];

            NavigationPage newPage = null;
            navigationIds.Push(id);

            switch (id)
            {
                case (int)MenuItemType.Items:
                    newPage = new NavigationPage(new ItemsPage());
                    break;
                case (int)MenuItemType.About:
                    newPage = new NavigationPage(new AboutPage());
                    break;
                case (int)MenuItemType.Login:
                    newPage = new NavigationPage(new LoginPage());
                    break;
                case (int)MenuItemType.Profile:
                    newPage = new NavigationPage(new ProfilePage());
                    break;
                case (int)MenuItemType.EditProfile:
                    newPage = new NavigationPage(new EditProfilePage());
                    break;
                case (int)MenuItemType.Stocks:
                    newPage = new NavigationPage(new StocksPage());
                    break;
                case (int)MenuItemType.AddStock:
                    newPage = new NavigationPage(new AddStock());
                    break;
                case (int)MenuItemType.AddItem:
                    newPage = new NavigationPage(new AddItemPage());
                    break;
                case (int)MenuItemType.MoveItem:
                    newPage = new NavigationPage(new MoveItemPage());
                    break;
                case (int)MenuItemType.ItemHistory:
                    newPage = new NavigationPage(new ItemHistoryPage());
                    break;
                case (int)MenuItemType.Administrating:
                    Device.OpenUri(new Uri(HttpRequest.BackendUrl + "administrating"));
                    return;
            }

            if (newPage != null)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        public async static void GoBack()
        {
            if (navigationIds.Count > 0)
            {
                navigationIds.Pop();
                await instance.NavigateFromMenu(navigationIds.Pop());
            }
        }
    }
}