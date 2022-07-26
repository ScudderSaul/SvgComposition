using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SvgComposition.Converters
{
    [ValueConversion(typeof(int), typeof(String))]
   public class IntConverter : IValueConverter
    {
          public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
          {
              int ii = (int) value;
              return (ii.ToString(culture));
          }

          public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
          {
              int qq;
              string strValue = value as string;
              if (int.TryParse(strValue, out qq))
              {
                  return (qq);
              }
              return DependencyProperty.UnsetValue;
          }
    }
}
