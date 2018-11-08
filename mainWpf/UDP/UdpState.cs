using System;
using System.Net;
using System.Net.Sockets;
namespace mainWpf
{
    public class UdpState//M>
    {
        public UdpClient ut;
        public IPEndPoint e;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public int counter = 0;

    };
}
