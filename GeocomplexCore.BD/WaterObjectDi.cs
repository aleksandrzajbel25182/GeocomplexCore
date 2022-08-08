using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Водозабор. Водные объекты в радиусе 1км
    /// </summary>
    public partial class WaterObjectDi
    {
        public int WtrObjDisId { get; set; }
        public int? WtrIntakeId { get; set; }
        public string WtrObjDisName { get; set; } = null!;
        public string? WtrObjDisLoc { get; set; }
        public double? WtrObjDisDistance { get; set; }

        public virtual Waterintake? WtrIntake { get; set; }
    }
}
