namespace StockSimulator.Contracts
{
	public interface IStockItemGenerator
	{
		IStockItem StockIem { get; set;  }
		void PollStockItem();
	}
}