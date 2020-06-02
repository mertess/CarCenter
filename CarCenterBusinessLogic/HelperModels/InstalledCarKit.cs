using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarCenterBusinessLogic.HelperModels
{
    public class InstalledCarKit : INotifyPropertyChanged
    {
        private string kitName;
        private int count;
        private DateTime installationDate;
        public bool RemovedFromStorages { set; get; }

        public string KitName 
        {
            get => kitName;
            set
            {
                kitName = value;
                OnPropertyChanged("KitName");
            }
        }

        public int Count 
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public DateTime InstallationDate 
        {
            get => installationDate;
            set
            {
                installationDate = value;
                OnPropertyChanged("InstallationDate");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
