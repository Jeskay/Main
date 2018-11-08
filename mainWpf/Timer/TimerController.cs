using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media;

namespace mainWpf
{
    public class TimerController
    {
        private bool TimerStopped;
        private string timeLeft;
        private DateTime timeleft;
        private TimeSpan StopRange;
        public void UpdateTimer()
        {
            if(!TimerStopped)timeLeft = (Int16)(timeleft.Subtract(DateTime.Now)).TotalHours + ":" + (Int16)((timeleft.Subtract(DateTime.Now)).Minutes) + ":" + (Int16)(timeleft.Subtract(DateTime.Now)).Seconds;
            
        }
        public void StopTimer()
        {
            StopRange = DateTime.Now.Subtract(timeleft);
            TimerStopped = true;
        }
        public void ContinueTimer()
        {
            timeleft = DateTime.Now - StopRange;
            TimerStopped = false;
        }
        public void StartTimer(double minutes)
        {
            timeleft = DateTime.Now;
            timeleft = timeleft.AddMinutes(minutes);
            //vtimerModel.BackgroundColor = new SolidColorBrush(Color.FromArgb(0, 255, 12, 12));
        }
        public bool TimerState
        {
            get
            {
                return TimerStopped;
            }
        }
        public string TimeLeft
        {
            get
            {
                return timeLeft;
            }
        }
    }
}
