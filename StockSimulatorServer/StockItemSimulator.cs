using System;
using System.ComponentModel.Composition;
using StockSimulator.Contracts;

namespace StockSimulatorerver.Contracts
{

	[Export(typeof(IStockItemSimulator))]
	public class StockItemSimulator : IStockItemSimulator
	{		
		public StockItemSimulator()
		{
		}

		public Tuple<double, double> StockItemRange { get; set; }
		public IStockItem StockIem { get; set ; }

		public void PollStockItem()
		{
			if (StockIem != null)
				StockIem.StockPrice.Value = new Random().NextDoubleRange(StockItemRange.Item1, StockItemRange.Item2);
		}
	}
}