using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace mainWpf
{
    public class ROVprojectionModelView : INotifyPropertyChanged
    {
        ROVprojectionModel ROVprojection = new ROVprojectionModel();
        public string axis_X;
        public string axis_Y;
        public string axis_Z;

        public float RotationZ
        {
            get
            {
                return ROVprojection.ROVrotationZ;
            }
            set
            {
                ROVprojection.ROVrotationZ = value;
                Axis_Z = Convert.ToString(value);
                OnPropertyChanged("RotationZ");
            }
        }
        public float RotationX
        {
            get
            {
                return ROVprojection.ROVrotationX;
            }
            set
            {
                ROVprojection.ROVrotationX = value;
                Axis_X = Convert.ToString(value);
                OnPropertyChanged("RotationX");
            }
        }
        public float RotationY
        {
            get
            {
                return ROVprojection.ROVrotationY;
            }
            set
            {
                ROVprojection.ROVrotationY = value;
                Axis_Y = Convert.ToString(value);
                OnPropertyChanged("RotationY");

            }
        }
        public string Axis_X
        {
            get
            {
                return axis_X;
            }
            set
            {
                axis_X = "Yaw: " + value;
                OnPropertyChanged("Axis_X");
            }
        }
        public string Axis_Y
        {
            get
            {
                return axis_Y;
            }
            set
            {
                axis_Y = "Lurch: " + value;
                OnPropertyChanged("Axis_Y");
            }
        }
        public string Axis_Z
        {
            get
            {
                return axis_Z;
            }
            set
            {
                axis_Z = "Pitch: " + value;
                OnPropertyChanged("Axis_Z");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged; // Событие, которое нужно вызывать при изменении
        public void OnPropertyChanged(string propertyName)//RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//1
        }
        public ROVprojectionModelView(ROVprojectionModel ROVprojection)
        {
            this.ROVprojection = ROVprojection;
        }
    }
}
