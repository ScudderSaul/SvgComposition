using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SvgComposition.Converters
{
    [ValueConversion(typeof(Color), typeof(String))]
  public  class PlacementColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            return (ColorConverter.ConvertFromString(strValue));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color cc = (Color)value;
            return (cc.ToString());
           
        }
    }
}
