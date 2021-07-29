using System.ComponentModel.Composition.Hosting;

namespace StockSimulatorClient
{
	public static class Mef
	{
		private static CompositionContainer container;

		public static CompositionContainer Container
		{
			get
			{
				if (container == null)
				{
					var catalog =
						new DirectoryCatalog(".", "StockSimulator.Component.*");

					container = new CompositionContainer(catalog);					
				}

				return container;
			}
		}
	}
}
