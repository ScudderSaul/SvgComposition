using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SvgComposition.Converters
{
    [ValueConversion(typeof(double), typeof(String))]
    class DoubleConverter : IValueConverter
    {
          public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
          {
              double zz= (double)value;
              return (zz.ToString("F", culture));
          }

          public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
          {
              double ww;
              string strValue = value as string;
              if (double.TryParse(strValue, out ww))
              {
                  return (ww);
              }
              return DependencyProperty.UnsetValue;
          }
    }
}
