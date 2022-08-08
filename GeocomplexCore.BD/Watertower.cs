using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Водонапорная башня 
    /// </summary>
    public partial class Watertower
    {
        public int WatertId { get; set; }
        public int WtrIntakeId { get; set; }
        public double? WatertVolume { get; set; }
        public double? WatertDistwell { get; set; }

        public virtual Waterintake WtrIntake { get; set; } = null!;
    }
}
