using System;
namespace AssetTrackingWeek30
{
    public class ExchangeRateResponse
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public decimal[] Rates { get; set; }
    }
}

