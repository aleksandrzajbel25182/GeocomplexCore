using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Координаты скважины гидрогеологической
    /// </summary>
    public partial class WellHydroCoordinate
    {
        public int WhydroCoordId { get; set; }
        public int? WellHydroId { get; set; }
        public double? WhydroCoordX { get; set; }
        public double? WhydroCoordY { get; set; }
        public double WhydroCoordZ { get; set; }

        public virtual WellHydrogeological? WellHydro { get; set; }
    }
}
