using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Threading;
using System.Windows.Threading;

namespace mainWpf
{
    /// <summary>
    /// Логика взаимодействия для ChartBuilder.xaml
    /// </summary>
    public partial class ChartBuilder : Window
    {
            public static List<List<int>> buttons;
            public static List<sbyte> axisX_p;
            public static List<sbyte> axisY_p;
            public static List<sbyte> axisW_p;
            public static List<sbyte> axisZ_p;
            public static List<sbyte> camera_rotate;
            public static List<sbyte> manipulator_rotate;
            public static List<float> Yaw;
            public static List<float> Pitch;
            public static List<float> Roll;
            public static List<float> Depth;
            public static List<float> Temperature;
            public static List<sbyte> core;
        public static List<string> ReceiveTime;
        public static List<string> SendTime;
        ChartController chartcontroller = new ChartController();
        DispatcherTimer charttimer = new DispatcherTimer();
        public ChartBuilder()
        {
            InitializeComponent();
            
        }
        private void Chart_Window_Loaded(object sender, RoutedEventArgs e)
        {
            buttons = new List<List<int>>();
            axisX_p = new List<sbyte>();
            axisY_p = new List<sbyte>();
            axisW_p = new List<sbyte>();
            axisZ_p = new List<sbyte>();
            camera_rotate = new List<sbyte>();
            manipulator_rotate = new List<sbyte>();
            Yaw = new List<float>();
            Pitch = new List<float>();
            Roll = new List<float>();
            Depth = new List<float>();
            Temperature = new List<float>();
            core = new List<sbyte>();
            SendTime = new List<string>();
            ReceiveTime = new List<string>();
            charttimer.Interval = new TimeSpan(0, 0, 0, 0, 80);
            charttimer.Tick += new EventHandler(ChartUpdate);
            Sendchart.ChartAreas.Add(new ChartArea("Default"));
            Receivechart.ChartAreas.Add(new ChartArea("Receive"));
            Receivechart.Series.Add(new Series("Yaw"));
            Receivechart.Series.Add(new Series("Pitch"));
            Receivechart.Series.Add(new Series("Roll"));
            Receivechart.Series.Add(new Series("Depth"));
            Receivechart.Series.Add(new Series("Temperature"));
            Receivechart.Series.Add(new Series("Core"));
            Sendchart.Series.Add(new Series("AxisY"));
            Sendchart.Series.Add(new Series("AxisX"));
            Sendchart.Series.Add(new Series("AxisW"));
            Sendchart.Series.Add(new Series("AxisZ"));
            Sendchart.Series.Add(new Series("Camera"));
            Sendchart.Series.Add(new Series("Manipulator"));

        }
        #region checkbox
        private void AxisXChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["AxisX"].Enabled = true;
            Sendchart.Series["AxisX"].ChartArea = "Default";
            Sendchart.Series["AxisX"].ChartType = SeriesChartType.Line;
        }

