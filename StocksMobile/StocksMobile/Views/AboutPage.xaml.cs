using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StocksMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            TestMethod();
        }

        private async void TestMethod()
        {
            try
            {
                Test.Text = "Waiting...";
                Test.Text = await HttpRequest.Get("users/test");
            }
            catch(Exception ex)
            {
                Test.Text = ex.Message;
            }
        }
    }
}