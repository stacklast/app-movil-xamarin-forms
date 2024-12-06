using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laboratorio_Bimestre_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridPage : ContentPage
    {
        public DateTime FilterDate { get; set; } = DateTime.Today;
        private ObservableCollection<TaskItem> _allTasks;
        private ObservableCollection<TaskItem> _filteredTasks = new ObservableCollection<TaskItem>();
        public GridPage(ObservableCollection<TaskItem> tasks)
        {
            InitializeComponent();
            _allTasks = tasks;
            taskListView.ItemsSource = _filteredTasks;
            FilterTasks();
        }
        private void OnFilter(object sender, EventArgs e)
        {
            FilterTasks();
        }
        private void FilterTasks()
        {
            _filteredTasks.Clear();
            var filtered = _allTasks.Where(t => t.Date.Date == FilterDate.Date).ToList();
            foreach (var task in filtered)
            {
                _filteredTasks.Add(task);
            }
        }
    }
}