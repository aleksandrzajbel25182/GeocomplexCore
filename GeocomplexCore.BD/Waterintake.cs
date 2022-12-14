using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Водозабор
    /// </summary>
    public partial class Waterintake
    {
        public Waterintake()
        {
            WaterObjectDis = new HashSet<WaterObjectDi>();
            WaterintakeFences = new HashSet<WaterintakeFence>();
            Watertowers = new HashSet<Watertower>();
            WellOtherUses = new HashSet<WellOtherUse>();
            WtrFenceangeles = new HashSet<WtrFenceangele>();
        }

        public int WtrIntakeId { get; set; }
        public int WpointId { get; set; }
        public string WtrIntakeName { get; set; } = null!;
        public string? WtrIntakeSubsoiluser { get; set; }
        public string? WtrIntakeLicense { get; set; }
        public int? WtrIntakeColwell { get; set; }
        /// <summary>
        /// Общий водоотбор
        /// </summary>
        public double? WtrIntakeGendrainage { get; set; }
        public int СulvertId { get; set; }
        public int PhotoWtrIntakeId { get; set; }

        public virtual PhotoWaterintake PhotoWtrIntake { get; set; } = null!;
        public virtual WtrNearestObj WtrNearestObj { get; set; } = null!;
        public virtual ICollection<WaterObjectDi> WaterObjectDis { get; set; }
        public virtual ICollection<WaterintakeFence> WaterintakeFences { get; set; }
        public virtual ICollection<Watertower> Watertowers { get; set; }
        public virtual ICollection<WellOtherUse> WellOtherUses { get; set; }
        public virtual ICollection<WtrFenceangele> WtrFenceangeles { get; set; }
    }
}
