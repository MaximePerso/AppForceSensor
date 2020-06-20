using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static K2000Rs232App.MainWindow;

namespace K2000Rs232App
{
    public class ClassConfiguration
    {
        public List<ClassForThreadMeasure> ListOfXmlId1 { get; set; }
        public List<ClassForThreadMeasure> ListOfXmlId2 { get; set; }
        public List<ClassForThreadMeasure> ListOfXmlId3 { get; set; }
        public List<ClassForThreadMeasure> ListOfXmlId4 { get; set; }
        public List<ClassForData> ListPos  { get; set; }
        public List<ClassAverageMeasure> ListAver { get; set; }
    }
}
