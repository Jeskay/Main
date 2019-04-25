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
            
        ChartController chartcontroller = new ChartController();
        DispatcherTimer charttimer = new DispatcherTimer();
        public ChartBuilder()
        {
            InitializeComponent();
            
        }
        private void Chart_Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChartModel.Buttons = new List<int>[22];
            ChartModel.AxisX_p = new List<sbyte>();
            ChartModel.AxisY_p = new List<sbyte>();
            ChartModel.AxisW_p = new List<sbyte>();
            ChartModel.AxisZ_p = new List<sbyte>();
            ChartModel.Camera_rotate = new List<sbyte>();
            ChartModel.Manipulator_rotate = new List<sbyte>();
            ChartModel.Yaw = new List<float>();
            ChartModel.Pitch = new List<float>();
            ChartModel.Roll = new List<float>();
            ChartModel.Depth = new List<float>();
            ChartModel.Temperature = new List<float>();
            ChartModel.Core = new List<sbyte>();
            ChartModel.SendTime = new List<string>();
            ChartModel.ReceiveTime = new List<string>();
            charttimer.Interval = new TimeSpan(0, 0, 0, 0, 80);
            charttimer.Tick += new EventHandler(ChartUpdate);
            Sendchart.ChartAreas.Add(new ChartArea("Default"));
            Receivechart.ChartAreas.Add(new ChartArea("Receive"));
            Buttonchart.ChartAreas.Add(new ChartArea("Buttons"));
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
            for (int i = 0; i < 22; i++)
            {
                Buttonchart.Series.Add(new Series("button" + i.ToString()));
                ChartModel.Buttons[i] = new List<int>();
            }

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
            charttimer.Stop();
            ClearData();

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
                    SetUserSettings(true);
                }
            }
            
        }
        private void SetUserSettings(bool turn)
        {
            Sendchart.ChartAreas["Default"].CursorX.IsUserSelectionEnabled = turn;
            Sendchart.ChartAreas["Default"].CursorX.IsUserEnabled = turn;
            Buttonchart.ChartAreas["Buttons"].CursorX.IsUserEnabled = turn;
            Buttonchart.ChartAreas["Buttons"].CursorX.IsUserSelectionEnabled = turn;
            Receivechart.ChartAreas["Receive"].CursorX.IsUserEnabled = turn;
            Receivechart.ChartAreas["Receive"].CursorX.IsUserSelectionEnabled = turn;
            Sendchart.ChartAreas["Default"].CursorY.IsUserSelectionEnabled = turn;
            Sendchart.ChartAreas["Default"].CursorY.IsUserEnabled = turn;
            Buttonchart.ChartAreas["Buttons"].CursorY.IsUserEnabled = turn;
            Buttonchart.ChartAreas["Buttons"].CursorY.IsUserSelectionEnabled = turn;
            Receivechart.ChartAreas["Receive"].CursorY.IsUserEnabled = turn;
            Receivechart.ChartAreas["Receive"].CursorY.IsUserSelectionEnabled = turn;

        }
        private void ReadFromReal_Selected(object sender, RoutedEventArgs e)
        {
            ClearData();
            charttimer.Start();
            SetUserSettings(false);
        }
        private void ChartUpdate(object sender, EventArgs e)
        {
            ChartDataupdate();
            UpdateChart();
        }
        private void ChartDataupdate()
        {
            ChartModel.AxisX_p.Add(Model.vGM.axisX_p);
            ChartModel.AxisY_p.Add(Model.vGM.axisY_p);
            ChartModel.AxisW_p.Add(Model.vGM.axisW_p);
            ChartModel.AxisZ_p.Add(Model.vGM.axisZ_p);
            ChartModel.Camera_rotate.Add(Model.vGM.camera_rotate);
            ChartModel.Manipulator_rotate.Add(Model.vGM.manipulator_rotate);
            ChartModel.SendTime.Add(DateTime.Now.ToLongTimeString());
            ChartModel.Yaw.Add(Model.vSM.yaw);
            ChartModel.Pitch.Add(Model.vSM.pitch);
            ChartModel.Roll.Add(Model.vSM.roll);
            ChartModel.Depth.Add(Model.vSM.depth);
            ChartModel.Temperature.Add(Model.vSM.depth);
            ChartModel.Core.Add(Model.vSM.core);
            ChartModel.ReceiveTime.Add(DateTime.Now.ToLongTimeString());
            for (int i = 0; i < 12; i++)
                ChartModel.Buttons[i].Add(JoystickController.GetButtons[i]);
            if (ChartModel.SendTime.Count > 100)
            {
                ChartModel.AxisX_p.Remove(ChartModel.AxisX_p.First());
                ChartModel.AxisY_p.Remove(ChartModel.AxisY_p.First());
                ChartModel.AxisW_p.Remove(ChartModel.AxisW_p.First());
                ChartModel.AxisZ_p.Remove(ChartModel.AxisZ_p.First());
                ChartModel.Camera_rotate.Remove(ChartModel.Camera_rotate.First());
                ChartModel.Manipulator_rotate.Remove(ChartModel.Manipulator_rotate.First());
                ChartModel.SendTime.Remove(ChartModel.SendTime.First());
                ChartModel.Yaw.Remove(ChartModel.Yaw.First());
                ChartModel.Pitch.Remove(ChartModel.Pitch.First());
                ChartModel.Roll.Remove(ChartModel.Roll.First());
                ChartModel.Depth.Remove(ChartModel.Depth.First());
                ChartModel.Temperature.Remove(ChartModel.Temperature.First());
                ChartModel.Core.Remove(ChartModel.Core.First());
                ChartModel.ReceiveTime.Remove(ChartModel.ReceiveTime.First());
                for (int i = 0; i < 12; i++)
                    ChartModel.Buttons[i].Remove(ChartModel.Buttons[i].First());
            }
        }
        private void UpdateChart()
        {
            Sendchart.Series["AxisX"].Points.DataBindXY(ChartModel.SendTime, ChartModel.AxisX_p);
            Sendchart.Series["AxisY"].Points.DataBindXY(ChartModel.SendTime, ChartModel.AxisY_p);
            Sendchart.Series["AxisW"].Points.DataBindXY(ChartModel.SendTime, ChartModel.AxisW_p);
            Sendchart.Series["AxisZ"].Points.DataBindXY(ChartModel.SendTime, ChartModel.AxisZ_p);
            Sendchart.Series["Camera"].Points.DataBindXY(ChartModel.SendTime, ChartModel.Camera_rotate);
            Sendchart.Series["Manipulator"].Points.DataBindXY(ChartModel.SendTime, ChartModel.Manipulator_rotate);
            Receivechart.Series["Yaw"].Points.DataBindXY(ChartModel.ReceiveTime, ChartModel.Yaw);
            Receivechart.Series["Pitch"].Points.DataBindXY(ChartModel.ReceiveTime, ChartModel.Pitch);
            Receivechart.Series["Roll"].Points.DataBindXY(ChartModel.ReceiveTime, ChartModel.Roll);
            Receivechart.Series["Depth"].Points.DataBindXY(ChartModel.ReceiveTime, ChartModel.Depth);
            Receivechart.Series["Temperature"].Points.DataBindXY(ChartModel.ReceiveTime, ChartModel.Temperature);
            Receivechart.Series["Core"].Points.DataBindXY(ChartModel.ReceiveTime, ChartModel.Core);
            for (int i = 0; i < 22; i++)
                Buttonchart.Series["button" + i.ToString()].Points.DataBindXY(ChartModel.SendTime, ChartModel.Buttons[i]);
        }
        private void ClearData()
        {
            ChartModel.AxisX_p.Clear();
            ChartModel.AxisY_p.Clear();
            ChartModel.AxisW_p.Clear();
            ChartModel.AxisZ_p.Clear();
            ChartModel.Camera_rotate.Clear();
            ChartModel.Manipulator_rotate.Clear();
            ChartModel.Yaw.Clear();
            ChartModel.Pitch.Clear();
            ChartModel.Roll.Clear();
            ChartModel.Depth.Clear();
            ChartModel.Temperature.Clear();
            ChartModel.Core.Clear();
            ChartModel.SendTime.Clear();
            ChartModel.ReceiveTime.Clear();
            for (int i = 0; i < 22; i++) ChartModel.Buttons[i].Clear();
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

        private void FirstButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button0"].Enabled = false;
        }

        private void FirstButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button0"].Enabled = true;
            Buttonchart.Series["button0"].ChartArea = "Buttons";
            Buttonchart.Series["button0"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button0"].Color = Color.FromArgb(200, 10, 10);
        }

        private void SecondButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button1"].Enabled = true;
            Buttonchart.Series["button1"].ChartArea = "Buttons";
            Buttonchart.Series["button1"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button1"].Color = Color.FromArgb(10, 200, 10);
        }

        private void ThirdButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button2"].Enabled = true;
            Buttonchart.Series["button2"].ChartArea = "Buttons";
            Buttonchart.Series["button2"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button2"].Color = Color.FromArgb(10, 10, 200);
        }

        private void FourthButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button3"].Enabled = true;
            Buttonchart.Series["button3"].ChartArea = "Buttons";
            Buttonchart.Series["button3"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button3"].Color = Color.FromArgb(200, 200, 10);
        }

        private void FifthButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button4"].Enabled = true;
            Buttonchart.Series["button4"].ChartArea = "Buttons";
            Buttonchart.Series["button4"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button4"].Color = Color.FromArgb(200, 10, 200);
        }

        private void SixthButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button5"].Enabled = true;
            Buttonchart.Series["button5"].ChartArea = "Buttons";
            Buttonchart.Series["button5"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button5"].Color = Color.FromArgb(10, 200, 200);
        }

        private void SevethButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button6"].Enabled = true;
            Buttonchart.Series["button6"].ChartArea = "Buttons";
            Buttonchart.Series["button6"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button6"].Color = Color.FromArgb(235, 20, 177);
        }

        private void EighthButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button7"].Enabled = true;
            Buttonchart.Series["button7"].ChartArea = "Buttons";
            Buttonchart.Series["button7"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button7"].Color = Color.FromArgb(116, 19, 235);
        }

        private void NinthButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button8"].Enabled = true;
            Buttonchart.Series["button8"].ChartArea = "Buttons";
            Buttonchart.Series["button8"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button8"].Color = Color.FromArgb(224, 125, 11);
        }

        private void TenthButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button9"].Enabled = true;
            Buttonchart.Series["button9"].ChartArea = "Buttons";
            Buttonchart.Series["button9"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button9"].Color = Color.FromArgb(11, 224, 100);
        }

        private void EleventhButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button10"].Enabled = true;
            Buttonchart.Series["button10"].ChartArea = "Buttons";
            Buttonchart.Series["button10"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button10"].Color = Color.FromArgb(70, 40, 20);
        }

        private void TwelvethButton_CB_Checked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button11"].Enabled = true;
            Buttonchart.Series["button11"].ChartArea = "Buttons";
            Buttonchart.Series["button11"].ChartType = SeriesChartType.Line;
            Buttonchart.Series["button11"].Color = Color.FromArgb(70, 105, 14);
        }

        private void SecondButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button1"].Enabled = false;
        }

        private void ThirdButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button2"].Enabled = false;
        }

        private void FourthButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button3"].Enabled = false;
        }

        private void FifthButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button4"].Enabled = false;
        }

        private void SixthButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button5"].Enabled = false;
        }

        private void SevethButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button6"].Enabled = false;
        }

        private void EighthButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button7"].Enabled = false;
        }

        private void NinthButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button8"].Enabled = false;
        }

        private void TenthButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button9"].Enabled = false;
        }

        private void EleventhButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button10"].Enabled = false;
        }

        private void TwelvethButton_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            Buttonchart.Series["button11"].Enabled = false;
        }
    }
}
