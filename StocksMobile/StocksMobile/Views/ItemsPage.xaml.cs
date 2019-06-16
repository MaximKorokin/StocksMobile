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
    public partial class ItemsPage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public static int ActiveItemId { get; set; }

        public ItemsPage()
        {
            InitializeComponent();
            LoadItems();
        }

        private async void LoadItems()
        {
            string itemsString = await HttpRequest.Get("items/" + StocksPage.ActiveStockId);
            IEnumerable<Item> items = JsonConvert.DeserializeObject<IEnumerable<Item>>(itemsString);

            int row = 0;
            foreach (var item in items)
            {
                Label itemLabel = new Label()
                {
                    Text = item.Name,
                    FontSize = 20,
                    Margin = new Thickness(0, 0, 0, 5),
                };
                itemLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked(item.Id)));

                ItemsGrid.Children.Add(itemLabel, 0, row);

                Button button = new Button()
                {
                    Text = "Move",
                    Margin = new Thickness(5, 0, 0, 0)
                };
                button.Clicked += async (sender, args) =>
                {
                    await RootPage.NavigateFromMenu(1);
                };
                ItemsGrid.Children.Add(button, 1, row++);
            }
        }

        private async void OnLabelClicked(int id)
        {
            await RootPage.NavigateFromMenu(1);
        }

        public async void AddItem(object sender, EventArgs eventArgs)
        {
            await RootPage.NavigateFromMenu((int)MenuItemType.AddItem);
        }
    }
}