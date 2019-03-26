﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace mainWpf
{
    public class VUDPModel : INotifyPropertyChanged
    {
        UDPModel udpmodel;

        public bool Connection
        {
            set
            {
                if (value)
                {
                    IsSignal = Visibility.Visible;
                    NoSignal = Visibility.Collapsed;
                }
                else
                {
                    IsSignal = Visibility.Collapsed;
                    NoSignal = Visibility.Visible;
                }
            }
        }
        public string ReceivingData
        {
            get { return udpmodel.ReceivingData; }
            set
            {
                udpmodel.ReceivingData = value;
                OnPropertyChanged("ReceivingData");
            }
        }
        public string SendingData
        {
            get { return udpmodel.SendingData; }
            set
            {
                udpmodel.SendingData = value;
                OnPropertyChanged("SendingData");
            }
        }
        public string ReceivingBytes
        {
            get { return udpmodel.ReceivingBytes; }
            set
            {
                udpmodel.ReceivingBytes = value;
                OnPropertyChanged("ReceivingBytes");
            }
        }
        public string SendingBytes
        {
            get { return udpmodel.SendingBytes; }
            set
            {
                udpmodel.SendingBytes = value;
                OnPropertyChanged("SendingBytes");
            }
        }
        public Visibility IsSignal
        {
            get
            {
                return udpmodel.Signal;
            }
            set
            {
                udpmodel.Signal = value;
            }
        }
        public Visibility NoSignal
        {
            get
            {
                return udpmodel.NoSignal;
            }
            set
            {
                udpmodel.NoSignal = value;
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
        public VUDPModel(UDPModel udpmodel)
        {
            this.udpmodel = udpmodel;
        }
    }
}
