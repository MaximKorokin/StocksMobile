using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;
using StocksMobile.Models;

namespace StocksMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void Login(object sender, EventArgs args)
        {
            User user = new User() { Name = LoginEntry.Text, Password = PasswordEntry.Text };
            string stringUser = JsonConvert.SerializeObject(user);
            try
            {
                string requestStringUser = await HttpRequest.Post("users/authenticate", stringUser);
                User requestUser = JsonConvert.DeserializeObject<User>(requestStringUser);
                if (!string.IsNullOrWhiteSpace(requestUser.Token))
                {
                    Preferences.Set(Constants.CurrentUserKey, requestUser.Token);
                    await DisplayAlert("Success", $"You have successfully logged in with user id: {requestUser.Id}", "Ok");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}