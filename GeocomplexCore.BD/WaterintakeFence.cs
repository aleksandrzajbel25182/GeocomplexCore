using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Водозабор ограждения
    /// </summary>
    public partial class WaterintakeFence
    {
        public int WtrFenceId { get; set; }
        public int WtrIntakeId { get; set; }
        public double? WtrIntakeWidth { get; set; }
        public double? WtrIntakeLength { get; set; }
        public bool? WtrIntakeInstal { get; set; }
        public int WtrFAngeleId { get; set; }

        public virtual WtrFenceangele WtrFAngele { get; set; } = null!;
        public virtual Waterintake WtrIntake { get; set; } = null!;
    }
}
