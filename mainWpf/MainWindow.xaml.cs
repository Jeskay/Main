using System;
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
                    info += "AxisZ: " + Model.vGM.JRZ_p + "\n";//state.Rz + ";";
                    info += "Lever: " + Model.vGM.axisZ_p + "\n";//state.Z + ";";
                    info += "Slighter:  " + Model.vGM.slighter_p + "\n"; //sligterP[0] + ";";
                    info += "PointOfView:   " + Model.vGM.manipulator_p + "\n";
                    for (int i = 0; i < 12; i++) info += "Key" + i + ": " + Maincontroller.GetButtons[i] + "\n";
                    MainUDP.Send(info);

                    vmodel.Depth = Model.vSM.depth;
                    vmodel.Pitch = Model.vSM.pitch;
                    vmodel.Roll  = Model.vSM.roll;
                    vmodel.Yaw   = Model.vSM.yaw;
                    vudp.SendingData = info;
                    vudp.ReceivingData = "ReseivedData:" + "\n" + "Yaw:   " + Model.vSM.yaw + "\n" + "Pitch:    " + Model.vSM.pitch + "\n" + "Roll:   " + Model.vSM.roll + "\n" + "Depth:   " + Model.vSM.depth;
                    vudp.SendingBytes = MainUDP.SendedBytes;
                    vudp.ReceivingBytes = MainUDP.ReceivedBytes;
                    Thread.Sleep(60);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение1: " + ex.ToString() + "\n  " + ex.Message);
            }
            
        }//C<
        private void Timerthread()//V>
        {
            bool MARK = false;
            while (true)
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                   (ThreadStart)delegate ()
                   {
                       if (!TimerIsStoped) TextBox_timer.Text = (Int16)(dtm.Subtract(DateTime.Now)).TotalHours + ":" + (Int16)((dtm.Subtract(DateTime.Now)).Minutes) + ":" + (Int16)(dtm.Subtract(DateTime.Now)).Seconds;
                       if ((dtm.Subtract(DateTime.Now).Minutes < 1 && !SecondLeft))
                       {
                           TextBox_timer.Background = new SolidColorBrush(Color.FromArgb(0, 255, 12, 12));
                           SecondLeft = true;
                       }
                       else if ((dtm.Subtract(DateTime.Now).Minutes < 4 && SecondLeft) || (mark && SecondLeft))
                       {
                           if (!MARK)
                           {
                               TimeOut1.Play();
                               MARK = true;
                           }
                           TextBox_timer.Background = Brushes.Crimson;
                           SecondLeft = false;
                       }
                   }
                   );
                Thread.Sleep(100);
            }
        }//V<

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
            /*
            dtm = DateTime.Now;
            dtm = dtm.AddMinutes(15.0);
            TextBox_timer.Background = new SolidColorBrush(Color.FromArgb(0, 255, 12, 12));
            */
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
                /*
            if (!TimerIsStoped)
            {
                StopRange = DateTime.Now.Subtract(dtm);
                TimerIsStoped = true;
                Pause_button.Content = "Continue";
            }
            else if (TimerIsStoped)
            {
                dtm = DateTime.Now - StopRange;
                timer.Start();
                TimerIsStoped = false;
                Pause_button.Content = "Pause";
            }*/
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

            webcam.FrameNumber                  = ((ulong)(0ul));
            webcam.TimeToCapture_milliseconds   = 30;
            webcam.ImageCaptured                += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            _FrameImage                         = ImageWebcam1;

            webcam.Start(0);
            try
            {
                Model.AirPressure = Convert.ToInt16(TextBox_AtmPr.Text);
            }
            catch (Exception ex)
            {
                Model.AirPressure = 0;
                Console.WriteLine("Возникло исключение: " + ex);
            }

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
            Thread thread2   = new Thread(Timerthread);
            thread2.Priority = ThreadPriority.Lowest;
            //thread2.Start();
            setter.ReadCoefficients("Coefficents.txt");
            timercontroller.StartTimer(15);
        }

        private void Keyboard_KeyUp(object sender, KeyEventArgs e)//V
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.D1)
            {
                ctext.Margin = new Thickness(4, 12, 0, 0);
                if (TextBox1.Visibility == Visibility.Visible) TextBox1.Visibility = Visibility.Collapsed;
                else TextBox1.Visibility = Visibility.Visible;
                if (TextBox1.Visibility == Visibility.Visible) ctext.Margin = new Thickness(122, 12, 0, 0);
                else ctext.Margin = new Thickness(4, 12, 0, 0);
                if (ctext.Visibility == Visibility.Visible && TextBox1.Visibility == Visibility.Visible) Label_ByteData.Margin = new Thickness(240, 12, 0, 0);
                else if (ctext.Visibility != Visibility.Visible ^ TextBox1.Visibility != Visibility.Visible) Label_ByteData.Margin = new Thickness(122, 12, 0, 0);
                else Label_ByteData.Margin = new Thickness(4, 12, 0, 0);
                if (Label_ByteData.Visibility == Visibility.Visible) Label_SendingBytes.Margin = new Thickness(Label_ByteData.Margin.Left, 86, 0, 0);
                else if (TextBox1.Visibility == Visibility.Visible && ctext.Visibility == Visibility.Visible) Label_SendingBytes.Margin = new Thickness(240, 12, 0, 0);
                else if (TextBox1.Visibility != Visibility.Visible ^ ctext.Visibility != Visibility.Visible) Label_SendingBytes.Margin = new Thickness(122, 12, 0, 0);
                else Label_SendingBytes.Margin = new Thickness(4, 12, 0, 0);
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.D2)
            {
                
                if (ctext.Visibility == Visibility.Visible) ctext.Visibility = Visibility.Collapsed;
                else ctext.Visibility = Visibility.Visible;
                if (ctext.Text == "") ctext.Text = "NO DATA RECEIVED";
                if (TextBox1.Visibility == Visibility.Visible) ctext.Margin = new Thickness(122, 12, 0, 0);
                else ctext.Margin = new Thickness(4, 12, 0, 0);
                if (ctext.Visibility == Visibility.Visible && TextBox1.Visibility == Visibility.Visible) Label_ByteData.Margin = new Thickness(240, 12, 0, 0);
                else if (ctext.Visibility != Visibility.Visible ^ TextBox1.Visibility != Visibility.Visible) Label_ByteData.Margin = new Thickness(122, 12, 0, 0);
                else Label_ByteData.Margin = new Thickness(4, 12, 0, 0);
                if (Label_ByteData.Visibility == Visibility.Visible) Label_SendingBytes.Margin = new Thickness(Label_ByteData.Margin.Left, 86, 0, 0);
                else if (TextBox1.Visibility == Visibility.Visible && ctext.Visibility == Visibility.Visible) Label_SendingBytes.Margin = new Thickness(240, 12, 0, 0);
                else if (TextBox1.Visibility != Visibility.Visible ^ ctext.Visibility != Visibility.Visible) Label_SendingBytes.Margin = new Thickness(122, 12, 0, 0);
                else Label_SendingBytes.Margin = new Thickness(4, 12, 0, 0);
                
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
                if (ctext.Visibility == Visibility.Visible && TextBox1.Visibility == Visibility.Visible) Label_ByteData.Margin = new Thickness(240, 12, 0, 0);
                else if (ctext.Visibility != Visibility.Visible ^ TextBox1.Visibility != Visibility.Visible) Label_ByteData.Margin = new Thickness(122, 12, 0, 0);
                else Label_ByteData.Margin = new Thickness(4, 12, 0, 0);
                if (Label_ByteData.Visibility == Visibility.Visible) Label_SendingBytes.Margin = new Thickness(Label_ByteData.Margin.Left, 86, 0, 0);
                else if (TextBox1.Visibility == Visibility.Visible && ctext.Visibility == Visibility.Visible) Label_SendingBytes.Margin = new Thickness(240, 12, 0, 0);
                else if (TextBox1.Visibility != Visibility.Visible ^ ctext.Visibility != Visibility.Visible) Label_SendingBytes.Margin = new Thickness(122, 12, 0, 0);
                else Label_SendingBytes.Margin = new Thickness(4, 12, 0, 0);                
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.D4)//V
            {
                
                if (Label_SendingBytes.Visibility == Visibility.Visible) Label_SendingBytes.Visibility = Visibility.Collapsed;
                else Label_SendingBytes.Visibility = Visibility.Visible;
                if (ctext.Visibility == Visibility.Visible && TextBox1.Visibility == Visibility.Visible) Label_ByteData.Margin = new Thickness(240, 12, 0, 0);
                else if (ctext.Visibility != Visibility.Visible ^ TextBox1.Visibility != Visibility.Visible) Label_ByteData.Margin = new Thickness(122, 12, 0, 0);
                else Label_ByteData.Margin = new Thickness(4, 12, 0, 0);
                if (Label_ByteData.Visibility == Visibility.Visible) Label_SendingBytes.Margin = new Thickness(Label_ByteData.Margin.Left, 86, 0, 0);
                else if (TextBox1.Visibility == Visibility.Visible && ctext.Visibility == Visibility.Visible) Label_SendingBytes.Margin = new Thickness(240, 12, 0, 0);
                else if (TextBox1.Visibility != Visibility.Visible ^ ctext.Visibility != Visibility.Visible) Label_SendingBytes.Margin = new Thickness(122, 12, 0, 0);
                else Label_SendingBytes.Margin = new Thickness(4, 12, 0, 0);
                
            }
            if (e.Key == System.Windows.Input.Key.M && TextBox_AtmPr.Visibility == Visibility.Collapsed)//V
            {
                TextBox_AtmPr.Visibility = Visibility.Visible;
                webcam.Stop();
            }
            else if(e.Key == System.Windows.Input.Key.M && TextBox_AtmPr.Visibility == Visibility.Visible)//V
            {
                TextBox_AtmPr.Visibility = Visibility.Collapsed;
                webcam.Start(0);
            }
            if (e.Key == System.Windows.Input.Key.T)//V
            {
                vmodel.FirstDepth = Model.vSM.depth;//+погрешность
            }
            if (e.Key == System.Windows.Input.Key.Y)//V
            {
                vmodel.SecondDepth = Model.vSM.depth;
                vmodel.ADVDepth = "1Depth:" + vmodel.FirstDepth + "\n2Depth" + vmodel.SecondDepth;
                vmodel.ADVHeigh = "ADVheight:" + Convert.ToString((Int16)(vmodel.FirstDepth - vmodel.SecondDepth + 16 + 6));//+погрешность
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

        private void TextBox_AtmPr_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Model.AirPressure = Convert.ToInt32(TextBox_AtmPr.Text);
            }
            catch (Exception ex)
            {
                Model.AirPressure = 0;
                Console.WriteLine("Возникло исключение: " + ex);
            }
        }

        public void SpeedModeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void TextBox_timer_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

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
            }/*
            if (vmodel.SpeedMode == "SpeedMode: 1")
            {
                Rectangle_lows.Fill = Brushes.Lime;
                Rectangle_normals.Fill = Brushes.White;
                Rectangle_highs.Fill = Brushes.White;
            }
            if (vmodel.SpeedMode == "SpeedMode: 2")
            {
                vMain.Rectangle_normals.Fill = Brushes.Gold;
                vMain.Rectangle_lows.Fill = Brushes.Lime;
                vMain.Rectangle_highs.Fill = Brushes.White;
            }
            if (vmodel.SpeedMode == "SpeedMode: 3")
            {
                vMain.Rectangle_highs.Fill = Brushes.Firebrick;
                vMain.Rectangle_normals.Fill = Brushes.Gold;
                vMain.Rectangle_lows.Fill = Brushes.Lime;
            }*/
        }
    }
}
