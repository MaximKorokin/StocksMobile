using StocksMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace StocksMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StocksPage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public static int ActiveStockId { get; set; }

        public StocksPage()
        {
            InitializeComponent();
            LoadStocks();
        }

        private async void LoadStocks()
        {
            string stocksString = await HttpRequest.Get("stocks");
            IEnumerable<Stock> stocks = JsonConvert.DeserializeObject<IEnumerable<Stock>>(stocksString);

            foreach (var stock in stocks)
            {
                Label stockLabel = new Label()
                {
                    Text = stock.Name,
                    FontSize = 20,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                stockLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked(stock.Id)));

                StocksStack.Children.Add(stockLabel);
            }
        }

        private async void OnLabelClicked(int id)
        {
            ActiveStockId = id;
            await RootPage.NavigateFromMenu((int)MenuItemType.Items);
        }

        public async void AddStock(object sender, EventArgs eventArgs)
        {
            await RootPage.NavigateFromMenu((int)MenuItemType.AddStock);
        }
    }
}