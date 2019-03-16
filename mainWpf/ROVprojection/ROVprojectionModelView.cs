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
        
        public float RotationZ
        {
            get
            {
                return ROVprojection.ROVrotationZ;
            }
            set
            {
                ROVprojection.ROVrotationZ = value;
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
                OnPropertyChanged("RotationY");

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
