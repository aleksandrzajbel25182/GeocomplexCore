using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Водозабор. Ближайшие объекты
    /// </summary>
    public partial class WtrNearestObj
    {
        public int WtrNrstObjId { get; set; }
        public int WtrIntakeId { get; set; }
        public double? WtrNrstObjDist { get; set; }
        public string? WtrNrstObjView { get; set; }
        public string? WtrNrstObjDescripLoc { get; set; }

        public virtual Waterintake WtrIntake { get; set; } = null!;
    }
}
