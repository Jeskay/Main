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
using System.Reflection;

namespace mainWpf
{
    //M<
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer3 = new DispatcherTimer();
        DispatcherTimer ClockTimer = new DispatcherTimer();
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        System.Media.SoundPlayer TimeOut1 = new System.Media.SoundPlayer();
        // обьявление используемых клаасов
        ROVprojection ProjectionWindow = new ROVprojection();
        ChartBuilder chartBuilder = new ChartBuilder();
        public JoystickController Maincontroller;
        public UDPController MainUDP;
        public SettingsController setter;
        public TimerController timercontroller;
        public VTimerModel vtimer;
        public VUDPModel vudp;
        FileStream SendLog;

        string path;
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

            while (true)
            {
                try
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
                    vmodel.Pitch = Model.vSM.pitch;
                    vmodel.Roll = Model.vSM.roll;
                    vmodel.Yaw = Model.vSM.yaw;
                    vudp.SendingData = info;
                    vudp.ReceivingData = "ReseivedData:" + "\n" + "Yaw:   " + Model.vSM.yaw + "\n" + "Pitch:    " + Model.vSM.pitch + "\n" + "Roll:   " + Model.vSM.roll + "\n" + "Depth:   " + Model.vSM.depth + '\n' + "Temperature: " + Model.vSM.temperature;
                    vudp.SendingBytes = MainUDP.SendedBytes;
                    vudp.ReceivingBytes = MainUDP.ReceivedBytes;
                    vudp.Connection = MainUDP.Connection;
                    ProjectionWindow.Yaw = Model.vSM.yaw;
                    ProjectionWindow.Diff = Model.vSM.pitch;
                    ProjectionWindow.Lurch = Model.vSM.roll;
                    
                    Thread.Sleep(20);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Возникло исключение1: " + ex.ToString() + "\n  " + ex.Message);
                }
                finally//logging data
                {
                    string senddata = Model.vGM.axisX_p + "-" + Model.vGM.axisY_p + "-" + Model.vGM.axisW_p + "-" + Model.vGM.axisZ_p + "-" + Model.vGM.manipulator_rotate + "-" + Model.vGM.camera_rotate + "-";
                    for (int i = 0; i < 12; i++) senddata += Maincontroller.GetButtons[i] + "-";
                    senddata += ":" + DateTime.Now.ToLongTimeString() + "\n";
                    Byte[] sendlog = new UTF8Encoding(true).GetBytes(senddata);
                    // Add some information to the file.
                    SendLog.Write(sendlog, 0, sendlog.Length);
                }
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

            TimeOut1.Stream =   Properties.Resources.time;//V>
            sp.Stream       =   Properties.Resources.NoSignalSound;

            sp.Load();
            TimeOut1.Load();

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
            dtm = DateTime.Now;
            dtm = dtm.AddMinutes(15.0);
            
            //включено в класс Controller
            Maincontroller.InitializeJoystick(this);

            MainUDP.Receiver();

            timer.Tick      += new EventHandler(timerTick);
            timer.Interval   = new TimeSpan(0, 0, 1);
            
            timer3.Tick     += new EventHandler(timer3Tick);
            timer3.Interval  = new TimeSpan(0, 0, 3);
            ClockTimer.Interval = new TimeSpan(0, 0, 1);
            ClockTimer.Tick += new EventHandler(ClockTimerTick);
            ClockTimer.Start();
            Thread thread1   = new Thread(Joystickthread);
            thread1.Priority = ThreadPriority.Highest;
            thread1.Start();
            //создание файла для записи
            path = Path.GetFullPath(@"ResourseFiles") + "\\Sendlog" + DateTime.Now.Hour + "h" + DateTime.Now.Minute + "m" + DateTime.Now.Second + "s" + ".txt";
            SendLog = File.Create(@path);
            //StreamReader sr = File.OpenText(@"ResourseFiles\\SendLog.txt");
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
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.N)//V
            {
                ProjectionWindow.Owner = this;
                ProjectionWindow.Show();
            }
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == System.Windows.Input.Key.V)
            {
                chartBuilder.Owner = this;
                chartBuilder.Show();
            }
        }

        private void MainWin_Closed(object sender, EventArgs e)//V
        {
            SendLog.Close();
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
