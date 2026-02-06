using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ScriptPlayer.Converters
{
    public class HeatmapVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return Brushes.Transparent;

            // values[0] = HeatMap (Brush)
            // values[1] = ShowHeatMap (bool)
            
            if (!(values[1] is bool showHeatMap))
                return Brushes.Transparent;

            if (!showHeatMap)
                return Brushes.Transparent;

            if (values[0] is Brush heatmap && heatmap != null && heatmap != Brushes.Transparent)
                return heatmap;

            return Brushes.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
