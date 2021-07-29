using System.ComponentModel.Composition;
using StockSimulator.Contracts;

namespace StockSimulatorerver.Contracts
{
	[Export(typeof(IStockItem))]
	public class StockItem : IStockItem
	{
		public Observable<string> StockName { get ; set ; }
		public Observable<double> StockPrice { get ; set ; }
	}
}