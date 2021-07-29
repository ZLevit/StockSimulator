using System;
using System.Collections.ObjectModel;

namespace StockSimulator.Contracts
{
	public interface IStockItemsProvider
	{
		ObservableCollection<IStockItem> StockItems { get; }

		void Start();		

	}
}
