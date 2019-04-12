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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;

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
        public static DateTime[] ReceiveTime = new DateTime[0];
        public static DateTime []SendTime = new DateTime[0];
        ChartController chartcontroller = new ChartController();
        public ChartBuilder()
        {
            InitializeComponent();
            buttons = new List<List<int>>();
            axisX_p = new List<sbyte>();
            axisY_p = new List<sbyte>();
            axisW_p = new List<sbyte>();
            axisZ_p = new List<sbyte>();
            camera_rotate = new List<sbyte>();
            manipulator_rotate = new List<sbyte>();
        }
        private void Chart_Window_Loaded(object sender, RoutedEventArgs e)
        {

            chartcontroller.ReadSentData("C:\\Users\\ASUS\\source\repos\\Main\\mainWpf\bin\\Release\\ResourseFiles\\Sendlog13h41m42s.txt");
            chart.ChartAreas.Add(new ChartArea("Default"));
            chart.Series.Add(new Series("AxisX"));
            chart.Series["AxisX"].ChartArea = "Default";
            chart.Series["AxisX"].ChartType = SeriesChartType.Line;
            //chart.Series["AxisX"].Points.AddXY(ChartTime, axisX_p);
            chart.Series["AxisX"].Points.DataBindXY(SendTime,axisX_p);
            
        }
        
        private void AxisXChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            string[] axisXData = new string[] { "a", "b", "c" };
            //sbyte[] axisYData = new double[] { 0.1, 1.5, 1.9 };
            

        }
    }
}
