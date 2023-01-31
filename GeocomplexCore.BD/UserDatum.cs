using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public partial class UserDatum
    {
        public UserDatum()
        {
            Districts = new HashSet<District>();
            Egps = new HashSet<Egp>();
            Grounds = new HashSet<Ground>();
            Plants = new HashSet<Plant>();
            Routes = new HashSet<Route>();
            Surfacewaters = new HashSet<Surfacewater>();
            Techobjects = new HashSet<Techobject>();
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int UserId { get; set; }
        public string UserLogin { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserRole { get; set; } = null!;
        public string UserName { get; set; } = null!;

        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Egp> Egps { get; set; }
        public virtual ICollection<Ground> Grounds { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<Surfacewater> Surfacewaters { get; set; }
        public virtual ICollection<Techobject> Techobjects { get; set; }
        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
