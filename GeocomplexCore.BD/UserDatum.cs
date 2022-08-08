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
            Routes = new HashSet<Route>();
        }

        public int UserId { get; set; }
        public string UserLogin { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserRole { get; set; } = null!;
        public string UserName { get; set; } = null!;

        public virtual ICollection<Route> Routes { get; set; }
    }
}
