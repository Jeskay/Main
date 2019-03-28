using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace mainWpf
{
    public class UDPController
    {
        DispatcherTimer ConnectionTimer;
        public bool Connection = false;
        private static UdpClient m_receivingUdpClient = new UdpClient(UDPModel.LocalPort);
        private static IPEndPoint m_RemoteIpEndPoint = new IPEndPoint(UDPModel.RemoteIP, UDPModel.RemotePort);
        

        public void ConnectionTimerTick(object sender, EventArgs e)
        {
            Connection = false;
        }
#region receiving
        private static string R = "NO RECEIVED DATA";
        public string ReceivedBytes
        {
            get { return R; }
        }
        public void Receiver()//C>
        {
            // Создаем UdpClient для чтения входящих данных
            long time1, nowtime1;
            UdpState s = new UdpState();
            s.e = m_RemoteIpEndPoint;
            s.ut = m_receivingUdpClient;
            try
            {
                long oldTime = DateTime.Now.Ticks;
                IAsyncResult res = m_receivingUdpClient.BeginReceive(new AsyncCallback(ReceiveCallback), s);// начинаем прием
                nowtime1 = DateTime.Now.Ticks;
                time1 = (nowtime1 - oldTime) / 1000;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);//V
            }
        }
        private void ReceiveCallback(IAsyncResult ar)//C>
        {
            try
            {
                UdpClient c = (UdpClient)((UdpState)(ar.AsyncState)).ut;
                IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;
                Byte[] buffer = c.EndReceive(ar, ref e);

                if (buffer.Length > 0)
                {
                    Connection = true;
                    int size = Marshal.SizeOf(Model.vSM);
                    IntPtr ptr = Marshal.AllocHGlobal(size);
                    Marshal.Copy(buffer, 0, ptr, size);
                    Model.vSM = (Model.SM)Marshal.PtrToStructure(ptr, Model.vSM.GetType());
                    Model.vSM.depth = (float)((Model.vSM.depth) / 1.197);//конвертирование глубины из паскалей в сантиметры
                    R = "";
                    for (int i = 0; i < buffer.Length; i++) R += buffer[i] + " ";
                    if (R != "") R = "ReceivedByteData: " + R;
                    else R = "NO RECEIVED DATA";
                    Marshal.FreeHGlobal(ptr);
                }
                UdpState s = new UdpState();
                s.ut = c;
                s.e = e;
                c.BeginReceive(new AsyncCallback(ReceiveCallback), s);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
                Connection = false;
            }
        }//C<
#endregion receiving
#region sending
        private static string S = "NO SENT DATA";
        public string SendedBytes
        {
            get { return S; }
        }
        public void Send(string datagram)//C>
        {
            try
            {
                // Преобразуем данные в массив байтов
                byte[] bytes = new byte[Marshal.SizeOf(Model.vGM)];
                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(Model.vGM));
                Console.WriteLine("размер пакета = " + Marshal.SizeOf(Model.vGM).ToString() + "\n  ");//V
                Marshal.StructureToPtr(Model.vGM, ptr, true);
                Marshal.Copy(ptr, bytes, 0, Marshal.SizeOf(Model.vGM));
                Console.WriteLine("отправлен пакет:");//V
                for (int i = 0; i < bytes.Length; i++) Console.Write(bytes[i] + " ");
                // Отправляем данные
                m_receivingUdpClient.Send(bytes, bytes.Length, m_RemoteIpEndPoint);
                //sender.Send(bytes, bytes.Length, endPoint);
                S = "Sending Bytes:\n";
                for (int i = 0; i < bytes.Length; i++) S += bytes[i] + " ";
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
        }
        #endregion sending
        public UDPController()
        {
            ConnectionTimer = new DispatcherTimer();
            ConnectionTimer.Tick += new EventHandler(ConnectionTimerTick);
            ConnectionTimer.Interval = new TimeSpan(0, 0, 3);
        }
    }
}
