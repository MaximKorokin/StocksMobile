using System;
using System.Collections.Generic;
using System.Text;

namespace StocksMobile.Models
{
    class ItemState
    {
        public int ItemId { get; set; }
        public int StockId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int? ItemStateId { get; set; }
    }
}
