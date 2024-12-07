using System;
using System.ComponentModel;

namespace TaskApp.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string name;
        private string description;
        private DateTime date;
        private string type;
        private int priority;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                { name = value; OnPropertyChanged(nameof(Name)); }
            }
        }
        public string Description
        {
            get => description; set
            {
                if (description != value)
                { description = value; OnPropertyChanged(nameof(Description)); }
            }
        }
        public DateTime Date
        {
            get => date;
            set
            {
                if (date != value)
                { date = value; OnPropertyChanged(nameof(Date)); }
            }
        }
        public string Type
        {
            get => type;
            set
            {
                if (type != value)
                { type = value; OnPropertyChanged(nameof(Type)); }
            }
        }
        public int Priority
        {
            get => priority;
            set
            {
                if (priority != value)
                { priority = value; OnPropertyChanged(nameof(Priority)); }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
