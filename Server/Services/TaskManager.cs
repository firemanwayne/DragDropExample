using DragDrop.Models;
using DragDrop.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DragDropExample.Server
{
    internal class TaskManager : ITaskManager
    {
        readonly static ConcurrentDictionary<string, IDropableTask> tasks = new();

        public TaskManager()
        { }

        public IEnumerable<IDropableTask> Tasks => tasks.Values;

        public event TasksChangedEventHandler OnTasksChanged;

        public void AddTask(IDropableTask t)
        {
            tasks.AddOrUpdate(t.Id, t, (k, v) => t);

            OnTasksChanged?.Invoke(this, new(t));
        }

        public IDropableTask Find(string Id)
        {
            if (tasks.ContainsKey(Id))
                return tasks.Select(a => a.Value).FirstOrDefault(a => a.Id.Equals(Id));

            else
                return default;
        }

        public void RemoveTask(string Id)
        {
            if (tasks.ContainsKey(Id))
            {
                tasks.Remove(Id, out var result);

                OnTasksChanged?.Invoke(this, new(result));
            }
        }

        public void Update(IDropableTask t)
        {
            if (tasks.ContainsKey(t.Id))
            {
                tasks.TryUpdate(t.Id, t, t);

                OnTasksChanged?.Invoke(this, new(t));
            }
        }
    }
}
