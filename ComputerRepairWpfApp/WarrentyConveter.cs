using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using FinalProject;

namespace FinalWpfApp
{
    /// <summary>
    /// For Highlighting row for warrenty and expired warrent devices in the data grid view.
    /// </summary>
    class WarrentyConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Appointment)
            {
                if (((Appointment)value).Warrenty.Equals("No"))
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.White;
                }
            }
            else
            {
                return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
