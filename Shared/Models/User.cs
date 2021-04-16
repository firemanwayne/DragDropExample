
using System.Collections;
using System.Collections.Generic;

namespace DragDropExample.Shared
{
    public class User : EntityBase
    {
        public IEnumerable<UserRole> Roles { get; set; } = new List<UserRole>();
    }

    public class Role : EntityBase
    {
    }

    public class UserRole : EntityBase
    {
    }
}
