using System;
using System.Collections.ObjectModel;
using TaskApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laboratorio_Bimestre_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StackLayoutPage : ContentPage
    {
        private TaskItem _task;
        private ObservableCollection<TaskItem> _tasks;
        public StackLayoutPage()
        {
            InitializeComponent();
            taskPriorityStepper.ValueChanged += OnPriorityChanged;
        }
        public StackLayoutPage(TaskItem task, ObservableCollection<TaskItem> tasks)
        {
            InitializeComponent();
            _task = task;
            _tasks = tasks;
            taskName.Text = task.Name;
            taskDescription.Text = task.Description;
            taskDate.Date = task.Date;
            taskTypePicker.SelectedItem = task.Type;
            taskPriorityStepper.Value = task.Priority;
            taskPriorityLabel.Text = $"Priority: {task.Priority}";
            taskPriorityStepper.ValueChanged += OnPriorityChanged;
        }
        private void OnPriorityChanged(object sender, ValueChangedEventArgs e)
        {
            taskPriorityLabel.Text = $"Priority: {e.NewValue}";
        }
        private async void OnSaveTask(object sender, EventArgs e)
        { // Validations
            if (string.IsNullOrWhiteSpace(taskName.Text))
            {
                await DisplayAlert("Validation Error", "Task Name is required.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(taskDescription.Text))
            {
                await DisplayAlert("Validation Error", "Task Description is required.", "OK");
                return;
            }
            if (taskDate.Date < DateTime.Today)
            {
                await DisplayAlert("Validation Error", "Task Date cannot be in the past.", "OK");
                return;
            }
            if (taskTypePicker.SelectedIndex == -1)
            {
                await DisplayAlert("Validation Error", "Task Type is required.", "OK");
                return;
            }
            _task.Name = taskName.Text;
            _task.Description = taskDescription.Text;
            _task.Date = taskDate.Date;
            _task.Type = taskTypePicker.Items[taskTypePicker.SelectedIndex];
            _task.Priority = (int)taskPriorityStepper.Value;
            await Navigation.PopAsync();
        }
    }
}