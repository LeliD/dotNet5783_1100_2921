using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections;

namespace PL
{
    public class ConvertImagePathToBitmap : IValueConverter
    {
        /// <summary>
        /// convert from Image to string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string imageRelativeName = (string)value;
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + imageRelativeName;
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
        /// <summary>
        /// convert from string to Image
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    public class ConvertBooleanToText : IValueConverter //According to the quantity of the product, we will update the catalog if it is in stock or not
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return " ";
            }
            else
            {
                return "Out Of Stock!";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertStatusToProgressBar : IValueConverter
    {
        private static readonly Random s_rand = new();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BO.OrderStatus orderStatus = (BO.OrderStatus)value;
            if(orderStatus== BO.OrderStatus.Delivered)
            {
                return 100;
            }
            if (orderStatus == BO.OrderStatus.Shipped)
            {
                return s_rand.Next(31, 70);
            }
            return s_rand.Next(0, 31);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertStatusToBackgroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (BO.OrderStatus)value;
            if (status==BO.OrderStatus.Ordered)
            {
                return "Pink";
            }
            if (status == BO.OrderStatus.Shipped)
            {
                return "DarkPink";
            }
            else
                return "Red";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
