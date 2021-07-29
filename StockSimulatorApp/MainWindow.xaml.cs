using StockSimulatorClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace StockSimulatorApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		
		public MainWindow()
		{
			InitializeComponent();

			var provider = StockItemsClient.GetStockItemsProvider();
			DataContext = 	new StocksViewModel(provider);
			provider.Start();
		}

		HistoryWindow _stockHistoryWnd;

		private void SelectedStock_Mouse(object sender, MouseButtonEventArgs e)
		{
			if (_stockHistoryWnd == null)
			{
				_stockHistoryWnd = new HistoryWindow();
				_stockHistoryWnd.DataContext = (DataContext as StocksViewModel).SelectedStock;
				_stockHistoryWnd.Closed += _stockHistoryWnd_Closed;
				_stockHistoryWnd.Show();
			}
			else
				_stockHistoryWnd.DataContext = (DataContext as StocksViewModel).SelectedStock;
		}

		private void _stockHistoryWnd_Closed(object sender, System.EventArgs e)
		{
			_stockHistoryWnd = null;
		}
	}
}
