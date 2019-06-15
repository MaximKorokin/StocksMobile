using StocksMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace StocksMobile
{
    public static class CurrentUserManager
    {
        private const string CurrentUserKey = "CurrentUser";

        public static bool IsLoggedIn { get => CurrentUser != null; }

        public static User CurrentUser { get; private set; }

        static CurrentUserManager()
        {
            if (!Preferences.ContainsKey(CurrentUserKey))
                return;
            string userString = Preferences.Get(CurrentUserKey, "");
            CurrentUser = JsonConvert.DeserializeObject<User>(userString);
        }

        public static void SetCurrentUser(User user)
        {
            string userString = JsonConvert.SerializeObject(user);
            Preferences.Set(CurrentUserKey, userString);
            CurrentUser = user;
        }

        public static void RemoveCurrentUser()
        {
            Preferences.Remove(CurrentUserKey);
            CurrentUser = null;
        }
    }
}
