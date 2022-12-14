using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Точки участка/координаты
    /// </summary>
    public partial class DistrictPoint
    {
        public int IdDisctrictPoint { get; set; }
        public int IdDistrict { get; set; }
        public double? DisctrictPointX { get; set; }
        public double? DisctrictPointY { get; set; }
        public double? DisctrictPointZ { get; set; }

        public virtual District IdDistrictNavigation { get; set; } = null!;
    }
}
