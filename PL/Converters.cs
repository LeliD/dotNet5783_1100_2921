﻿using System;
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
        /// convert from string to Image
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
            catch(Exception)
            {
                string ImageRelativeName = @"\Images\IMG.png";
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + ImageRelativeName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    public class ConvertBooleanToText : IValueConverter //According to the quantity of the product, we will update the catalog if it is in stock or not
    {
        /// <summary>
        /// Convert Boolean To Text
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
                return " ";

            else
                return "Out Of Stock!";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertStatusToProgressBar : IValueConverter
    {
        private static readonly Random s_rand = new();
        /// <summary>
        /// Convert Status To ProgressBar
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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
            return s_rand.Next(0, 31);//ordered
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertStatusToBackgroundColor : IValueConverter
    {
        /// <summary>
        /// Convert Status To Background Color
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (BO.OrderStatus)value;
            if (status==BO.OrderStatus.Ordered)
            {
                return "PaleVioletRed";
            }
            if (status == BO.OrderStatus.Shipped)
            {
                return "HotPink";
            }
            else
                return "Pink";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
