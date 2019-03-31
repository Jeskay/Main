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

            public ArrayList buttons;
            public sbyte [] axisX_p;
            public sbyte [] axisY_p;
            public sbyte [] axisW_p;
            public sbyte [] axisZ_p;
            public sbyte [] camera_rotate;
            public sbyte [] manipulator_rotate;
   
        public DateTime []ChartTime;
        public ChartBuilder()
        {
            InitializeComponent();
            
        }
        private void Chart_Window_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i = 0;i < 12; i++) vSL.buttons.AddRange(new int[] { });

            chart.ChartAreas.Add(new ChartArea("Default"));
            chart.Series.Add(new Series("AxisX"));
            chart.Series["AxisX"].ChartArea = "Default";
            chart.Series["AxisX"].ChartType = SeriesChartType.Line;
            //chart.Series["AxisX"].Points.AddXY(ChartTime, vSL.axisX_p);
            //chart.Series["AxisX"].Points.DataBindXY(ChartTime,vSL.axisX_p);
        }
        
        private void AxisXChart_CB_Checked(object sender, RoutedEventArgs e)
        {
            string[] axisXData = new string[] { "a", "b", "c" };
            //sbyte[] axisYData = new double[] { 0.1, 1.5, 1.9 };
            

        }
    }
}
