using StocksMobile.Models;
using StocksMobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StocksMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        private List<HomeMenuItem> menuItems;

        private static MenuPage instance;

        public MenuPage()
        {
            InitializeComponent();
            instance = this;
        }

        public void SetMenuItems(params HomeMenuItem[] menuItems)
        {
            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }

        public static void SetLoggedOutItems()
        {
            instance.SetMenuItems(
                new HomeMenuItem { Id = MenuItemType.Login, Title = TranslateExtension.Translate("login") },
                new HomeMenuItem { Id = MenuItemType.About, Title = TranslateExtension.Translate("about") }
                );
        }

        public static void SetLoggedInUserItems()
        {
            instance.SetMenuItems(
                new HomeMenuItem { Id = MenuItemType.Profile, Title = TranslateExtension.Translate("profile") },
                new HomeMenuItem { Id = MenuItemType.Stocks, Title = TranslateExtension.Translate("mystocks") },
                new HomeMenuItem { Id = MenuItemType.About, Title = TranslateExtension.Translate("about") }
                );
        }

        public static void SetLoggedInAdminItems()
        {
            instance.SetMenuItems(
                new HomeMenuItem { Id = MenuItemType.Profile, Title = TranslateExtension.Translate("profile") },
                new HomeMenuItem { Id = MenuItemType.Administrating, Title = TranslateExtension.Translate("administrating") },
                new HomeMenuItem { Id = MenuItemType.About, Title = TranslateExtension.Translate("about") }
                );
        }
    }
}