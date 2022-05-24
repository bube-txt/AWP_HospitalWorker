using AWPLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace AWPApp.Model
{
    public partial class Record
    {
        public string GetFormatedRecordDate
        {
            get
            {
                return ConvertationClass.GetFormatedDate(RecordDate);
            }
        }
        public string GetRecordText
        {
            get
            {
                FlowDocument doc = XamlReader.Parse(RecordText) as FlowDocument;
                TextRange range = new TextRange(doc.ContentStart, doc.ContentEnd);
                return range.Text;
            }
        }
    }
}
