using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerWPF
{
    public class PlotInfo
    {
        public string XValue { get; set; }
        public string YValue { get; set; }
        public int ZValue { get; set; }

        public PlotInfo(string xValue, string yValue, int zValue)
        {
            XValue = xValue;
            YValue = yValue;
            ZValue = zValue;
        }
    }


}
