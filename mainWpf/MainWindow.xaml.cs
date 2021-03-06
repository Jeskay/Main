﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Threading;
using WebCam_Capture;
using System.IO;
namespace mainWpf
{
    //M<
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        internal static MainWindow vMain;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        DispatcherTimer timer3 = new DispatcherTimer();
        DispatcherTimer ClockTimer = new DispatcherTimer();
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        System.Media.SoundPlayer TimeOut1 = new System.Media.SoundPlayer();
        // обьявление используемых клаасов
        public JoystickController Maincontroller;
        public UDPController MainUDP;
        public SettingsController setter;
        public TimerController timercontroller;
        public VTimerModel vtimer;
        public VUDPModel vudp;

        bool mark = false;
        int NoSoundEffects = 0;
        bool TimerIsStoped = false;
        bool SecondLeft = true;
        private WebCamCapture webcam;//M
        private Image _FrameImage;
        public VModel vmodel;
        
        public DateTime dtm;
        public TimeSpan StopRange;

        public MainWindow()
        {
            InitializeComponent();

            //Теперь MainWindow главное окно для ProjectionWindow
            setter          = new SettingsController();
            Maincontroller  = new JoystickController();
            MainUDP         = new UDPController();
            vmodel          = new VModel(new Model());
            vtimer          = new VTimerModel(new TimerModel());
            vudp            = new VUDPModel(new UDPModel());
            timercontroller = new TimerController();
            DataContext = vmodel;
            UDPData_Grid.DataContext = vudp;
            GroupBoxTimer_Grid.DataContext = vtimer;
        }

        public void Joystickthread()//C>
        {
            
            try
            {
                while (true)
                {
                    if (!Maincontroller.GetJoystick)
                    {
                        Maincontroller.UpdateJoystick(vmodel);
                        try
                        {
                            if (Model.vSM.yaw - 90 != vmodel.YawAngle.Angle)
                            {
                                RotateTransform a = new RotateTransform();
                                a.Angle = Model.vSM.yaw - 90;
                                vmodel.YawAngle = a;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Возникло исключение: " + ex);
                        }
                           
                    }
                    string info = "AxisX: " + Model.vGM.axisX_p + "\n";//state.X + "\n";
                    info += "AxisY: " + Model.vGM.axisY_p + "\n";
                    info += "AxisW: " + Model.vGM.axisW_p + "\n";//state.Rz + ";";
                    info += "AxisZ: " + Model.vGM.axisZ_p + "\n";//state.Z + ";";
                    info += "Slighter:  " + Model.vGM.manipulator_rotate + "\n"; //sligterP[0] + ";";
                    info += "PointOfView:   " + Model.vGM.camera_rotate + "\n";
                    for (int i = 0; i < 12; i++) info += "Key" + i + ": " + Maincontroller.GetButtons[i] + "\n";
                    MainUDP.Send(info);

                    vmodel.Depth = Model.vSM.depth;
                    vmodel.Temperature = Model.vSM.temp;
                    vmodel.Roll  = Model.vSM.roll;
                    vmodel.Yaw   = Model.vSM.yaw;
                    vudp.SendingData = info;
                    vudp.ReceivingData = "ReseivedData:" + "\n" + "Yaw:   " + Model.vSM.yaw + "\n" + "Temperature:    " + Model.vSM.temp + "\n" + "Roll:   " + Model.vSM.roll + "\n" + "Depth:   " + Model.vSM.depth;
                    vudp.SendingBytes = MainUDP.SendedBytes;
                    vudp.ReceivingBytes = MainUDP.ReceivedBytes;
                    //ProjectionWindow.Yaw = Model.vSM.yaw;
                    //ProjectionWindow.Diff = Model.vSM.pitch;
                    //ProjectionWindow.Lurch = Model.vSM.roll;
                    Thread.Sleep(20);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение1: " + ex.ToString() + "\n  " + ex.Message);
            }
            
        }//C<
        

        private void timerTick(object sender, EventArgs e)//V>
        {
            if (NoSoundEffects == 0) sp.Play();
        }

        #region timers
        public void ClockTimerTick(object sender, EventArgs e)
        {
            timercontroller.UpdateTimer();
            vtimer.TimeLeft = timercontroller.TimeLeft;
        }
        public void timer2Tick(object sender, EventArgs e)
        {
            MainUDP.Connection = false;
        }
        public void timer3Tick(object sender, EventArgs e)
        {
            Image_IsSound.Visibility = Visibility.Collapsed;
            timer3.Stop();
        }//V<
        #endregion timers
        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void reset_button_Click(object sender, RoutedEventArgs e)//V>
        {
            timercontroller.StartTimer(15);
        }//V<
        class Helper//M>
        {
            //Block Memory Leak
            [System.Runtime.InteropServices.DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr handle);
            public static BitmapSource bs;
            public static IntPtr ip;
            public static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
            {

                ip = source.GetHbitmap();

                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, System.Windows.Int32Rect.Empty,

                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ip);

                return bs;

            }//M<
        }
        private void Stop_button_Click(object sender, RoutedEventArgs e)//V>
        {
            if (vtimer.TimerStopped)
            {
                vtimer.TimerStopped = false;
                vtimer.ButtonContent = "Pause";
                timercontroller.ContinueTimer();
            }
            else
            {
                vtimer.TimerStopped = true;
                vtimer.ButtonContent = "Continue";
                timercontroller.StopTimer();
            }
        }//V<
        void webcam_ImageCaptured(object source, WebcamEventArgs e)//M
        {
            _FrameImage.Source = Helper.LoadBitmap((System.Drawing.Bitmap)e.WebCamImage);
        }

        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            vMain = this;//M

            TimeOut1.Stream =   Properties.Resources.time;//V>
            sp.Stream       =   Properties.Resources.NoSignalSound;

            sp.Load();
            TimeOut1.Load();

            GroupBox_SensorData.BorderBrush = System.Windows.Media.Brushes.Navy;
            GroupBox_Timer.BorderBrush      = System.Windows.Media.Brushes.Navy;
            TextBox1.Visibility             = Visibility.Collapsed;
            ctext.Visibility                = Visibility.Collapsed;
            Image_lantern.Visibility        = Visibility.Collapsed;
            Label_ByteData.Visibility       = Visibility.Collapsed;
            //Label_DephMeter.Visibility    = Visibility.Collapsed;
            Label_SendingBytes.Visibility   = Visibility.Collapsed;//V<
            webcam                          = new WebCamCapture();//C>
           // webcam.CaptureHeight = 100;
           // webcam.CaptureWidth = 100;
            webcam.FrameNumber                  = ((ulong)(0ul));
            webcam.TimeToCapture_milliseconds   = 30;
            webcam.ImageCaptured                += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            _FrameImage                         = ImageWebcam1;

            webcam.Start(0);
            dtm = DateTime.Now;
            dtm = dtm.AddMinutes(15.0);
            
            //включено в класс Controller
            Maincontroller.InitializeJoystick(this);

            MainUDP.Receiver();

            timer.Tick      += new EventHandler(timerTick);
            timer.Interval   = new TimeSpan(0, 0, 1);
            timer2.Tick     += new EventHandler(timer2Tick);
            timer2.Interval  = new TimeSpan(0, 0, 1);
            timer3.Tick     += new EventHandler(timer3Tick);
            timer3.Interval  = new TimeSpan(0, 0, 3);
            ClockTimer.Interval = new TimeSpan(0, 0, 1);
            ClockTimer.Tick += new EventHandler(ClockTimerTick);
            ClockTimer.Start();
            Thread thread1   = new Thread(Joystickthread);
            thread1.Priority = ThreadPriority.Highest;
            thread1.Start();
            //setter.ReadCoefficients("Coefficents.txt");
            timercontroller.StartTimer(15);
        }

