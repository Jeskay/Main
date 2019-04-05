using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Windows;

namespace mainWpf
{
    public class VModel : INotifyPropertyChanged
    {
        Model model;

        public sbyte Core
        {
            get { return model.Core; }
            set
            {
                model.Core = value;
                if (value != 0) Lamp = Visibility.Visible;
                else Lamp = Visibility.Hidden;
                
            }
        }
        public float Pitch
        {
            get { return model.Pitch; }
            set
            {
                model.Pitch = value;
                OnPropertyChanged("Pitch");
            }
        }
        public float Roll
        {
            get { return model.Roll; }
            set
            {
                model.Roll = value;
                OnPropertyChanged("Roll");
            }
        }
        public float Yaw
        {
            get { return model.Yaw; }
            set
            {
                model.Yaw = value;
                OnPropertyChanged("Yaw");
            }
        }
        public float Depth
        {
            get { return model.Roll; }
            set
            {
                model.Depth = value;
                OnPropertyChanged("Depth");
            }
        }
        public float FirstDepth
        {
            get
            {
                return model.FirstDepth;
            }
            set
            {
                model.FirstDepth = value;
            }
        }
        public float SecondDepth
        {
            get
            {
                return model.SecondDepth;
            }
            set
            {
                model.SecondDepth = value;
            }
        }
        public Visibility Lamp
        {
            get
            {
                return model.Lamp;
            }
            set
            {
                model.Lamp = value;
                OnPropertyChanged("Lamp");
            }
        }
        public string SpeedMode
        {
            get { return "SpeedMode: " + Model.SpeedMode; }
            set
            {
                try
                {
                    Model.SpeedMode = Convert.ToInt16(value);
                }
                catch (FormatException)
                {
                    Model.SpeedMode = 1;
                }
                finally
                {
                    OnPropertyChanged("SpeedMode");
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Speed1Brush"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Speed2Brush"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Speed3Brush"));
                }
            }
        }
        #region Brushes
        public Brush Speed1Brush => GetBrush(1);
        public Brush Speed2Brush => GetBrush(2);
        public Brush Speed3Brush => GetBrush(3);
        public Brush GetBrush(int speed)
        {
            if (model == null || speed > Model.SpeedMode)
            {
                return Brushes.White;
            }
            switch (Model.SpeedMode)
            {
                case 1: return Brushes.Lime;
                case 2: return Brushes.Gold;
                default: return Brushes.Firebrick;
            }
        }
        #endregion Brushes
        public RotateTransform YawAngle
        {
            get { return model.YawAngle;  }
            set
            {
                model.YawAngle = value;
                OnPropertyChanged("YawAngle");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; // Событие, которое нужно вызывать при изменении
        public void OnPropertyChanged(string propertyName)//RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;//1

            // Если кто-то на него подписан, то вызывем его
            //if (PropertyChanged != null)
            if (handler != null) //1
            {
                //PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                handler(this, new PropertyChangedEventArgs(propertyName));//1
            }
        }

        public VModel(Model model)
        {
            this.model = model;
        }
    }
}
