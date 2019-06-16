using Newtonsoft.Json;
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
    public partial class AddItemPage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public AddItemPage()
        {
            InitializeComponent();
        }

        private async void AddItem(object sender, EventArgs e)
        {
            double.TryParse(CapacityEntry.Text, out double capacity);

            Item stock = new Item()
            {
                Name = NameEntry.Text,
                Capacity = capacity,
            };
            await HttpRequest.Post("items/add/" + StocksPage.ActiveStockId, JsonConvert.SerializeObject(stock));
            await RootPage.NavigateFromMenu((int)MenuItemType.Items);
        }
    }
}