using System;

namespace DragDrop.Models
{
    public interface IDropableTask
    {
        string Id { get; }
        string Description { get; }
        StatusEnum Status { get; }
        DateTime LastUpdated { get; }
        public DateTime? Started { get; }
        public DateTime? Completed { get; }

        void UpdateStatus(StatusEnum s);

        event StatusChangedEventHandler OnStatusChanged;
    }
}