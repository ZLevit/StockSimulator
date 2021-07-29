using StockSimulator.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockSimulatorApp
{
    public class StocksViewModel : INotifyPropertyChanged
    {
        private StockViewModel _selectedStock;        
        public StocksViewModel(IStockItemsProvider provider)
        {
            Stocks = new Collection<StockViewModel>();            
            
			foreach (var stock in provider.StockItems)
			{
                var model = new StockViewModel(stock);
                Stocks.Add(model);
            }            
        }

		public Collection<StockViewModel> Stocks { get; }

        public StockViewModel SelectedStock
        {
            get => _selectedStock;
            set
            {
                if (_selectedStock != value)
                {
                    _selectedStock = value;
                    OnPropertyChanged("SelectedStock");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
