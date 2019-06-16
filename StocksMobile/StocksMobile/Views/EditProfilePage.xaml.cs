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
    public partial class EditProfilePage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public EditProfilePage()
        {
            InitializeComponent();

            LanguagePicker.SelectedItem = CurrentUserManager.CurrentUser.Language;
            NameEntry.Text = CurrentUserManager.CurrentUser.Name;
        }

        public async void Edit(object sender, EventArgs args)
        {
            User newUser = new User()
            {
                Language = LanguagePicker.SelectedItem.ToString(),
                Name = NameEntry.Text,
                Password = OldPasswordEntry.Text == RepeatOldPasswordEntry.Text ? NewPasswordEntry.Text : null,
                Role = CurrentUserManager.CurrentUser.Role,
                Id = CurrentUserManager.CurrentUser.Id,
                Token = CurrentUserManager.CurrentUser.Token,
            };

            await HttpRequest.Post("users/edit", JsonConvert.SerializeObject(newUser));
            CurrentUserManager.SetCurrentUser(newUser);
            await RootPage.NavigateFromMenu((int)MenuItemType.Profile);
        }
    }
}