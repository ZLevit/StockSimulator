using System;

namespace StockSimulator.Contracts
{
	public interface IStockItemSimulator : IStockItemGenerator
	{
		Tuple<double, double> StockItemRange { get; set; }
	}
}