using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainWpf
{
    public class SettingsController//setingscontroller
    {
        public void ReadCoefficients(string filename)
        {
            StreamReader sr = new StreamReader(@"ResourseFiles\" + filename);
            Model.vGM.depth_KP_p = (sbyte)(Convert.ToDouble(sr.ReadLine()) * 100);
            Model.vGM.depth_KD_p = (sbyte)(Convert.ToDouble(sr.ReadLine()) * 100);
            Model.vGM.yaw_KP_p = (sbyte)(Convert.ToDouble(sr.ReadLine()) * 100);
            Model.vGM.yaw_KD_p = (sbyte)(Convert.ToDouble(sr.ReadLine()) * 100);
            Model.vGM.pitch_KP_p = (sbyte)(Convert.ToDouble(sr.ReadLine()) * 100);
            Model.vGM.pitch_KD_p = (sbyte)(Convert.ToDouble(sr.ReadLine()) * 100);
            sr.Close();//C<
        }
    }
}
