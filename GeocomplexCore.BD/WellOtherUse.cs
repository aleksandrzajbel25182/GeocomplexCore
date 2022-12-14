using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Водозабор.Скважина других недропользователей
    /// </summary>
    public partial class WellOtherUse
    {
        public int WellOtherUsId { get; set; }
        public int? WtrIntakeId { get; set; }
        public int WellOtherUsNumber { get; set; }
        public double? WellOtherUsDepth { get; set; }
        public int? OrgId { get; set; }

        public virtual Organization? Org { get; set; }
        public virtual Waterintake? WtrIntake { get; set; }
    }
}
