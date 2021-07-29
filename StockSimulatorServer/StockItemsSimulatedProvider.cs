using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace StockSimulator.Contracts
{
	[Export(typeof(IStockItemsProvider))]
	public class StockItemsSimulatedProvider : IStockItemsProvider
	{

		#region Implementation

		[Import(typeof(IStockItem))]
		protected ExportFactory<IStockItem> StockItemFactory { get; set; }

		[Import(typeof(IStockItemSimulator))]
		protected ExportFactory<IStockItemSimulator> StockItemSimulatorFactory { get; set; }


		private readonly ObservableCollection<IStockItem> _stockItems;
		private readonly Collection<IStockItemSimulator> _stockSimulators;
		private Task _stockSimulationTask;


		[ImportingConstructor]
		public StockItemsSimulatedProvider(ExportFactory<IStockItem> stockItemFactory, ExportFactory<IStockItemSimulator> stockItemSimulatorFactory)
		{
			StockItemFactory = stockItemFactory;
			StockItemSimulatorFactory = stockItemSimulatorFactory;

			_stockItems = new ObservableCollection<IStockItem>();
			_stockSimulators = new Collection<IStockItemSimulator>();

			CreateStock("Stock1", 240, 270);
			CreateStock("Stock2", 180, 210);

		}

		private void CreateStock(string stockName, double minValue, double maxValue)
		{
			if (minValue < 0.0)
				throw new ArgumentException("minValue < 0");

			if (maxValue < 0.0)
				throw new ArgumentException("maxValue < 0");

			if (minValue > maxValue)
				throw new ArgumentException("minValue > maxValue");

			var stockItemSimulator = StockItemSimulatorFactory.CreateExport().Value;
			var stockItem = StockItemFactory.CreateExport().Value;

			stockItem.StockName = new Observable<string>(stockName);
			stockItem.StockPrice = new Observable<double>(0.0);
			stockItemSimulator.StockIem = stockItem;
			stockItemSimulator.StockItemRange = new Tuple<double, double>(minValue, maxValue);
			stockItemSimulator.PollStockItem();

			_stockSimulators.Add(stockItemSimulator);
			_stockItems.Add(stockItem);	
		}

		
		#endregion

		#region IStockItemsProvider implementation

		public void Start()
		{
			if (_stockSimulationTask != null)
				return;

		   _stockSimulationTask = Task.Factory.StartNew(() =>
		   {
			   System.Diagnostics.Trace.WriteLine("started");

			   while (true)
			   {
				   
				   var stockSimulators = _stockSimulators;
				   foreach (var sim in stockSimulators)
				   {
					   sim.PollStockItem();
				   }
				   System.Threading.Thread.Sleep(1000);
			   }
		   });
		}

		

		public ObservableCollection<IStockItem> StockItems => _stockItems;


		#endregion
	}
}
