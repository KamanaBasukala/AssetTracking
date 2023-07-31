using System;
namespace AssetTrackingWeek30
{
    public class Asset
    {
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Office { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PriceInUSD { get; set; }
        public string Currency { get; set; }
        public decimal LocalPriceToday { get; set; }

        public Asset(string type, string brand, string model,string office,
            DateTime purchaseDate, decimal priceInUSD, string currency)
        {
            Type = type;
            Brand = brand;
            Model = model;
            Office = office;
            PurchaseDate = purchaseDate;
            PriceInUSD = priceInUSD;
            Currency = currency;
        }
    }
}

