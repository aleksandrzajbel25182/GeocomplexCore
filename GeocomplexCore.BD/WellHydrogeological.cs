using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Скважина гидрогеологическая
    /// </summary>
    public partial class WellHydrogeological
    {
        public WellHydrogeological()
        {
            WellHydroCoordinates = new HashSet<WellHydroCoordinate>();
        }

        public int WellHydroId { get; set; }
        public int? WpointId { get; set; }
        public string WellHydroName { get; set; } = null!;
        public string? WellHydroAddres { get; set; }
        public string? WellHydroGeoBinding { get; set; }
        public string? WellHydroPassport { get; set; }
        public int? DrilOrgId { get; set; }
        public DateOnly? WellHydroDateDrillingStart { get; set; }
        public DateOnly? WellHydroDateDrillingEnd { get; set; }
        public double? WellHydroDepth { get; set; }
        public double? WellHydroDiametr { get; set; }
        public bool? Waterintake { get; set; }
        public string? WellHydroNote { get; set; }

        public virtual Watchpoint? Wpoint { get; set; }
        public virtual ICollection<WellHydroCoordinate> WellHydroCoordinates { get; set; }
    }
}
