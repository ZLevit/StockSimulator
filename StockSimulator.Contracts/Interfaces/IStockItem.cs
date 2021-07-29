namespace StockSimulator.Contracts
{
	public interface IStockItem
	{
		Observable<string> StockName { get; set; }
		Observable<double> StockPrice { get; set; }
	}
}
