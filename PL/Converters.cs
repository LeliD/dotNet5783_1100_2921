using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PL
{
    public class ConvertImagePathToBitmap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string ImageRelativeName = (string)value;
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + ImageRelativeName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }
            catch(Exception ex)
            {
                string ImageRelativeName = @"\Images\IMG.png";//jkjhjggnghhhhhhhh
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + ImageRelativeName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ImageRelativeName=(string)value;
            return ImageRelativeName;
        }
    }
}
