using System;
using System.Windows.Data;
using System.Windows.Media;

namespace StockSimulatorApp
{
    public class StockPriceChangeToColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int change = (int) value;
            switch (change)
            {
                case 1:
                    return Green;
                case -1:
                    return Red;
                case 0:
                    return White;
                default:
                    throw new ArgumentOutOfRangeException("Price Change");
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        private static readonly SolidColorBrush Red = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush Green = new SolidColorBrush(Colors.Green);
        private static readonly SolidColorBrush White = new SolidColorBrush(Colors.White);
    }
}
