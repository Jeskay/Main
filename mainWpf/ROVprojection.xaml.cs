﻿using System;
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
                projectionmodelview.RotationZ = value;
            }
        }
        public float Diff
        {
            set
            {
                projectionmodelview.RotationX = value;
            }
        }
        public float Lurch
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

            model = importer.Load("C:\\Users\\ASUS\\Pictures\\15981_Hollow_Cylinder_v1.obj");
            DataContext = projectionmodelview;
            Models.Content = model;
        }

        private void TextBox_X_TextChanged(object sender, TextChangedEventArgs e)
        {
            float number;
            if (float.TryParse(TextBox_X.Text, out number))
            {
                Yaw = number;
            }
            
        }

        private void TextBox_Y_TextChanged(object sender, TextChangedEventArgs e)
        {
            float number;
            if (float.TryParse(TextBox_Y.Text, out number))
            {
                Lurch = number;
            }
        }

        private void TextBox_Z_TextChanged(object sender, TextChangedEventArgs e)
        {
            float number;
            if (float.TryParse(TextBox_Z.Text, out number))
            {
                Diff = number;
            }
        }

        private void Yaw_Szlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Yaw = (float)Yaw_Szlider.Value;
        }

        private void Lurch_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Lurch = (float)Lurch_Slider.Value;
        }

        private void Diff_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Diff = (float)Diff_Slider.Value;
        }
    }
}
