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
            position = 0;
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
                    ChartBuilder.ReceiveTime.Add(Convert.ToDateTime(time).ToLongTimeString());
                    position = 0;
                }
                CheckReceiveData();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex);
            }
        }
        public void ReadSentData(string Path)
        {
            StreamReader sr = new StreamReader(Path);
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
                    string time = "";
                    position++;
                    for (; position < line.Length; position++)
                        time += line[position];
                    ChartBuilder.SendTime.Add(Convert.ToDateTime(time).ToLongTimeString());
                    position = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex);
            }
            CheckSendData();
        }
        private void CheckSendData()
        {
            int min = ChartBuilder.SendTime.Count;
            if (ChartBuilder.axisX_p.Count > min)
            {
                ChartBuilder.axisX_p.Remove(ChartBuilder.axisX_p.Last());
                CheckSendData();
            }
            if (ChartBuilder.axisY_p.Count > min)
            {
                ChartBuilder.axisY_p.Remove(ChartBuilder.axisY_p.Last());
                CheckSendData();
            }
            if (ChartBuilder.axisW_p.Count > min)
            {
                ChartBuilder.axisW_p.Remove(ChartBuilder.axisW_p.Last());
                CheckSendData();
            }
            if (ChartBuilder.axisZ_p.Count > min)
            { 
                ChartBuilder.axisZ_p.Remove(ChartBuilder.axisZ_p.Last());
                CheckSendData();
            }
            if (ChartBuilder.manipulator_rotate.Count > min)
            {
                ChartBuilder.manipulator_rotate.Remove(ChartBuilder.manipulator_rotate.Last());
                CheckSendData();
            }
            if (ChartBuilder.camera_rotate.Count > min)
            {
                ChartBuilder.camera_rotate.Remove(ChartBuilder.camera_rotate.Last());
                CheckSendData();
            }
        }
        private void CheckReceiveData()
        {
            int min = ChartBuilder.ReceiveTime.Count;
            if (ChartBuilder.Yaw.Count > min)
            {
                ChartBuilder.Yaw.Remove(ChartBuilder.Yaw.Last());
                CheckReceiveData();
            }
            if (ChartBuilder.Pitch.Count > min)
            {
                ChartBuilder.Pitch.Remove(ChartBuilder.Pitch.Last());
                CheckReceiveData();
            }
            if (ChartBuilder.Roll.Count > min)
            {
                ChartBuilder.Roll.Remove(ChartBuilder.Roll.Last());
                CheckReceiveData();
            }
            if (ChartBuilder.Depth.Count > min)
            {
                ChartBuilder.Depth.Remove(ChartBuilder.Depth.Last());
                CheckReceiveData();
            }
            if (ChartBuilder.Temperature.Count > min)
            {
                ChartBuilder.Temperature.Remove(ChartBuilder.Temperature.Last());
                CheckReceiveData();
            }
            if (ChartBuilder.core.Count > min)
            {
                ChartBuilder.core.Remove(ChartBuilder.core.Last());
                CheckReceiveData();
            }
        }
        private void ReadSBNumber(string line, List<sbyte> list)
        {
            string number = "";
            while (line[position] != '!')
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
            while (line[position] != '!')
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
