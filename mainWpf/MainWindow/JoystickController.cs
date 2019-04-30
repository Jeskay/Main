using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Interop;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using WebCam_Capture;
using System.Diagnostics;
using System.IO;

namespace mainWpf
{
    public class JoystickController
    {
        private Device joystick;
        private double SpeedK = 0.25;
        private bool NoneJoystick = false;
        private static int[] Buttons = new int[22];

        public static int[] GetButtons
        {
            get
            {
                return Buttons;
            }
            set
            {
                Buttons = value;
            }
        }
        public bool GetJoystick
        {
            get { return NoneJoystick; }
        }
        public void InitializeJoystick(Window window)
        {
            foreach (DeviceInstance di in
            Manager.GetDevices(
            DeviceClass.GameControl,
            EnumDevicesFlags.AttachedOnly))
            {
                joystick = new Device(di.InstanceGuid);
                break;
            }

            if (joystick == null)
            {

                //Throw exception if joystick not found.
                NoneJoystick = true;
                //throw new Exception("No joystick found.");
            }
            if (!NoneJoystick)
            {
                foreach (DeviceObjectInstance doi in joystick.Objects)
                {
                    if ((doi.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                    {
                        joystick.Properties.SetRange(
                            ParameterHow.ById,
                            doi.ObjectId,
                            new InputRange(-100, 100));
                    }
                }

                //Set joystick axis mode absolute.
                joystick.Properties.AxisModeAbsolute = true;

                //set cooperative level.
                //joystick.SetCooperativeLevel()
                joystick.SetCooperativeLevel(
                    new WindowInteropHelper(window).Handle,
                    CooperativeLevelFlags.NonExclusive |
                    CooperativeLevelFlags.Background);

                //Acquire devices for capturing.
                joystick.Acquire();
            }
        }
        public void UpdateJoystick(VModel Vmodel)
        {

            JoystickState state = joystick.CurrentJoystickState;
  
            Buttons_Update(state.GetButtons());
            Manipulator_Rotation_Update(state.GetPointOfView());
            SpeedMode_Update(Vmodel);
            Slider_Update(state.GetSlider());
            Main_Joystick_Parameters_Update(state);
        }
        private void Buttons_Update(byte[] buttons)
        {
            for (int i = 0; i < 12; i++)
            {
                if (buttons[i] != 0)
                {
                    Buttons[i] = 1;
                }
                else
                {
                    Buttons[i] = 0;
                }
            }
            Model.vGM.button_data1 = 0;
            Model.vGM.button_data2 = 0;
            Model.vGM.button_data3 = 0;
            for (int i = 0; i <= 7; i++)//C>
            {
                if ((Buttons[i] == 1))
                {
                    Model.vGM.button_data1 = (sbyte)(((int)Model.vGM.button_data1) | (1 << i));
                }
            }
            for (int i = 8; i < 15; i++)
            {
                if ((Buttons[i] == 1))
                {
                    Model.vGM.button_data2 = (sbyte)((int)Model.vGM.button_data2 | (1 << (i - 8)));
                }
            }
            for (int i = 15; i < 22; i++)
            {
                if (Buttons[i] == 1)
                {
                    Model.vGM.button_data3 = (sbyte)((int)Model.vGM.button_data3 | (1 << (i - 15)));
                }
            }
        }
        private void Manipulator_Rotation_Update(int[] viewP)
        {
            if (viewP[2] == 0) viewP[0] = 0;
            Model.vGM.camera_rotate = (sbyte)viewP[0];
            if (Model.vGM.camera_rotate == (sbyte)40) Model.vGM.camera_rotate = (sbyte)1;
            if (Model.vGM.camera_rotate == (sbyte)120) Model.vGM.camera_rotate = (sbyte)3;
            if (Model.vGM.camera_rotate == (sbyte)80) Model.vGM.camera_rotate = (sbyte)2;
            if (Model.vGM.camera_rotate == (sbyte)0) Model.vGM.camera_rotate = (sbyte)4;
            if (Model.vGM.camera_rotate == (sbyte)-1) Model.vGM.camera_rotate = 0;

        }
        private void SpeedMode_Update(VModel vmodel)
        {
            if (Buttons[6] == 1)
            {
                vmodel.SpeedMode = "1";
                SpeedK = 0.5;
            }
            if (Buttons[5] == 1)
            {
                vmodel.SpeedMode = "2";
                SpeedK = 0.75;
            }
            if (Buttons[4] == 1)
            {
                vmodel.SpeedMode = "3";
                SpeedK = 0.9;
            }
        }
        private void Slider_Update(int[] slider_p)
        {
            if (slider_p[0] > -50 && slider_p[0] < 50) slider_p[0] = 0;
            Model.vGM.manipulator_rotate = (sbyte)(slider_p[0]);

        }
        private void Main_Joystick_Parameters_Update(JoystickState state)
        {
            Model.vGM.axisX_p = (sbyte)Math.Round(state.X * SpeedK);
            Model.vGM.axisY_p = (sbyte)Math.Round(state.Y * SpeedK * -1);
            Model.vGM.axisZ_p = (sbyte)state.Z;
            Model.vGM.axisW_p   = (sbyte)Math.Round(state.Rz * SpeedK);
        }
    } 
}
