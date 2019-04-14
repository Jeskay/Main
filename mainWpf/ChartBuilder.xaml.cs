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
        public static string[] ReceiveTime = new string[0];
        public static string []SendTime = new string[0];
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
            charttimer.Interval = new TimeSpan(0, 0, 0, 0, 55);
            charttimer.Tick += new EventHandler(ChartUpdate);
            chart.ChartAreas.Add(new ChartArea("Default"));
            chart.Series.Add(new Series("AxisY"));
            chart.Series.Add(new Series("AxisX"));
            chart.Series.Add(new Series("AxisW"));
            chart.Series.Add(new Series("AxisZ"));
        }
        
        private void AxisXChart_CB_Checked(object sender, RoutedEventArgs e)
        {
           
            chart.Series["AxisX"].ChartArea = "Default";
            chart.Series["AxisX"].ChartType = SeriesChartType.Line;
        }

        private void AxisYChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            
            chart.Series["AxisY"].ChartArea = "Default";
            chart.Series["AxisY"].ChartType = SeriesChartType.Line;
            chart.Series["AxisY"].Color = Color.FromArgb(200, 20, 10);
           
        }

        private void AxisWChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            
            chart.Series["AxisW"].ChartArea = "Default";
            chart.Series["AxisW"].ChartType = SeriesChartType.Line;
            chart.Series["AxisW"].Color = Color.FromArgb(100, 20, 200);
            
        }

        private void AxisZChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            
            chart.Series["AxisZ"].ChartArea = "Default";
            chart.Series["AxisZ"].ChartType = SeriesChartType.Line;
            chart.Series["AxisZ"].Color = Color.FromArgb(200, 20, 200);
        }

        private void ReadFromFile_Selected(object sender, RoutedEventArgs e)
        {
            chartcontroller.ReadSentData("C:\\Users\\ASUS\\source\repos\\Main\\mainWpf\bin\\Release\\ResourseFiles\\Sendlog13h41m42s.txt");
            chartcontroller.ReadReceivedData("ff");
            chart.ChartAreas.Add(new ChartArea("Default"));
        }

        private void ReadFromReal_Selected(object sender, RoutedEventArgs e)
        { 
            axisX_p.Clear();
            axisY_p.Clear();
            axisW_p.Clear();
            axisZ_p.Clear();
            camera_rotate.Clear();
            manipulator_rotate.Clear();
            Array.Clear(SendTime, 0, SendTime.Length);
            charttimer.Start();
        }
        private void ChartUpdate(object sender, EventArgs e)
        {
            ChartDataupdate();
            chart.Series["AxisX"].Points.DataBindXY(SendTime, axisX_p);
            chart.Series["AxisY"].Points.DataBindXY(SendTime, axisY_p);
            chart.Series["AxisW"].Points.DataBindXY(SendTime, axisW_p);
            chart.Series["AxisZ"].Points.DataBindXY(SendTime, axisZ_p);
        }
        private void ChartDataupdate()
        {
            axisX_p.Add(Model.vGM.axisX_p);
            axisY_p.Add(Model.vGM.axisY_p);
            axisW_p.Add(Model.vGM.axisW_p);
            axisZ_p.Add(Model.vGM.axisZ_p);
            camera_rotate.Add(Model.vGM.camera_rotate);
            manipulator_rotate.Add(Model.vGM.manipulator_rotate);
            Array.Resize(ref SendTime, SendTime.Length + 1);
            SendTime[SendTime.Length - 1] = DateTime.Now.ToLongTimeString();

        }
    }
}
