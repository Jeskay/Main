using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using mainWpf;
using System.Windows.Threading;

namespace mainWpf
{
    /// <summary>
    /// Логика взаимодействия для ROVprojection.xaml
    /// </summary>
    public partial class ROVprojection : Window
    {
        ROVprojectionModelView projectionmodelview;
        ModelImporter importer;
        Model3D model;

        public float Yaw
        {
            set
            {
                projectionmodelview.RotationX = value;
            }
        }
        public float Pitch
        {
            set
            {
                projectionmodelview.RotationZ = value;
            }
        }
        public float Roll
        {
            set
            {
                projectionmodelview.RotationY = value;
            }
        }

        public ROVprojection()
        {
            projectionmodelview = new ROVprojectionModelView(new ROVprojectionModel());
            importer = new ModelImporter();
            
            InitializeComponent();

            model = importer.Load(@"ResourseFiles\\ROV.obj");
            DataContext = projectionmodelview;

            Models.Content = model;
            
        }
    }
}
