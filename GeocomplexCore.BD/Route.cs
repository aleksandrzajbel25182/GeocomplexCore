using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Маршрут
    /// </summary>
    public partial class Route
    {
        public Route()
        {
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int RouteId { get; set; }
        public int IdDistrict { get; set; }
        public string RouteName { get; set; } = null!;
        public int UserId { get; set; }
        public int? Settlement { get; set; }
        public DateOnly? RouteData { get; set; }
        public string? RouteNote { get; set; }

        public virtual District IdDistrictNavigation { get; set; } = null!;
        public virtual UserDatum User { get; set; } = null!;
        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
