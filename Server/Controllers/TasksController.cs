using DragDrop.Models;
using DragDrop.Services;
using DragDropExample.Client;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace DragDropExample.Server.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        readonly ITaskManager Tasks;

        public TasksController(ITaskManager Tasks)
        {
            this.Tasks = Tasks;
        }

        [HttpGet]
        public IEnumerable<IDropableTask> Get()
        {
            foreach (var t in Tasks.Tasks)
                yield return t;
        }

        [HttpGet("{id}")]
        public IDropableTask GetById(string id)
        {
            return Tasks.Find(id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] string task)
        {
            var data = JsonSerializer.Deserialize(task, typeof(JobModel));
            var entity = data as JobModel;

            Tasks.Update(entity);

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] string task)
        {
            var data = JsonSerializer.Deserialize(task, typeof(JobModel));
            var entity = data as JobModel;

            Tasks.AddTask(entity);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Tasks.RemoveTask(id);

            return Ok();
        }
    }
}
