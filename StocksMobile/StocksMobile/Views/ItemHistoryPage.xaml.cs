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
    public partial class ItemHistoryPage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public ItemHistoryPage()
        {
            InitializeComponent();
            ItemNameLabel.Text = $"{ItemsPage.ActiveItem.Name}:{ItemsPage.ActiveItem.Id} {TranslateExtension.Translate("history")}";
            LoadItemHistory();
        }

        public async void LoadItemHistory()
        {
            string statesString = await HttpRequest.Get($"items/history/{ItemsPage.ActiveItem.Id}");
            IEnumerable<ItemState> states = JsonConvert.DeserializeObject<IEnumerable<ItemState>>(statesString);

            StatesGrid.Children.Add(new Label() { Text = TranslateExtension.Translate("stockid") }, 0, 0);
            StatesGrid.Children.Add(new Label() { Text = TranslateExtension.Translate("date") }, 1, 0);

            int row = 1;
            foreach (var state in states)
            {
                StatesGrid.Children.Add(new Label() { Text = state.StockId.ToString() }, 0, row);
                StatesGrid.Children.Add(new Label() { Text = state.ArrivalDate.ToString() }, 1, row++);
            }
        }

        private async void RemoveItem(object sender, EventArgs e)
        {
            await HttpRequest.Post($"items/remove/{ItemsPage.ActiveItem.Id}", "");
            await RootPage.NavigateFromMenu((int)MenuItemType.Items);
        }
    }
}