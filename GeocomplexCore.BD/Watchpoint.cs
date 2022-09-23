using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Точка наблюдения
    /// </summary>
    public partial class Watchpoint
    {
        public Watchpoint()
        {
            WaterpipeWtrs = new HashSet<WaterpipeWtr>();
            WellHydrogeologicals = new HashSet<WellHydrogeological>();
            WpointCoordinates = new HashSet<WpointCoordinate>();
        }

        public int WpointId { get; set; }
        public int RouteId { get; set; }
        public string? WpointType { get; set; }
        public string? WpointNumber { get; set; }
        public string? WpointLocation { get; set; }
        public DateOnly? WpointDateAdd { get; set; }
        public string? WpointNote { get; set; }
        public int FUserId { get; set; }
        public int FWpointCoord { get; set; }
        public string? WpointIndLandscape { get; set; }

        public virtual UserDatum FUser { get; set; } = null!;
        public virtual Route Route { get; set; } = null!;
        public virtual ICollection<WaterpipeWtr> WaterpipeWtrs { get; set; }
        public virtual ICollection<WellHydrogeological> WellHydrogeologicals { get; set; }
        public virtual ICollection<WpointCoordinate> WpointCoordinates { get; set; }
    }
}
