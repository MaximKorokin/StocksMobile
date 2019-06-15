﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StocksMobile.Models;

namespace StocksMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);

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
            MenuPage.SetLoggedInUserItems();
            await NavigateFromMenu((int)MenuItemType.Profile);
        }

        public async void SetLoggedOutState()
        {
            MenuPage.SetLoggedOutItems();
            await NavigateFromMenu((int)MenuItemType.Login);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.Login:
                        MenuPages.Add(id, new NavigationPage(new LoginPage()));
                        break;
                    case (int)MenuItemType.Profile:
                        MenuPages.Add(id, new NavigationPage(new ProfilePage()));
                        break;
                    case (int)MenuItemType.EditProfile:
                        MenuPages.Add(id, new NavigationPage(new EditProfilePage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}