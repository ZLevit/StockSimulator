using System;

namespace StockSimulatorApp
{
    public class StockPriceViewModel
    {
        public StockPriceViewModel(DateTime time, double price, int priceStatus)
        {            
            StockPrice = price;
            StockTime = time;
            StockPriceChangeStatus = priceStatus;
        }

        public DateTime StockTime { get; }
        public double StockPrice { get; }
        public int StockPriceChangeStatus { get;  }
     
    }
}
