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

namespace mainWpf
{
    /// <summary>
    /// Логика взаимодействия для ChartBuilder.xaml
    /// </summary>
    public partial class ChartBuilder : Window
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SL
        {
            public sbyte button_data1;
            public sbyte button_data2;
            public sbyte axisX_p;
            public sbyte axisY_p;
            public sbyte axisW_p;
            public sbyte axisZ_p;
            public sbyte camera_rotate;
            public sbyte manipulator_rotate;
        };

        public static SL vSL;//M<
        public ChartBuilder()
        {
            InitializeComponent();
        }
        private void Chart_Window_Loaded(object sender, RoutedEventArgs e)
        {
            chart.ChartAreas.Add(new ChartArea("Default"));
        }

        private void AxisXChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            chart.Series.Add(new Series("Series1"));
            chart.Series["AxisX"].ChartArea = "Default";
            chart.Series["AxisX"].ChartType = SeriesChartType.Line;
            string[] axisXData = new string[] { "a", "b", "c" };
            double[] axisYData = new double[] { 0.1, 1.5, 1.9 };
            chart.Series["AxisX"].Points.DataBindXY(axisXData, axisYData);

        }
    }
}
