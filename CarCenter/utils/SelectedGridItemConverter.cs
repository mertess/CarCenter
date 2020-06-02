using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using CarCenterBusinessLogic.ViewModels;

namespace CarCenter.utils
{
    public class SelectedGridItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if(value != null)
                    return ((StorageViewModel)value).StoragedKits;
                else
                    return DependencyProperty.UnsetValue;
            }
            catch (Exception) { return DependencyProperty.UnsetValue; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
