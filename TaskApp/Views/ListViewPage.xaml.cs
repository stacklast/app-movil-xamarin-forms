using System;
using System.Collections.ObjectModel;
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
        }
        private async void OnViewTask(object sender, EventArgs e)
        {
            var task = (TaskItem)((Button)sender).CommandParameter;
            await Navigation.PushAsync(new StackLayoutPage(task, Tasks));
        }
        private async void OnDeleteTask(object sender, EventArgs e)
        {
            var task = (TaskItem)((Button)sender).CommandParameter;
            var confirm = await DisplayAlert("Confirmar Eliminado", $"Estas seguro de eliminar {task.Name}?", "Si", "No");
            if (confirm)
            {
                Tasks.Remove(task);
            }
        }
        private async void OnBackToMainPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}