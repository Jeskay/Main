using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows;
namespace mainWpf
{
    public class UDPModel
    {
        public static IPAddress remoteIPAddress = IPAddress.Parse("192.168.1.5");//IPAddress.Broadcast;
        private static int remotePort = 5000;
        private static int localPort = 5000;
        private Visibility signal;
        private Visibility nosignal;
        private string sendingdata;
        private string sendingbytes;
        private string receivingdata;
        private string receivingbytes;

        public static IPAddress RemoteIP
        {
            get { return remoteIPAddress; }
        }
        public static int RemotePort
        {
            get { return remotePort; }
        }
        public static int LocalPort
        {
            get { return localPort; }
        }

        public Visibility Signal
        {
            get
            {
                return signal;
            }
            set
            {
                signal = value;
            }
        }
        public Visibility NoSignal
        {
            get
            {
                return nosignal;
            }
            set
            {
                nosignal = value;
            }
        }
        public string SendingData
        {
            get
            {
                return sendingdata;
            }
            set
            {
                // Устанавливаем новое значение
                if (value != null) sendingdata = value;
                else sendingdata = "NO DATA SENT";
            }
        }
        public string SendingBytes
        {
            get
            {
                return sendingbytes;
            }
            set
            {
                // Устанавливаем новое значение
                sendingbytes = value;
            }
        }
        public string ReceivingData
        {
            get
            {
                return receivingdata;
            }
            set
            {
                // Устанавливаем новое значение
                receivingdata = value;
            }
        }
        public string ReceivingBytes
        {
            get
            {
                return receivingbytes;
            }
            set
            {
                // Устанавливаем новое значение
                receivingbytes = value;
            }
        }
        public UDPModel()
        {

            SendingData = "NO DATA SENT";
            SendingBytes = "NO BYTES SENT";
            ReceivingData = "NO DATA RECEIVED";
            ReceivingBytes = "NO BYTES RECEIVED";
        }
    }
}
