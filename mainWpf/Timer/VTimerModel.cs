using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace mainWpf
{
    public class VTimerModel : INotifyPropertyChanged
    {
        TimerModel timermodel;
        public string TimeLeft
        {
            get
            {
                return timermodel.TimeLeft;
            }
            set
            {
                timermodel.TimeLeft = value;
                OnPropertyChanged("TimeLeft");
            }
        }
        public bool TimerStopped
        {
            get
            {
                return timermodel.TimerStopped;
            }
            set
            {
                timermodel.TimerStopped = value;
            }
        }
        public string ButtonContent
        {
            get
            {
                return timermodel.ButtonContent;
            }
            set
            {
                timermodel.ButtonContent = value;
                OnPropertyChanged("ButtonContent");
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
        public VTimerModel(TimerModel timermodel)
        {
            this.timermodel = timermodel;
        }
    }
}
