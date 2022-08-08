using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Водозабор углы ограждения
    /// </summary>
    public partial class WtrFenceangele
    {
        public WtrFenceangele()
        {
            WaterintakeFences = new HashSet<WaterintakeFence>();
        }

        public int WtrFAngeleId { get; set; }
        public int WtrIntakeId { get; set; }
        public int? WtrFAngeleNumber { get; set; }
        public double? WtrFAngeleZ { get; set; }
        public double? WtrFAngeleY { get; set; }
        public double? WtrFAngeleX { get; set; }

        public virtual Waterintake WtrIntake { get; set; } = null!;
        public virtual ICollection<WaterintakeFence> WaterintakeFences { get; set; }
    }
}
