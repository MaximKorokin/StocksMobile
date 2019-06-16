using System;
using System.Collections.Generic;
using System.Text;

namespace StocksMobile.Models
{
    public enum MenuItemType
    {
        Items,
        About,
        Login,
        Profile,
        EditProfile,
        Administrating,
        Stocks,
        AddStock,
        AddItem,
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
