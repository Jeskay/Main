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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RotateY"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RotateX"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RotateY"));

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ROVprojectionModelView(ROVprojectionModel ROVprojection)
        {
            this.ROVprojection = ROVprojection;
        }
    }
}
