using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GeocomplexCore.Service
{
    class IntToBoolenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                if (value.Equals(1))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                if (value.Equals(1))
                    return true;
                else
                    return false;
            }
            throw new NotImplementedException();
        }
    }
}
