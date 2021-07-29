using StockSimulator.Contracts;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace StockSimulatorApp
{
     public class StockViewModel : INotifyPropertyChanged, IDisposable
    {
        IStockItem _stockItem;
        private readonly Dispatcher _dispatcher;
        
        public StockViewModel(IStockItem stockItem)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;

            _stockItem = stockItem;
			_stockItem.StockPrice.OnChanged += StockPrice_OnChanged;

            StockName = _stockItem.StockName.Value;
            StockPrice = _stockItem.StockPrice.Value;
            StockTime = DateTime.Now;
            StockPriceChangeStatus = 0;
        }

		private void StockPrice_OnChanged(object sender, Observable<double>.OnChangedEventArgs<double> e)
		{
            _dispatcher.CheckAccessAndInvoke(() =>
           {
               StockPrice = e.newValue;
               OnPropertyChanged("StockPrice");
               StockPriceChangeStatus = GetStockPriceStatus(e);
               OnPropertyChanged("StockPriceChangeStatus");

               StockPrices.Add(new StockPriceViewModel(DateTime.Now, e.newValue, StockPriceChangeStatus));
           });
        }

		private int GetStockPriceStatus(Observable<double>.OnChangedEventArgs<double> e)
		{
			if (e.newValue == e.oldValue)
				return 0;
			else if (e.newValue > e.oldValue)
                return  1;
			else
                return -1;
		}

		public void Dispose()
        {
            _stockItem.StockPrice.OnChanged -= StockPrice_OnChanged;
        }

        public IStockItem Stock => _stockItem;

        public string StockName { get; set; }
        public double StockPrice { get; set; }
		public DateTime StockTime { get; set; }
		public int StockPriceChangeStatus { get; set; }

        public ObservableCollection<StockPriceViewModel> StockPrices { get; } = new ObservableCollection<StockPriceViewModel>();
     
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
