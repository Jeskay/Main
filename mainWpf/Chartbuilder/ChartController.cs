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
                    ReadFloatNumber(line, ChartModel.Yaw);
                    ReadFloatNumber(line, ChartModel.Pitch);
                    ReadFloatNumber(line, ChartModel.Roll);
                    ReadFloatNumber(line, ChartModel.Depth);
                    ReadFloatNumber(line, ChartModel.Temperature);
                    ReadSBNumber(line, ChartModel.Core);
                    string time = "";
                    position++;
                    for (; position < line.Length; position++)
                        time += line[position];
                    ChartModel.ReceiveTime.Add(Convert.ToDateTime(time).ToLongTimeString());
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
            string line = "";
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    ReadSBNumber(line, ChartModel.AxisX_p);
                    ReadSBNumber(line, ChartModel.AxisY_p);
                    ReadSBNumber(line, ChartModel.AxisW_p);
                    ReadSBNumber(line, ChartModel.AxisZ_p);
                    ReadSBNumber(line, ChartModel.Manipulator_rotate);
                    ReadSBNumber(line, ChartModel.Camera_rotate);
                    int count = 0;
                    for (int limit = position + 44; position < limit; position += 2)
                    {
                        ChartModel.Buttons[count].Add(line[position]);
                        count++;
                    }
                    string time = "";
                    position++;
                    for (; position < line.Length; position++)
                        time += line[position];
                    ChartModel.SendTime.Add(Convert.ToDateTime(time).ToLongTimeString());
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
            int min = ChartModel.SendTime.Count;
            if (ChartModel.AxisX_p.Count > min)
            {
                ChartModel.AxisX_p.Remove(ChartModel.AxisX_p.Last());
                CheckSendData();
            }
            if (ChartModel.AxisY_p.Count > min)
            {
                ChartModel.AxisY_p.Remove(ChartModel.AxisY_p.Last());
                CheckSendData();
            }
            if (ChartModel.AxisW_p.Count > min)
            {
                ChartModel.AxisW_p.Remove(ChartModel.AxisW_p.Last());
                CheckSendData();
            }
            if (ChartModel.AxisZ_p.Count > min)
            {
                ChartModel.AxisZ_p.Remove(ChartModel.AxisZ_p.Last());
                CheckSendData();
            }
            if (ChartModel.Manipulator_rotate.Count > min)
            {
                ChartModel.Manipulator_rotate.Remove(ChartModel.Manipulator_rotate.Last());
                CheckSendData();
            }
            if (ChartModel.Camera_rotate.Count > min)
            {
                ChartModel.Camera_rotate.Remove(ChartModel.Camera_rotate.Last());
                CheckSendData();
            }
            for (int i = 0; i < 22; i++)
                if (ChartModel.Buttons[i].Count > min)
                {
                    ChartModel.Buttons[i].Remove(ChartModel.Buttons[i].Last());
                    CheckReceiveData();
                }
        }
        private void CheckReceiveData()
        {
            int min = ChartModel.ReceiveTime.Count;
            if (ChartModel.Yaw.Count > min)
            {
                ChartModel.Yaw.Remove(ChartModel.Yaw.Last());
                CheckReceiveData();
            }
            if (ChartModel.Pitch.Count > min)
            {
                ChartModel.Pitch.Remove(ChartModel.Pitch.Last());
                CheckReceiveData();
            }
            if (ChartModel.Roll.Count > min)
            {
                ChartModel.Roll.Remove(ChartModel.Roll.Last());
                CheckReceiveData();
            }
            if (ChartModel.Depth.Count > min)
            {
                ChartModel.Depth.Remove(ChartModel.Depth.Last());
                CheckReceiveData();
            }
            if (ChartModel.Temperature.Count > min)
            {
                ChartModel.Temperature.Remove(ChartModel.Temperature.Last());
                CheckReceiveData();
            }
            if (ChartModel.Core.Count > min)
            {
                ChartModel.Core.Remove(ChartModel.Core.Last());
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
