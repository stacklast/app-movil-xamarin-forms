using System;
using System.Collections.ObjectModel;
using System.Linq;
using TaskApp.Models;
using TaskApp.Services;
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
            Tasks.CollectionChanged += (sender, e) => UpdateTaskCount();
        }
        private void UpdateTaskCount()
        {
            taskCountLabel.Text = $"Total Tasks: {Tasks.Count}";
        }
        private void OnPriorityChanged(object sender, ValueChangedEventArgs e)
        {
            taskPriorityLabel.Text = $"Prioridad: {e.NewValue}";
        }
        private async void OnAddTask(object sender, EventArgs e)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(taskName.Text))
            {
                await DisplayAlert("Validación", "El título de la tarea es requerido.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(taskDescription.Text))
            {
                await DisplayAlert("Validación", "La descripción es requerida.", "OK");
                return;
            }
            if (taskDate.Date < DateTime.Today)
            {
                await DisplayAlert("Validación", "La fecha no puede ser menor a hoy.", "OK");
                return;
            }
            if (taskTypePicker.SelectedIndex == -1)
            {
                await DisplayAlert("Validación", "El tipo de tarea es requerido", "OK");
                return;
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
        private async void OnNavigateToGrid(object sender, EventArgs e)
        {
            var currentPage = Navigation.NavigationStack.LastOrDefault();
            if (currentPage != null)
            {
                await NavigationService.NavigateWithAnimation(currentPage, new GridPage(Tasks));
            }
        }
        private async void OnNavigateToAbsolute(object sender, EventArgs e)
        {
            var currentPage = Navigation.NavigationStack.LastOrDefault();
            if (currentPage != null)
            {
                await NavigationService.NavigateWithAnimation(currentPage, new AbsolutePage());
            }
        }
        private async void OnNavigateToListView(object sender, EventArgs e)
        {
            var currentPage = Navigation.NavigationStack.LastOrDefault();
            if (currentPage != null)
            {
                await NavigationService.NavigateWithAnimation(currentPage, new ListViewPage(Tasks));
            }
        }
        private void OnColorSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value) // Si el Switch está encendido
            {
                this.BackgroundColor = Color.LightGreen;
            }
            else // Si el Switch está apagado
            {
                this.BackgroundColor = Color.White;
            }
        }
    }
}