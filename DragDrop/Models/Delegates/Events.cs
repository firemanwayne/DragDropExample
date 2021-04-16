using System;

namespace DragDrop.Models
{
    public delegate void StatusChangedEventHandler(object s, StatusChangedEventArgs a);
    public delegate void TasksChangedEventHandler(object s, TaskChangedEventArgs a);


    public class StatusChangedEventArgs : EventArgs
    {
        public StatusEnum Status { get; }

        public StatusChangedEventArgs(StatusEnum Status)
        {
            this.Status = Status;
        }

        public static implicit operator StatusChangedEventArgs(StatusEnum s) => new(s);
    }

    public class TaskChangedEventArgs : EventArgs
    {
        public IDropableTask Task { get; }

        public TaskChangedEventArgs(IDropableTask Task)
        {
            this.Task = Task;
        }
    }
}