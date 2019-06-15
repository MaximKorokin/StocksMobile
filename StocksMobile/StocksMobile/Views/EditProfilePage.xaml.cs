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
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage()
        {
            InitializeComponent();

            LanguagePicker.SelectedItem = CurrentUserManager.CurrentUser.Language;
            NameEntry.Text = CurrentUserManager.CurrentUser.Name;
        }
    }
}