        private void Keyboard_KeyUp(object sender, KeyEventArgs e)//V
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.D1)
            {
                if (TextBox1.Visibility == Visibility.Visible) TextBox1.Visibility = Visibility.Collapsed;
                else TextBox1.Visibility = Visibility.Visible;
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.D2)
            {
                if (ctext.Visibility == Visibility.Visible) ctext.Visibility = Visibility.Collapsed;
                else ctext.Visibility = Visibility.Visible;
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.R)//C
            {
                webcam.Stop();
                webcam.Start(0);
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.P)//V
            {
                if (ME_Test.Visibility != Visibility.Visible)
                {
                    ME_Test.Visibility = Visibility.Visible;
                    webcam.Stop();
                    ME_Test.Play();
                }
                else
                {
                    ME_Test.Stop();
                    ME_Test.Visibility = Visibility.Collapsed;
                    webcam.Start(0);
                }
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.F3)//V
            {
                Image_IsSound.Visibility = Visibility.Visible;
                if (NoSoundEffects == 0)
                {
                    NoSoundEffects = 1;
                    Image_IsSound.Source = new BitmapImage(new Uri("Images/NoSound.png", UriKind.Relative));
                    timer3.Start();
                }
                else
                {
                    NoSoundEffects = 0;
                    Image_IsSound.Source = new BitmapImage(new Uri("Images/SoundIcon.png", UriKind.Relative));
                    timer3.Start();
                }
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.D3)//V
            {
                if (Label_ByteData.Visibility == Visibility.Visible) Label_ByteData.Visibility = Visibility.Collapsed;
                else Label_ByteData.Visibility = Visibility.Visible;
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.D4)//V
            {
                if (Label_SendingBytes.Visibility == Visibility.Visible) Label_SendingBytes.Visibility = Visibility.Collapsed;
                else Label_SendingBytes.Visibility = Visibility.Visible;
            }
            if (e.Key == System.Windows.Input.Key.T)//V
            {
                vmodel.FirstDepth = Model.vSM.depth;//+погрешность
            }
        }

        private void MainWin_Closed(object sender, EventArgs e)//V
        {
            Environment.Exit(0);
        }

        private void TextBox_AtmPr_PreviewTextInput_1(object sender, TextCompositionEventArgs e)//V
        {
            e.Handled = !Char.IsDigit(e.Text, 0);
        }

        public void SpeedModeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void TextBox_timer_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
        //вынести изменение состояния соединения и адекватно это реализовать
        private void TextBox_SpeedMode_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (MainUDP.Connection)
            {
                Image_signal.Visibility = Visibility.Visible;
                Image_Nosignal.Visibility = Visibility.Collapsed;
                timer.Stop();
            }
            else
            {
                Image_Nosignal.Visibility = Visibility.Visible;
                Image_signal.Visibility = Visibility.Collapsed;
                timer.Start();
            }
        }
    }
}