        private void AxisYChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["AxisY"].Enabled = true;
            Sendchart.Series["AxisY"].ChartArea = "Default";
            Sendchart.Series["AxisY"].ChartType = SeriesChartType.Line;
            Sendchart.Series["AxisY"].Color = Color.FromArgb(200, 20, 10);
           
        }

        private void AxisWChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["AxisW"].Enabled = true;
            Sendchart.Series["AxisW"].ChartArea = "Default";
            Sendchart.Series["AxisW"].ChartType = SeriesChartType.Line;
            Sendchart.Series["AxisW"].Color = Color.FromArgb(100, 20, 200);
            
        }

        private void AxisZChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["AxisZ"].Enabled = true;
            Sendchart.Series["AxisZ"].ChartArea = "Default";
            Sendchart.Series["AxisZ"].ChartType = SeriesChartType.Line;
            Sendchart.Series["AxisZ"].Color = Color.FromArgb(200, 20, 200);
        }

        private void CameraChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["Camera"].Enabled = true;
            Sendchart.Series["Camera"].ChartArea = "Default";
            Sendchart.Series["Camera"].ChartType = SeriesChartType.Line;
            Sendchart.Series["Camera"].Color = Color.FromArgb(50, 100, 200);
        }

        private void ManipulatorChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["Manipulator"].Enabled = true;
            Sendchart.Series["Manipulator"].ChartArea = "Default";
            Sendchart.Series["Manipulator"].ChartType = SeriesChartType.Line;
            Sendchart.Series["Manipulator"].Color = Color.FromArgb(200, 200, 10);
        }

        private void YawChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Yaw"].Enabled = true;
            Receivechart.Series["Yaw"].ChartArea = "Receive";
            Receivechart.Series["Yaw"].ChartType = SeriesChartType.Line;
            Receivechart.Series["Yaw"].Color = Color.FromArgb(200, 200, 10);
        }

        private void PitchChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Pitch"].Enabled = true;
            Receivechart.Series["Pitch"].ChartArea = "Receive";
            Receivechart.Series["Pitch"].ChartType = SeriesChartType.Line;
            Receivechart.Series["Pitch"].Color = Color.FromArgb(200, 10, 200);
        }

        private void RollChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Roll"].Enabled = true;
            Receivechart.Series["Roll"].ChartArea = "Receive";
            Receivechart.Series["Roll"].ChartType = SeriesChartType.Line;
            Receivechart.Series["Roll"].Color = Color.FromArgb(200, 10, 10);
        }

        private void DepthChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Depth"].Enabled = true;
            Receivechart.Series["Depth"].ChartArea = "Receive";
            Receivechart.Series["Depth"].ChartType = SeriesChartType.Line;
            Receivechart.Series["Depth"].Color = Color.FromArgb(10, 200, 200);
        }

        private void TemperatureChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Temperature"].Enabled = true;
            Receivechart.Series["Temperature"].ChartArea = "Receive";
            Receivechart.Series["Temperature"].ChartType = SeriesChartType.Line;
            Receivechart.Series["Temperature"].Color = Color.FromArgb(50, 200, 100);
        }

        private void CoreChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Core"].Enabled = true;
            Receivechart.Series["Core"].ChartArea = "Receive";
            Receivechart.Series["Core"].ChartType = SeriesChartType.Line;
            Receivechart.Series["Core"].Color = Color.FromArgb(250, 50, 100);
        }
        #endregion
        private void ReadFromFile_Selected(object sender, RoutedEventArgs e)
        {
            ClearData();
            charttimer.Stop();
            Microsoft.Win32.OpenFileDialog Sentdlg = new Microsoft.Win32.OpenFileDialog();
            Sentdlg.FileName = "SendLog"; // Default file name
            Sentdlg.DefaultExt = ".txt"; // Default file extension
            Sentdlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = Sentdlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = Sentdlg.FileName;
                chartcontroller.ReadSentData(filename);
                Microsoft.Win32.OpenFileDialog Receivedlg = new Microsoft.Win32.OpenFileDialog();
                Receivedlg.FileName = "ReceiveLogLog"; // Default file name
                Receivedlg.DefaultExt = ".txt"; // Default file extension
                Receivedlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> secondresult = Receivedlg.ShowDialog();
                if (secondresult == true)
                {
                    filename = Receivedlg.FileName;
                    chartcontroller.ReadReceivedData(filename);
                    UpdateChart();
                }
            }
            
        }

        private void ReadFromReal_Selected(object sender, RoutedEventArgs e)
        {
            ClearData();
            charttimer.Start();
        }
        private void ChartUpdate(object sender, EventArgs e)
        {
            ChartDataupdate();
            UpdateChart();
        }
        private void ChartDataupdate()
        {
            axisX_p.Add(Model.vGM.axisX_p);
            axisY_p.Add(Model.vGM.axisY_p);
            axisW_p.Add(Model.vGM.axisW_p);
            axisZ_p.Add(Model.vGM.axisZ_p);
            camera_rotate.Add(Model.vGM.camera_rotate);
            manipulator_rotate.Add(Model.vGM.manipulator_rotate);
            SendTime.Add(DateTime.Now.ToLongTimeString());
            Yaw.Add(Model.vSM.yaw);
            Pitch.Add(Model.vSM.pitch);
            Roll.Add(Model.vSM.roll);
            Depth.Add(Model.vSM.depth);
            Temperature.Add(Model.vSM.depth);
            core.Add(Model.vSM.core);
            ReceiveTime.Add(DateTime.Now.ToLongTimeString());
            if (SendTime.Count > 100)
            {
                axisX_p.Remove(axisX_p.First());
                axisY_p.Remove(axisY_p.First());
                axisW_p.Remove(axisW_p.First());
                axisZ_p.Remove(axisZ_p.First());
                camera_rotate.Remove(camera_rotate.First());
                manipulator_rotate.Remove(manipulator_rotate.First());
                SendTime.Remove(SendTime.First());
                Yaw.Remove(Yaw.First());
                Pitch.Remove(Pitch.First());
                Roll.Remove(Roll.First());
                Depth.Remove(Depth.First());
                Temperature.Remove(Temperature.First());
                core.Remove(core.First());
                ReceiveTime.Remove(ReceiveTime.First());
            }
        }
        private void UpdateChart()
        {
            Sendchart.Series["AxisX"].Points.DataBindXY(SendTime, axisX_p);
            Sendchart.Series["AxisY"].Points.DataBindXY(SendTime, axisY_p);
            Sendchart.Series["AxisW"].Points.DataBindXY(SendTime, axisW_p);
            Sendchart.Series["AxisZ"].Points.DataBindXY(SendTime, axisZ_p);
            Sendchart.Series["Camera"].Points.DataBindXY(SendTime, camera_rotate);
            Sendchart.Series["Manipulator"].Points.DataBindXY(SendTime, manipulator_rotate);
            Receivechart.Series["Yaw"].Points.DataBindXY(ReceiveTime, Yaw);
            Receivechart.Series["Pitch"].Points.DataBindXY(ReceiveTime, Pitch);
            Receivechart.Series["Roll"].Points.DataBindXY(ReceiveTime, Roll);
            Receivechart.Series["Depth"].Points.DataBindXY(ReceiveTime, Depth);
            Receivechart.Series["Temperature"].Points.DataBindXY(ReceiveTime, Temperature);
            Receivechart.Series["Core"].Points.DataBindXY(ReceiveTime, core);
        }
        private void ClearData()
        {
            axisX_p.Clear();
            axisY_p.Clear();
            axisW_p.Clear();
            axisZ_p.Clear();
            camera_rotate.Clear();
            manipulator_rotate.Clear();
            Yaw.Clear();
            Pitch.Clear();
            Roll.Clear();
            Depth.Clear();
            Temperature.Clear();
            core.Clear();
            SendTime.Clear();
            ReceiveTime.Clear();
        }

        private void AxisXChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["AxisX"].Enabled = false;
        }

        private void AxisYChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["AxisY"].Enabled = false;
        }

        private void AxisZChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["AxisZ"].Enabled = false;
        }

        private void CameraChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["Camera"].Enabled = false;
        }

        private void ManipulatorChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["Manipulator"].Enabled = false;
        }

        private void YawChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Yaw"].Enabled = false;
        }

        private void PitchChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Pitch"].Enabled = false;
        }

        private void RollChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Roll"].Enabled = false;
        }

        private void DepthChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Depth"].Enabled = false;
        }

        private void TemperatureChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Temperature"].Enabled = false;
        }

        private void CoreChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Receivechart.Series["Core"].Enabled = false;
        }

        private void AxisWChart_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Sendchart.Series["AxisW"].Enabled = false;
        }
    }
}
