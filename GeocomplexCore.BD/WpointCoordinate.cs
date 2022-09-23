using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Координаты точки наблюдения
    /// </summary>
    public partial class WpointCoordinate
    {
        public int WpCoordinatesId { get; set; }
        public int WpointId { get; set; }
        public double? WpCoordinatesX { get; set; }
        public double? WpCoordinatesY { get; set; }
        public double? WpCoordinatesZ { get; set; }

        public virtual Watchpoint Wpoint { get; set; } = null!;
    }
}
