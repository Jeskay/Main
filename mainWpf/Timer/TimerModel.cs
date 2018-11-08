using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace mainWpf
{
    public class TimerModel 
    {
        public string TimeLeft      { get; set; }
        public bool TimerStopped    { get; set; }
        public string ButtonContent { get; set; }
        public TimerModel()
        {
            TimeLeft      = "00:00:00";
            TimerStopped  = false;
            ButtonContent = "Pause";
        }

    }
}
