using DragDrop.Models;
using System;

namespace DragDropExample.Client
{
    public class JobModel : IDropableTask
    {
        DateTime lastUpdated;
        DateTime? started;
        DateTime? completed;
        StatusEnum status;

        public JobModel()
        {
            Id = Guid.NewGuid().ToString();
            status = StatusEnum.Todo;
            lastUpdated = DateTime.UtcNow;
        }

        public string Id { get; }
        public StatusEnum Status => status;
        public DateTime LastUpdated => lastUpdated; 
        public string Description { get; set; }
        public DateTime? Started => started;
        public DateTime? Completed => completed;

        public event StatusChangedEventHandler OnStatusChanged;

        public void UpdateStatus(StatusEnum s)
        {
            lastUpdated = DateTime.UtcNow;
            status = s;

            switch (s)
            {
                case StatusEnum.Started:
                    started = DateTime.UtcNow;
                    break;

                case StatusEnum.Completed:
                    completed = DateTime.UtcNow;
                    break;

                case StatusEnum.Todo:
                    started = null;
                    break;

                default:break;
            }

            OnStatusChanged?.Invoke(this, new(s));
        }
    }
}
