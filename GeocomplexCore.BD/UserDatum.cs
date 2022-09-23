using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public partial class UserDatum
    {
        public UserDatum()
        {
            Districts = new HashSet<District>();
            Routes = new HashSet<Route>();
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int UserId { get; set; }
        public string UserLogin { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserRole { get; set; } = null!;
        public string UserName { get; set; } = null!;

        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
