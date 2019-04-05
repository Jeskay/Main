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
   

    public class Model 
    {

 
        private static int Airpressure;
        private RotateTransform yawangle;
        private string advdepth;
        private static int speedmode;
        private Visibility lamp;
        

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SM
        {
            public float yaw;
            public float pitch;
            public float roll;
            public float depth;
            public float temperature;
            public sbyte  core;
        };
        public static SM vSM;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GM
        {
            public sbyte button_data1;
            public sbyte button_data2;
            public sbyte axisX_p;
            public sbyte axisY_p;
            public sbyte axisW_p;
            public sbyte axisZ_p;
            public sbyte camera_rotate;
            /*public sbyte yaw_KP_p;//убрать коэфиценты
            public sbyte yaw_KI_p;
            public sbyte yaw_KD_p;
            public sbyte pitch_KP_p;
            public sbyte pitch_KI_p;
            public sbyte pitch_KD_p;
            public sbyte depth_KP_p;
            public sbyte depth_KI_p;
            public sbyte depth_KD_p;*/
            public sbyte manipulator_rotate;
            

        };

        public static GM vGM;//M<
        
        public RotateTransform YawAngle
        {
            get { return yawangle; }
            set { yawangle = value; }
        }
        public float FirstDepth
        {
            get;
            set;
        }
        public float SecondDepth
        {
            get;
            set;
        }
        public static int SpeedMode
        {
            get { return speedmode; }
            set { speedmode = value; }
        }
        public float Depth
        {
            get { return vSM.depth; }
            set
            {
                vSM.depth = value;
            }
        }
        public float Yaw
        {
            get { return vSM.yaw; }
            set
            {
                vSM.yaw = value;
            }
        }
        public float Pitch
        {
            get { return vSM.pitch; }
            set
            {
                vSM.pitch = value;
            }
        }
        public float Roll
        {
            get { return vSM.roll; }
            set
            {
                vSM.roll = value;
            }
        }
        public float Temperature
        {
            get { return vSM.temperature; }
            set
            {
                vSM.temperature = value;
            }
        }
        public sbyte Core
        {
            get { return vSM.core; }
            set
            {
                vSM.core = value;
            }
        }
        public Visibility Lamp
        {
            get { return lamp; }
            set
            {
                lamp = value;
            }
        }
        public Model()
        {
            
            Depth          = 0;
            Yaw            = 0;
            Pitch          = 0;
            Roll           = 0;
            FirstDepth     = 0;
            SecondDepth    = 0;
            speedmode      = 1;
            Core           = 0;
            Lamp = Visibility.Collapsed;
        }

}
    
}
