using Microsoft.EntityFrameworkCore;

namespace DragDropExample.Shared.Data
{
    public class DataContext : DbContext
    {
        readonly string connectionString;

        protected DataContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
