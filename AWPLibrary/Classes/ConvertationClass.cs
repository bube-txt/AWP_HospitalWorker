using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace AWPLibrary.Classes
{
    public class ConvertationClass
    {
        public static string GetFormatedDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        public static string GetFormatedTime(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        }
        public static string GetFormatedTime(TimeSpan dateTime)
        {
            return dateTime.ToString(@"hh\:mm\:ss");
        }
        public static string GetFormatedTimeSpan(TimeSpan timeSpan)
        {
            return timeSpan.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        }
        public static string GetFormatedDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        }
        public static string RichTextToString(FlowDocument richText)
        {
            StringWriter wr = new StringWriter();
            XamlWriter.Save(richText, wr);
            string xaml = wr.ToString();
            return xaml;
        }
    }
}
