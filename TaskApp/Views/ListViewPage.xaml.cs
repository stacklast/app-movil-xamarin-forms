using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laboratorio_Bimestre_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        private ObservableCollection<Views.TaskItem> Tasks { get; set; }
        public ListViewPage(ObservableCollection<Views.TaskItem> tasks)
        {
            InitializeComponent();
            Tasks = tasks;
            listView.ItemsSource = Tasks;
        }
        private async void OnViewTask(object sender, EventArgs e)
        {
            var task = (Views.TaskItem)((Button)sender).CommandParameter;
            await Navigation.PushAsync(new StackLayoutPage(task, Tasks));
        }
        private async void OnDeleteTask(object sender, EventArgs e)
        {
            var task = (Views.TaskItem)((Button)sender).CommandParameter;
            var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {task.Name}?", "Yes", "No");
            if (confirm)
            {
                Tasks.Remove(task);
            }
        }
        private async void OnBackToMainPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public class TaskItem
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
        }
    }
}