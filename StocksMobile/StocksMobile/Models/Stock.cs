using Newtonsoft.Json;
using System.Collections.Generic;

namespace StocksMobile.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Capacity { get; set; }
    }
}
