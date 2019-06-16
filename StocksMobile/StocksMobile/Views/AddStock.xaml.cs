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
    public partial class AddStock : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public AddStock()
        {
            InitializeComponent();
        }

        private async void CreateStock(object sender, EventArgs e)
        {
            double.TryParse(CapacityEntry.Text, out double capacity);

            Stock stock = new Stock()
            {
                Name = NameEntry.Text,
                Capacity = capacity,
            };
            await HttpRequest.Post("stocks/add", JsonConvert.SerializeObject(stock));
            await RootPage.NavigateFromMenu((int)MenuItemType.Stocks);
        }
    }
}