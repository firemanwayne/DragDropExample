using DragDropExample.Shared;
using DragDropExample.Shared.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragDropExample.Server.Controllers
{

    public class EntityController : Controller
    {
        readonly DataContext Context;
        readonly IRepository<User> UserRepo;
        readonly IRepository<UserRole> UserRoleRepo;

        public EntityController(
            DataContext Context,
            IRepository<User> UserRepo,
            IRepository<UserRole> UserRoleRepo)
        {
            this.Context = Context;
            this.UserRepo = UserRepo;
            this.UserRoleRepo = UserRoleRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Handle(User user)
        {
            var list = new List<Func<Task<EntityBase>>>
            {
                async () => await UserRepo.SaveAsync(user)
            };

            foreach (var r in user.Roles)
                list.Add(async () => await UserRoleRepo.SaveAsync(r));

            await Context.Commit(() => list);

            return Ok();
        }
    }
}
