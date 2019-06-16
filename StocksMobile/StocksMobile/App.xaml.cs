using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StocksMobile.Services;
using StocksMobile.Views;
using StocksMobile.Models;

namespace StocksMobile
{
    public partial class App : Application
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
