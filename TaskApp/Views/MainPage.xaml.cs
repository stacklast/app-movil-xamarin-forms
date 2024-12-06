using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laboratorio_Bimestre_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<TaskItem> Tasks { get; set; } = new ObservableCollection<TaskItem>();
        public MainPage()
        {
            InitializeComponent();
            taskPriorityStepper.ValueChanged += OnPriorityChanged;
        }
        private void OnPriorityChanged(object sender, ValueChangedEventArgs e)
        {
            taskPriorityLabel.Text = $"Priority: {e.NewValue}";
        }
        private async void OnAddTask(object sender, EventArgs e)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(taskName.Text))
            {
                await DisplayAlert("Validation Error", "Task Name is required.", "OK"); return;
            }
            if (string.IsNullOrWhiteSpace(taskDescription.Text))
            {
                await DisplayAlert("Validation Error", "Task Description is required.", "OK"); return;
            }
            if (taskDate.Date < DateTime.Today)
            {
                await DisplayAlert("Validation Error", "Task Date cannot be in the past.", "OK"); return;
            }
            if (taskTypePicker.SelectedIndex == -1)
            {
                await DisplayAlert("Validation Error", "Task Type is required.", "OK"); return;
            }
            var task = new TaskItem
            {
                Name = taskName.Text,
                Description = taskDescription.Text,
                Date = taskDate.Date,
                Type = taskTypePicker.Items[taskTypePicker.SelectedIndex],
                Priority = (int)taskPriorityStepper.Value
            };
            Tasks.Add(task);
            taskName.Text = string.Empty;
            taskDescription.Text = string.Empty;
            taskTypePicker.SelectedIndex = -1;
            taskPriorityStepper.Value = 1;
        }
        private async void OnViewTask(object sender, EventArgs e)
        {
            var task = (TaskItem)((Button)sender).CommandParameter;
            await Navigation.PushAsync(new StackLayoutPage(task, Tasks));
        }
        private async void OnDeleteTask(object sender, EventArgs e)
        {
            var task = (TaskItem)((Button)sender).CommandParameter;
            var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {task.Name}?", "Yes", "No");
            if (confirm)
            {
                Tasks.Remove(task);
            }
        }
        private async void OnNavigateToStackLayout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StackLayoutPage(new TaskItem(), Tasks));
        }
        private async void OnNavigateToGrid(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GridPage(Tasks));
        }
        private async void OnNavigateToAbsolute(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AbsolutePage());
        }
        private async void OnNavigateToListView(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListViewPage(Tasks));
        }
    }
    public class TaskItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Priority
        {
            get; set;
        }
    }
}