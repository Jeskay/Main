using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace mainWpf
{
   

    public class Model 
    {

        
        private static int Airpressure;
        private RotateTransform yawangle;
        private string advdepth;
        private static int speedmode;
        

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SM
        {
            public float yaw;
            public float pitch;
            public float roll;
            public float depth;
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
            public sbyte yaw_KP_p;
            public sbyte yaw_KI_p;
            public sbyte yaw_KD_p;
            public sbyte pitch_KP_p;
            public sbyte pitch_KI_p;
            public sbyte pitch_KD_p;
            public sbyte depth_KP_p;
            public sbyte depth_KI_p;
            public sbyte depth_KD_p;
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
                //OnPropertyChanged("Depth");
            }
        }
        public float Yaw
        {
            get { return vSM.yaw; }
            set
            {
                vSM.yaw = value;
                //OnPropertyChanged("Yaw");
            }
        }
        public float Pitch
        {
            get { return vSM.pitch; }
            set
            {
                vSM.pitch = value;
                //OnPropertyChanged("Pitch");
            }
        }
        public float Roll
        {
            get { return vSM.roll; }
            set
            {
                vSM.roll = value;
                //OnPropertyChanged("Roll");
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
        }

}
    
}
