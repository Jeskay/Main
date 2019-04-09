using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainWpf
{
    public class ChartController
    {
        public void ReadData(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            List<int> String = new List<int>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                ReadNumber(line, ChartBuilder.axisX_p);
                ReadNumber(line, ChartBuilder.axisY_p);
                ReadNumber(line, ChartBuilder.axisW_p);
                ReadNumber(line, ChartBuilder.axisZ_p);
                ReadNumber(line, ChartBuilder.manipulator_rotate);
                ReadNumber(line, ChartBuilder.camera_rotate);
                for (int limit = position + 24; position < limit; position+=2) String.Add(line[position]);
                Array.Resize( ref ChartBuilder.ChartTime, ChartBuilder.ChartTime.Length + 1);
                string time = "";
                for (; position <= line.Length; position++)
                    time += line[position];
                ChartBuilder.ChartTime[ChartBuilder.ChartTime.Length - 1] = Convert.ToDateTime(time);
            }
        }
        private void ReadNumber(string line, List<sbyte> list)
        {
            string number = "";
            while (line[position] != '-')
            {
                number += line[position];
                position++;
            }
            position++;
            list.Add(Convert.ToSByte(number));
        }
        private int position
        {
            set;
            get;
        }
    }
}
