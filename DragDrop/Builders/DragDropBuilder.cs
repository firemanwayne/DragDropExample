using DragDrop.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegistryExtensions
    {
        public static DragDropBuilder AddDragDropTaskManager<TManager>(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ITaskManager), typeof(TManager));

            return new(services);
        }
    }

    public class DragDropBuilder
    {
        readonly IServiceCollection services;

        public DragDropBuilder(IServiceCollection services)
        {
            this.services = services;
        }
    }
}
