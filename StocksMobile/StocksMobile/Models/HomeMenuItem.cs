using System;
using System.Collections.Generic;
using System.Text;

namespace StocksMobile.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Login,
        Profile,
        EditProfile
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
