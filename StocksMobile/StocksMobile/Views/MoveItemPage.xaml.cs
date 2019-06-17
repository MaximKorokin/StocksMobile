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
    public partial class MoveItemPage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public MoveItemPage()
        {
            InitializeComponent();
        }

        private async void MoveItem(object sender, EventArgs e)
        {
            double.TryParse(StockIdEntry.Text, out double stockId);
            await HttpRequest.Post($"items/move/{ItemsPage.ActiveItem.Id}/{stockId}", "");
            await RootPage.NavigateFromMenu((int)MenuItemType.Items);
        }
    }
}