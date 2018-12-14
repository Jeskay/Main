using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainWpf
{
    public class ROVprojectionModel
    {
        private float rovrotationZ;
        private float rovrotationY;
        private float rovrotationX;
        public float ROVrotationZ
        {
            get
            {
                return rovrotationZ;
            }
            set
            {
                rovrotationZ = value;
            }
        }
        public float ROVrotationY
        {
            get
            {
                return rovrotationY;
            }
            set
            {
                 rovrotationY = value;
            }
        }
        public float ROVrotationX
        {
            get
            {
                return rovrotationX;
            }
            set
            {
                rovrotationX = value;
            }
        }
    }
}
