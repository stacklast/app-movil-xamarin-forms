using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Laboratorio_Bimestre_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        private ObservableCollection<TaskItem> Tasks { get; set; }
        public ListViewPage(ObservableCollection<TaskItem> tasks)
        {
            InitializeComponent();
            Tasks = tasks;
            listView.ItemsSource = Tasks;
            BindingContext = new ListViewPageViewModel(tasks);
        }
        private async void OnViewTask(object sender, EventArgs e)
        {
            var task = (TaskItem)((Button)sender).CommandParameter;
            await Navigation.PushAsync(new StackLayoutPage(task, Tasks));
        }
        private async void OnDeleteTask(object sender, EventArgs e)
        {
            var task = (TaskItem)((Button)sender).CommandParameter;
            var confirm = await DisplayAlert("Confirmar Eliminado",
                $"Estas seguro de eliminar {task.Name}?",
                "Si", "No");

            if (confirm)
            {
                Tasks.Remove(task);
            }
        }

    }
    public class ListViewPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TaskItem> _tasks;
        public ObservableCollection<TaskItem> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTasks));
                OnPropertyChanged(nameof(HasNoTasks));
            }
        }
        public bool HasTasks => Tasks.Count > 0;
        public bool HasNoTasks => Tasks.Count == 0;
        public ListViewPageViewModel(ObservableCollection<TaskItem> tasks)
        {
            Tasks = tasks;
            Tasks.CollectionChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(HasTasks));
                OnPropertyChanged(nameof(HasNoTasks));
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}