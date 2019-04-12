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
        public void ReadReceivedData(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            string line = "";
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    ReadFloatNumber(line, ChartBuilder.Yaw);
                    ReadFloatNumber(line, ChartBuilder.Pitch);
                    ReadFloatNumber(line, ChartBuilder.Roll);
                    ReadFloatNumber(line, ChartBuilder.Depth);
                    ReadFloatNumber(line, ChartBuilder.Temperature);
                    ReadSBNumber(line, ChartBuilder.core);
                    string time = "";
                    position++;
                    for (; position < line.Length; position++)
                        time += line[position];
                    ChartBuilder.ReceiveTime[ChartBuilder.ReceiveTime.Length - 1] = Convert.ToDateTime(time);
                    position = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex);
            }
        }
        public void ReadSentData(string Path)
        {
            StreamReader sr = new StreamReader(@"ResourseFiles\\Sendlog13h41m42s.txt");
            List<int> String = new List<int>();
            string line = "";
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    ReadSBNumber(line, ChartBuilder.axisX_p);
                    ReadSBNumber(line, ChartBuilder.axisY_p);
                    ReadSBNumber(line, ChartBuilder.axisW_p);
                    ReadSBNumber(line, ChartBuilder.axisZ_p);
                    ReadSBNumber(line, ChartBuilder.manipulator_rotate);
                    ReadSBNumber(line, ChartBuilder.camera_rotate);
                    for (int limit = position + 24; position < limit; position += 2) String.Add(line[position]);
                    Array.Resize(ref ChartBuilder.SendTime, ChartBuilder.SendTime.Length + 1);
                    string time = "";
                    position++;
                    for (; position < line.Length; position++)
                        time += line[position];
                    ChartBuilder.SendTime[ChartBuilder.SendTime.Length - 1] = Convert.ToDateTime(time);
                    ChartBuilder.buttons.Add(String);
                    position = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex);
            }
        }
        private void ReadSBNumber(string line, List<sbyte> list)
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
        private void ReadFloatNumber(string line, List<float> list)
        {
            string number = "";
            while (line[position] != '-')
            {
                number += line[position];
                position++;
            }
            position++;
            list.Add(float.Parse(number));
        }
        private int position
        {
            set;
            get;
        }
    }
}
