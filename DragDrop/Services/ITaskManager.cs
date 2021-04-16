using DragDrop.Models;
using System.Collections.Generic;

namespace DragDrop.Services
{
    public interface ITaskManager
    {
        void Update(IDropableTask t);
        void RemoveTask(string Id);
        void AddTask(IDropableTask t);
        IDropableTask Find(string Id);
        IEnumerable<IDropableTask> Tasks { get; }

        event TasksChangedEventHandler OnTasksChanged;

    }
}
