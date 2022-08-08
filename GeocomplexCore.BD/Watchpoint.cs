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
        public string WpointType { get; set; } = null!;
        public string? WpointNumber { get; set; }
        public string? WpointLocation { get; set; }
        public DateOnly? WpointDateAdd { get; set; }
        public int? WpointHeight { get; set; }
        public int? WpointIndLandscape { get; set; }
        public string? WpointShrDesck { get; set; }
        public string? WpointNote { get; set; }

        public virtual Route Route { get; set; } = null!;
        public virtual ICollection<WaterpipeWtr> WaterpipeWtrs { get; set; }
        public virtual ICollection<WellHydrogeological> WellHydrogeologicals { get; set; }
        public virtual ICollection<WpointCoordinate> WpointCoordinates { get; set; }
    }
}
