using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainWpf
{
    public static class ChartModel
    {
        private static List<int>[] buttons;
        private static List<sbyte> axisX_p;
        private static List<sbyte> axisY_p;
        private static List<sbyte> axisW_p;
        private static List<sbyte> axisZ_p;
        private static List<sbyte> camera_rotate;
        private static List<sbyte> manipulator_rotate;
        private static List<float> yaw;
        private static List<float> pitch;
        private static List<float> roll;
        private static List<float> depth;
        private static List<float> temperature;
        private static List<sbyte> core;
        private static List<string> sendTime;
        private static List<string> receiveTime;


        public static List<int>[] Buttons
        {
            get
            {
                return buttons;
            }
            set
            {
                buttons = value;

            }
        }
        public static List<sbyte> AxisX_p
        {
            get
            {
                return axisX_p;
            }
            set
            {
                axisX_p = value;
            }
        }
        public static List<sbyte> AxisY_p
        {
            get
            {
                return axisY_p;
            }
            set
            {
                axisY_p = value;
            }
        }
        public static List<sbyte> AxisW_p
        {
            get
            {
                return axisW_p;
            }
            set
            {
                axisW_p = value;
            }
        }
        public static List<sbyte> AxisZ_p
        {
            get
            {
                return axisZ_p;
            }
            set
            {
                axisZ_p = value;
            }
        }
        public static List<sbyte> Camera_rotate
        {
            get
            {
                return camera_rotate;
            }
            set
            {
                camera_rotate = value;
            }
        }
        public static List<sbyte> Manipulator_rotate
        {
            get
            {
                return manipulator_rotate;
            }
            set
            {
                manipulator_rotate = value;
            }
        }
        public static List<float> Yaw
        {
            get
            {
                return yaw;
            }
            set
            {
                yaw = value;
            }
        }
        public static List<float> Pitch
        {
            get
            {
                return pitch;
            }
            set
            {
                pitch = value;
            }
        }
        public static List<float> Roll
        {
            get
            {
                return roll;
            }
            set
            {
                roll = value;
            }
        }
        public static List<float> Depth
        {
            get
            {
                return depth;
            }
            set
            {
                depth = value;
            }
        }
        public static List<float> Temperature
        {
            get
            {
                return temperature;
            }
            set
            {
                temperature = value;
            }
        }
        public static List<sbyte> Core
        {
            get
            {
                return core;
            }
            set
            {
                core = value;
            }
        }
        public static List<string> ReceiveTime
        {
            get
            {
                return receiveTime;
            }
            set
            {
                receiveTime = value;
            }
        }
        public static List<string> SendTime
        {
            get
            {
                return sendTime;
            }
            set
            {
                sendTime = value;
            }
        }
    }
}
