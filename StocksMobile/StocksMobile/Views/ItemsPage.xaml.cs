using Newtonsoft.Json;
using StocksMobile.Models;
using StocksMobile.Services;
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

        public static Item ActiveItem { get; set; }

        public ItemsPage()
        {
            InitializeComponent();
            StockNameLabel.Text = $"{StocksPage.ActiveStock.Name}:{StocksPage.ActiveStock.Id}";
            LoadItems();
        }

        private async void LoadItems()
        {
            RemoveButton.IsEnabled = false;
            string itemsString = await HttpRequest.Get("items/" + StocksPage.ActiveStock.Id);
            IEnumerable<Item> items = JsonConvert.DeserializeObject<IEnumerable<Item>>(itemsString);

            if (!items.Any())
                RemoveButton.IsEnabled = true;

            int row = 0;
            foreach (var item in items)
            {
                Label itemLabel = new Label()
                {
                    Text = $"{item.Name}:{item.Id}",
                    FontSize = 20,
                    Margin = new Thickness(0, 0, 0, 5),
                };
                itemLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked(item)));

                ItemsGrid.Children.Add(itemLabel, 0, row);

                Button button = new Button()
                {
                    Text = TranslateExtension.Translate("move"),
                    Margin = new Thickness(5, 0, 0, 0)
                };
                button.Clicked += async (sender, args) =>
                {
                    ActiveItem = item;
                    await RootPage.NavigateFromMenu((int)MenuItemType.MoveItem);
                };
                ItemsGrid.Children.Add(button, 1, row++);
            }
        }

        private async void OnLabelClicked(Item item)
        {
            ActiveItem = item;
            await RootPage.NavigateFromMenu((int)MenuItemType.ItemHistory);
        }

        public async void AddItem(object sender, EventArgs eventArgs)
        {
            await RootPage.NavigateFromMenu((int)MenuItemType.AddItem);
        }

        public async void RemoveStock(object sender, EventArgs eventArgs)
        {
            await HttpRequest.Post($"stocks/remove/{StocksPage.ActiveStock.Id}", "");
            await RootPage.NavigateFromMenu((int)MenuItemType.Stocks);
        }
    }
}