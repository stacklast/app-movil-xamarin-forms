using System.Collections.ObjectModel;

namespace TaskApp.Models
{
    public class GroupedTaskItem : ObservableCollection<TaskItem>
    {
        public string GroupName { get; set; }
        public string ShortName { get; set; }

        // Constructor to initialize the collection and properties
        public GroupedTaskItem(string groupName, string shortName)
        {
            GroupName = groupName;
            ShortName = shortName;
        }
    }
}
