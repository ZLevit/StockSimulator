using System;
using System.Windows.Threading;

namespace StockSimulatorApp
{
	public static class DispatcherExtensions
    {
        public static void CheckAccessAndInvoke(this Dispatcher dispatcher, Action action)
        {
            if (dispatcher.CheckAccess())
                action();
            else
                dispatcher.BeginInvoke((Delegate)action);
        }
    }
}
