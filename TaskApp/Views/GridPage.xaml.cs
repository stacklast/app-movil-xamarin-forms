using System;
using System.Collections.ObjectModel;
using System.Linq;
using TaskApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laboratorio_Bimestre_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridPage : ContentPage
    {
        public ObservableCollection<TaskItem> FilteredTasks { get; set; }
        private ObservableCollection<TaskItem> _allTasks;
        public GridPage(ObservableCollection<TaskItem> tasks)
        {
            InitializeComponent();
            _allTasks = tasks;
            FilteredTasks = new ObservableCollection<TaskItem>();
            taskListView.ItemsSource = FilteredTasks;
        }
        private void OnFilter(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void OnFilterDateSelected(object sender, DateChangedEventArgs e)
        {
            ApplyFilter();
        }
        private void ApplyFilter()
        {
            DateTime selectedDate = filterDatePicker.Date;

            // Filtra las tareas sin eliminar de la colección original
            var filtered = _allTasks
                .Where(t => t.Date.Date == selectedDate.Date)
                .ToList();

            // Actualiza la colección de tareas filtradas
            FilteredTasks.Clear();
            foreach (var task in filtered)
            {
                FilteredTasks.Add(task);
            }

            // Verifica si no hay tareas para la fecha seleccionada y agrega un mensaje
            if (!FilteredTasks.Any())
            {
                FilteredTasks.Add(new TaskItem
                {
                    Name = "No tasks",
                    Description = "No tasks found for the selected date",
                    Date = selectedDate,
                    Type = string.Empty,
                    Priority = 0
                });
            }
        }
    }
}
