using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FileExplorer
{
    /// <summary>
    /// Converts a full path to specific image type of a drive, folder or file.
    /// </summary>
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //By default, we presume an image
            var image = "Images/file.png";

            var type = (DirectoryItemType)value;

            switch(type)
            {
                case DirectoryItemType.Drive:
                    image = @"Images\drive.png";
                    break;
                case DirectoryItemType.Folder:
                        image = "Images/folder.png";
                    break;
                case DirectoryItemType.File:
                        image = "Images/file.png";
                    break;
            }

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        static HeaderToImageConverter()
        {
            Instance = new HeaderToImageConverter();
        }
    }
}
