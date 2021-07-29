using StockSimulator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSimulatorClient
{
	public class StockItemsClient
	{
		public static IStockItemsProvider GetStockItemsProvider()
		{
			var provider = Mef.Container.GetExportedValue<IStockItemsProvider>();
			provider.Start();
			return provider;
		}	
	}
}
